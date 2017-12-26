using Askanser.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Askanser.Web.ViewModels
{
    public class AnswerViewModel
    {
        [Required]
        [Display(Name = "Question")]
        public Question Question { get; set; }

        [Required]
        [Display(Name = "Text")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long. And less then {1}", MinimumLength = 4)]
        public string Text { get; set; }

    }
}
