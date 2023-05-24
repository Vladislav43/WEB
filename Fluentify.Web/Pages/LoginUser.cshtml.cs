using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fluentify.Models.Frontend;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fluentify.Web.Pages
{
    public class LoginUserModel : PageModel
    {
        [BindProperty]
        public new LoginUserRequest User { get; set; }

        private readonly Database.Services.UserService _userService;

        public LoginUserModel(Database.Services.UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
    }
}