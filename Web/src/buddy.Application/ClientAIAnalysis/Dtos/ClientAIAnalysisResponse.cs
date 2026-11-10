using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buddy.ClientAIAnalysis.Dtos
{

    public class ClientAIAnalysisResponse
    {
        public List<string> PossibleCauses { get; set; }
        public List<string> Recommendations { get; set; }
        public List<string> WhatToTellYourVet { get; set; }
    }
}
