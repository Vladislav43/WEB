using Fluentify.Database.Interfaces;
using Fluentify.Models.Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Fluentify.Models.Database;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Fluentify.Web.Pages
{
    public class UsersModel : PageModel
    {
        public IList<Users> Users { get; set; }

        private readonly IDbEntityService<Users> _userService;

        public UsersModel(IDbEntityService<Users> userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            Users = await _userService.GetAll().ToListAsync();
        }
    }

    public class LoginModel : PageModel
    {
        private readonly SignInManager<Users> _signInManager;

        public LoginModel(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetGoogleLoginAsync(string returnUrl = "/")
        {
            var redirectUrl = Url.Page("/Login", pageHandler: "GoogleCallback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        public async Task<IActionResult> OnGetGoogleCallbackAsync(string returnUrl = "/")
        {
            var result = await _signInManager.ExternalLoginSignInAsync("Google", "", isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
        }
    }
}
