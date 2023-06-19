using System.ComponentModel.DataAnnotations;
namespace Fluentify.Web.Areas.Identity.Data
{
    public class TestResult
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public int UserLevel { get; set; }

        public int CorrectAnswersCount { get; set; }
    }
}
