using System.Collections.Generic;
using Fluentify.Web.Areas.Identity.Data;

namespace Fluentify.Web.Models
{
    public class TestViewModel
    {
        public int CurrentTaskIndex { get; set; }
        public List<MathTask> MathTasks { get; set; }
        public int Score { get; set; }
        public string Answer { get; set; }
    }
}
