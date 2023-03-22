using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fluentify.Database.Interfaces;
using Fluentify.Models.Database;
using Fluentify.Database;
using System.Globalization;
using Fluentify.Database.Services;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Fluentify.Models.Database.Tables;
using Fluentify.Models.Frontend;

namespace Fluentify.Web.Pages
{
    public class UserService : PageModel
    {
        [BindProperty]
        public CreateUser Users { get; set; }

        private readonly Database.Services.UserService _userService;

        public UserService(Database.Services.UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            // Check if username already exists
            if (await _userService.UserNameExists(Users.Nickname))
            {
                ModelState.AddModelError("Users.UserName", "Username already exists.");
                OnGet();
                return Page();
            }

            // Check if email already exists
            else if (await _userService.EmailExists(Users.Email))
            {
                ModelState.AddModelError("Users.EmailAddress", "Current email already in use.");
                OnGet();
                return Page();
            }

            await _userService.Create(new Users()
            {
                Nickname = Users.Nickname,
                Email = Users.Email,
                Password = Users.Password,
                RegDate = DateTime.Now,
            });

            return new RedirectToPageResult("/Users");
        }
    }
}