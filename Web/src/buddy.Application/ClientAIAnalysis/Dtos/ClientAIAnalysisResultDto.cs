using System;
using System.Collections.Generic;

namespace buddy.ClientAIAnalysis.Dtos
{
    public class ClientAIAnalysisResultDto
    {
        public string Prompt { get; set; }
        public string Duration { get; set; }
        public string PetType { get; set; }
        public string Comments { get; set; }
        public List<byte[]> ImageBytesList { get; set; } = new List<byte[]>();
        public List<string> MimeTypes { get; set; } = new List<string>();
        public List<string> PossibleCauses { get; set; } = new List<string>();
        public List<string> Recommendations { get; set; } = new List<string>();
        public List<string> WhatToTellYourVet { get; set; } = new List<string>();
        public DateTime Date { get; set; }
    }
}
