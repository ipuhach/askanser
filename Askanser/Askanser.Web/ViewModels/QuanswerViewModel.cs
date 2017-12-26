using Askanser.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Askanser.Web.ViewModels
{
    public class QuanswerViewModel
    {

        [Required]
        [Display(Name = "Quanswer")]
        public Quanswer Quanswer { get; set; }

        [Required]
        [Display(Name = "Score")]
        [Range(-10, 10, ErrorMessage = "Numbers should be beetween {1} and {2}")]
        public int Score { get; set; }

    }
}
