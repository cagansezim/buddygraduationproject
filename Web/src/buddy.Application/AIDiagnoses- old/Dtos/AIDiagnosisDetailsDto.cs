using System;
using System.Collections.Generic;
using System.Linq;

namespace buddy.AIDiagnosis.Dtos
{
    public class AIDiagnosisDetailsDto
    {
        public int Id { get; set; }
        public string PetName { get; set; }
        public DateTime Date { get; set; }
        public string Symptoms { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public List<string> Causes { get; set; } = new List<string>();
        public List<string> Recommendations { get; set; } = new List<string>();
        public List<string> VetInstructions { get; set; } = new List<string>();

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
