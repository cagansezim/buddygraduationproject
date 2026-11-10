using System.Collections.Generic;

namespace MyPets.ClientAIAnalysis.Dto
{
    public class ClientAIAnalysisInput
    {
        public string Prompt { get; set; }
        public string Duration { get; set; }
        public string PetType { get; set; }
        public string Comments { get; set; }
        public List<byte[]> ImageBytesList { get; set; }
        public List<string> MimeTypes { get; set; }
    }

}