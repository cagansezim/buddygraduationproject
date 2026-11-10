using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buddy.ClientAIAnalysis.Dtos
{
    public class ClientAnalysisDetailsDto
    {
        public Guid Id { get; set; }
        public string PetName { get; set; }
        public DateTime Date { get; set; }
        public string Symptoms { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string StatusBadge => Status switch
        {
            "Pending" => "warning",
            "In Progress" => "info",
            "Completed" => "success",
            "Failed" => "danger",
            _ => "secondary"
        };
    }
}
