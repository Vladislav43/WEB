using System.Collections.Generic;
using Fluentify.Web.Areas.Identity.Data;

namespace Fluentify.Web.Models
{
    public class TestViewModel
    {
        public int CurrentTaskIndex { get; set; }
            public List<MathTask> MathTasks { get; set; }
        public int Score { get; set; }
        public TestResult TestResult { get; set; }
    }

    public class Task
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
       
    }
}
