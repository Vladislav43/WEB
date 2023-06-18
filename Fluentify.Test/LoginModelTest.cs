using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fluentify.UnitTests.Areas.Identity.Pages.Account
{
    public class LoginModelTests
    {
        [Fact]
        public async Task OnPostAsync_WithValidInput_RedirectsToReturnUrl()
        {
            // Arrange
            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                Mock.Of<UserManager<ApplicationUser>>(),
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<ILogger<SignInManager<ApplicationUser>>>(),
                Mock.Of<IAuthenticationSchemeProvider>()
            );

            var loggerMock = new Mock<ILogger<LoginModel>>();
            var loginModel = new LoginModel(signInManagerMock.Object, loggerMock.Object)
            {
                Input = new LoginModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "P@ssw0rd",
                    RememberMe = false
                }
            };

            var returnUrl = "/home";
            signInManagerMock.Setup(x => x.PasswordSignInAsync(loginModel.Input.Email, loginModel.Input.Password, loginModel.Input.RememberMe, false))
                            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await loginModel.OnPostAsync(returnUrl);

            // Assert
            Xunit.Assert.IsType<LocalRedirectResult>(result);
            var redirectResult = (LocalRedirectResult)result;
           Xunit.Assert.Equal(returnUrl, redirectResult.Url);
        }
    }
}
