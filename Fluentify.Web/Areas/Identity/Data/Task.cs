using System.ComponentModel.DataAnnotations;
namespace Fluentify.Web.Areas.Identity.Data
{
    public class MathTask
    {
        [Key]
        public int Id { get; set; }

        public int UnitId { get; set; }

        public int TaskId { get; set; }

        public string Question { get; set; }

        public string CorrectAnswer { get; set; }
    }
}
