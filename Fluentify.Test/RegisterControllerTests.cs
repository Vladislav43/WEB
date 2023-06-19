using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Areas.Identity.Pages.Account;
using Fluentify.Web.Controllers;
using Fluentify.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using net.openstack.Providers.Rackspace.Objects.Monitoring;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Fluentify.UnitTests.Controllers
{
    public class RegisterControllerTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Index_Post_WithValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
            );

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null
            );

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerModel = new RegisterModel.InputModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "P@ssw0rd"
            };

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password))
                .ReturnsAsync(IdentityResult.Success);

            signInManagerMock.Setup(x => x.SignInAsync(It.IsAny<ApplicationUser>(), false, null))
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            var controller = new RegisterController(userManagerMock.Object, signInManagerMock.Object, loggerMock.Object);

            // Act
            var result = await controller.Index(registerModel);

            // Assert
            Xunit.Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            Xunit.Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async System.Threading.Tasks.Task Index_Post_WithInvalidModel_ReturnsViewWithModelErrors()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
            );

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null
            );

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerModel = new RegisterModel.InputModel
            {
                Email = "john.doe@example.com",
                Password = "P@ssw0rd"
            };

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error description" }));

            var controller = new RegisterController(userManagerMock.Object, signInManagerMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError(string.Empty, "Model error");

            // Act
            var result = await controller.Index(registerModel);

            // Assert
            Xunit.Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Xunit.Assert.Equal(registerModel, viewResult.Model);
            Xunit.Assert.False(viewResult.ViewData.ModelState.IsValid);
            Xunit.Assert.Equal("Error description", viewResult.ViewData.ModelState[string.Empty].Errors[0].ErrorMessage);
            Xunit.Assert.Equal("Model error", viewResult.ViewData.ModelState[string.Empty].Errors[1].ErrorMessage);
        }
    }
}