using System.Collections.Generic;

namespace buddy.AIDiagnosis.Dtos
{
    public class AIDiagnosisInputDto
    {
        public string Prompt { get; set; }
        public string Duration { get; set; }
        public string PetType { get; set; }
        public string Comments { get; set; }
        public List<byte[]> ImageBytesList { get; set; } = new List<byte[]>();
        public List<string> MimeTypes { get; set; } = new List<string>();
    }
}
