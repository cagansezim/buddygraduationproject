using System.ComponentModel.DataAnnotations;

namespace buddy.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}