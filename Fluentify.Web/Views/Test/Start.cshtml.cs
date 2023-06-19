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
                // Збереження результатів тестування
                SaveTestResults(TestViewModel);

                return RedirectToPage("TestResult", new { id = TestViewModel.TestResult.UserID });
            }

            // Якщо дані недійсні, повертаємо сторінку тестування
            return Page();
        }

        private void SaveTestResults(TestViewModel model)
        {
            // Створення об'єкту з результатами тестування для збереження у базі даних
            var testResult = new TestResult
            {
                UserID = model.TestResult.UserID,
                UserLevel = model.TestResult.UserLevel,
                CorrectAnswersCount = model.TestResult.CorrectAnswersCount
            };

            // Збереження результатів тестування у базі даних використовуючи ORM або інші методи
            _dbContext.TestResults.Add(testResult);
            _dbContext.SaveChanges();
        }
    }
}
