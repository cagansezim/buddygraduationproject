using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buddy.ClientAIAnalysis.Dtos
{
    public class ClientAnalysisDto
    {
        public Guid Id { get; set; }
        public string PetName { get; set; }
        public DateTime Date { get; set; }
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
