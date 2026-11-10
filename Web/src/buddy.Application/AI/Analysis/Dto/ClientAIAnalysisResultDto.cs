using System.Collections.Generic;
using System;

namespace buddy.AI.Analysis.Dto
{
    public class ClientAIAnalysisResultDto
    {
        /* original sections */
        public string Prompt { get; set; }
        public string Duration { get; set; }
        public string PetType { get; set; }
        public string Comments { get; set; }

        public int? PetId { get; set; }
        public DateTime Date { get; set; }

        public List<byte[]> ImageBytesList { get; set; } = new();
        public List<string> MimeTypes { get; set; } = new();

        public List<string> PossibleCauses { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public List<string> WhatToTellYourVet { get; set; } = new();

        /* step‑2 picks */
        public int? ClinicId { get; set; }
        public int? VetUserId { get; set; }
    }
}
