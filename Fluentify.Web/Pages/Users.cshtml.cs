using Fluentify.Database.Interfaces;
using Fluentify.Models.Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Fluentify.Models.Database;

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
}