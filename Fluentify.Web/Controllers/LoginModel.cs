using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fluentify.Web.Areas.Identity.Pages.Account;

namespace Fluentify.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{UserID}")]
        public IActionResult Get(int UserID)
        {
            // Логіка для GET-запиту
            _logger.LogInformation($"Received a GET request for login with id: {UserID}");

            // Додаткові дії для GET-запиту, якщо потрібно

            return Ok($"This is the login page for user with id: {UserID}");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Перевірка логіна і пароля
            if (model.Input.Email == "admin@example.com" && model.Input.Password == "password")
            {
                // Якщо вірні дані, виконайте додаткові дії
                // Наприклад, збереження даних користувача до бази даних або створення токена доступу

                return Ok("Login successful");
            }

            // Якщо дані невірні, поверніть відповідь з помилкою
            ModelState.AddModelError("", "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }
}

