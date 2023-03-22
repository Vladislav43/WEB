using Fluentify.Models.Configuration;
using Fluentify.Models.Database.Tables;
using Fluentify.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Fluentify.Database;
using Fluentify.Database.Interfaces;
using Fluentify.Database.Services;
using Fluentify.Models.Configuration;
using Fluentify.Models.Database;

namespace Fluentify.Test
{
    [TestClass]
    public class UserTests : TestBase
    {
        UserService _usersService;
        IOptions<AppConfig> _configuration;
        StoreDbContext _dbContext;

        public UserTests()
        {
            _dbContext = ResolveService<StoreDbContext>();
            _usersService = ResolveService<UserService>();
            _configuration = ResolveService<IOptions<AppConfig>>();
        }

        [TestMethod]
        public async Task Create()
        {
            var user = await _usersService.Create(new Users()
            {
                Nickname = "Test User Service",
                Email = "test_username_7",
                Password = "test_password",
                RegDate = DateTime.Now,
            });
        }

        [TestMethod]
        public void GetAllUsers()
        {
            var usersTest = _usersService.GetAll().ToListAsync();
        }
    }
}