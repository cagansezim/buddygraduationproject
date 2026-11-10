using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using buddy.AI.Analysis.Dto;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using buddy.AI.Analysis;

namespace buddy.ImageClassification
{
    [AbpAuthorize]
    public class AnalysisAppService : buddyAppServiceBase, IAnalysisAppService
    {
        private readonly HttpClient _httpClient;
        private static readonly string ApiKey = System.Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? string.Empty; // set GEMINI_API_KEY before running
        private const string TextApiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

        public AnalysisAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", ApiKey);
        }

        public async Task<string> AnalyzeAsync(AnalysisInput input)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(input.Prompt) && (input.ImageBytesList == null || input.ImageBytesList.Count == 0))
            {
                throw new Exception("At least one of Prompt or ImageBytes must be provided.");
            }

            // Construct the structured prompt
            string structuredPrompt = "You are an AI assistant providing a health analysis for a pet. Please format your response with the following sections, ensuring each section is clearly separated and labeled with the exact headings below. Use numbered lists for \"Possible Causes\", \"Recommendations\", and \"What to Tell Your Veterinarian\" to list individual items. Do not use ** characters for emphasis; use plain text instead:\n\n" +
                                     "Symptoms Analysis\n[Provide a detailed analysis of the symptoms here]\n\n" +
                                     "Possible Causes\n1. [First possible cause with a brief description]\n2. [Second possible cause with a brief description]\n3. [Third possible cause with a brief description]\n[...and so on]\n\n" +
                                     "Recommendations\n1. [First recommendation]\n2. [Second recommendation]\n3. [Third recommendation]\n[...and so on]\n\n" +
                                     "What to Tell Your Veterinarian\n1. [First item to tell the vet]\n2. [Second item to tell the vet]\n3. [Third item to tell the vet]\n[...and so on]\n\n" +
                                     "Provide your response based on the following input:\n" +
                                     $"Pet Type: {input.PetType}\nSymptoms: {input.Prompt}\nDuration: {input.Duration}\nComments: {input.Comments}";

            var parts = new List<object>
            {
                new { text = structuredPrompt }
            };

            // Add image data if provided
            if (input.ImageBytesList != null && input.ImageBytesList.Count > 0)
            {
                for (int i = 0; i < input.ImageBytesList.Count; i++)
                {
                    parts.Add(new
                    {
                        inline_data = new
                        {
                            mime_type = input.MimeTypes[i] ?? "image/jpeg",
                            data = Convert.ToBase64String(input.ImageBytesList[i])
                        }
                    });
                }
            }

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = parts.ToArray()
                    }
                },
                generationConfig = new
                {
                    temperature = input.Temperature ?? 0.3
                },
                systemInstruction = new
                {
                    parts = new[]
                    {
                        new { text = input.SystemInstruction }
                    }
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(TextApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseString);

            return responseObject?.candidates?[0]?.content?.parts?[0]?.text?.ToString() ?? "Analysis not found";
        }
    }
}