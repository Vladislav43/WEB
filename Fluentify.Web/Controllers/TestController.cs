using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Data;
using Fluentify.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Fluentify.Web.Controllers
{
    
  
    public class TestController : Controller
    {
        private readonly FluentifyDbContext _context;

        public TestController(FluentifyDbContext context)
        {
            _context = context;
        }

        public IActionResult Start(int level)
        {
            // Отримати список завдань для вказаного рівня (TaskId = level)
            var mathTasks = _context.Tasks.Where(t => t.TaskId == level).ToList();

            // Перевірити, чи є завдання для вказаного рівня
            if (mathTasks.Count == 0)
            {
                return NotFound(); // Якщо немає завдань, повернути статус 404
            }

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
                    // Отримати правильну відповідь для поточного завдання
                    var correctAnswer = model.MathTasks[model.CurrentTaskIndex].CorrectAnswer;

                    // Перевірити, чи відповідь правильна
                    if (answer == correctAnswer)
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
                    return RedirectToAction("Result");
                }
            }
            else
            {
                // Ініціалізувати model.MathTasks пустим списком, якщо потрібно
                model.MathTasks = new List<MathTask>();
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
