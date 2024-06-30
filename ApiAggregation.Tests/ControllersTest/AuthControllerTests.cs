using ApiAggregation.Controllers;
using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.User;
using ApiAggregation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace ApiAggregation.Tests.ControllersTest
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<AuthTokenService> _tokenServiceMock = null!;
        private Mock<IOptions<ApiSettings>> _settingsMock = null!;
        private AuthController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _tokenServiceMock = new Mock<AuthTokenService>();
            _settingsMock = new Mock<IOptions<ApiSettings>>();

            _settingsMock.Setup(s => s.Value).Returns(new ApiSettings
            {
                LoginModel = new LoginModelSettings
                {
                    Username = "kon-amp",
                    Password = "123qweEWQ#@!"
                }
            });

            _controller = new AuthController(_tokenServiceMock.Object, _settingsMock.Object);
        }

        [TestMethod]
        public void Login_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "kon-amp", Password = "123qweEWQ#@!" };

            // Act
            var result = _controller.Login(loginModel) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result?.StatusCode);
            Assert.IsTrue(result?.Value?.ToString()?.Contains("token") ?? false);
        }

        [TestMethod]
        public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "wronguser", Password = "wrongpassword" };

            // Act
            var result = _controller.Login(loginModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }
    }
}
