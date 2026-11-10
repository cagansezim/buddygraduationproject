using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buddy.ClientAIAnalysis.Dtos
{
    public class CreateAnalysisDto
    {
        public string PetName { get; set; }
        public string Symptoms { get; set; }
        public int PetId { get; set; }
    }
}
