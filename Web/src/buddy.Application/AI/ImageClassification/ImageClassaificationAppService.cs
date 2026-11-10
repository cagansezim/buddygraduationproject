using System.Threading.Tasks;
using Abp.Authorization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System;
using buddy.AI.ImageClassification.Dto;

namespace buddy.AI.ImageClassification
{
    [AbpAuthorize]
    public class ImageClassificationAppService : buddyAppServiceBase, IImageClassificationAppService
    {
        private readonly HttpClient _httpClient;
        private static readonly string ApiKey = System.Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? string.Empty; // set GEMINI_API_KEY before running
        private const string TextApiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

        public ImageClassificationAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", ApiKey);
        }

        public async Task<string> ClassifyImageAsync(ClassifyImageInput input)
        {
            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = "image/jpeg", // Adjust if needed (e.g., image/png)
                                    data = Convert.ToBase64String(input.ImageBytes)
                                }
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.3
                },
                systemInstruction = new
                {
                    parts = new[]
                    {
                        new { text = "You are an expert image classifier. Respond only with 'dog' or 'cat' based on the image." }
                    }
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(TextApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseString);

            return responseObject?.candidates[0]?.content?.parts[0]?.text?.ToString() ?? "Classification not found";
        }
    }
}