using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Data;
using Fluentify.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fluentify.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly FluentifyDbContext _context;

        public TestController(FluentifyDbContext context)
        {
            _context = context;
        }

        public IActionResult Start()
        {
            // Отримати список завдань з бази даних
            var mathTasks = _context.Tasks.ToList();

            // Створити модель тесту і заповнити її
            var model = new TestViewModel
            {
                CurrentTaskIndex = 0,
                MathTasks = mathTasks,
                Score = 0
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Start(TestViewModel model, string answer)
        {
            if (ModelState.IsValid)
            {
                if (model.CurrentTaskIndex >= 0 && model.CurrentTaskIndex < model.MathTasks.Count)
                {
                    // Перевірити, чи відповідь правильна
                    if (answer == model.MathTasks[model.CurrentTaskIndex].CorrectAnswer)
                    {
                        // Збільшити бал за правильну відповідь
                        model.Score++;
                    }
                    else
                    {
                        // Якщо відповідь неправильна, вивести повідомлення про помилку
                        ModelState.AddModelError(string.Empty, "Ваша відповідь не правильна. Спробуйте ще раз.");
                      
                    }

                    // Збільшити індекс поточного завдання
                    model.CurrentTaskIndex++;
                }

                // Перевірити, чи всі завдання вже виконані
                if (model.CurrentTaskIndex >= model.MathTasks.Count)
                {
                    // Перенаправлення до сторінки з результатами
                 
                }
            }

            return View(model);
        }


        public IActionResult Result()
        {
            // TODO: Додайте відображення результатів тесту
            return View();
        }
    }
}
