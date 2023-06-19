using Fluentify.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fluentify.Web.Data;
using Fluentify.Web.Areas.Identity.Data;

namespace Fluentify.Web.Views.Test
{
    public class StartModel : PageModel
    {
        private readonly FluentifyDbContext _dbContext;

        [BindProperty]
        public TestViewModel TestViewModel { get; set; }

        public StartModel(FluentifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [HttpPost]
        public IActionResult OnPostSubmitTest()
        {
            if (ModelState.IsValid)
            {
                // ���������� ���������� ����������
                SaveTestResults(TestViewModel);

                return RedirectToPage("TestResult", new { id = TestViewModel.TestResult.UserID });
            }

            // ���� ��� ������, ��������� ������� ����������
            return Page();
        }

        private void SaveTestResults(TestViewModel model)
        {
            // ��������� ��'���� � ������������ ���������� ��� ���������� � ��� �����
            var testResult = new TestResult
            {
                UserID = model.TestResult.UserID,
                UserLevel = model.TestResult.UserLevel,
                CorrectAnswersCount = model.TestResult.CorrectAnswersCount
            };

            // ���������� ���������� ���������� � ��� ����� �������������� ORM ��� ���� ������
            _dbContext.TestResults.Add(testResult);
            _dbContext.SaveChanges();
        }
    }
}
