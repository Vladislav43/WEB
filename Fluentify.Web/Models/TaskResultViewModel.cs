using System.ComponentModel.DataAnnotations;

namespace Fluentify.Web.Models
{
    public class TaskResultViewModel
    {

        [Display(Name = "Question")]
        public string Question { get; set; }

        [Display(Name = "Your Answer")]
        public string UserAnswer { get; set; }

        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }

        [Display(Name = "Result")]
        public bool IsCorrect { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }
    }
}
