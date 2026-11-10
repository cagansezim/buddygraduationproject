using System.Collections.Generic;

namespace buddy.AI.Analysis.Dto
{
    public class AnalysisInput
    {
        public string Prompt { get; set; }
        public string Duration { get; set; }
        public string PetType { get; set; }
        public string Comments { get; set; }
        public List<byte[]> ImageBytesList { get; set; } // Updated to a list for multiple images
        public List<string> MimeTypes { get; set; } // Updated to a list for multiple MIME types
        public string SystemInstruction { get; set; }
        public float? Temperature { get; set; }

        public AnalysisInput()
        {
            ImageBytesList = new List<byte[]>();
            MimeTypes = new List<string>();
        }
    }
}