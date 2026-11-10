using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace buddy.Adoptions.Dtos
{
    public class PagedAdoptionResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }

        [Range(0, 240, ErrorMessage = "MaxAge must be between 0 and 240.")]
        public int? MaxAge { get; set; }

        [Range(0, 100, ErrorMessage = "MaxWeight must be between 0 and 100.")]
        public double? MaxWeight { get; set; }
    }
}
