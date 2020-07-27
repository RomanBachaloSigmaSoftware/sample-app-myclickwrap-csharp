using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DocuSign.MyClick.COVID19Waiver.Controllers;
using DocuSign.MyClick.COVID19Waiver.Exceptions;
using DocuSign.MyClick.COVID19Waiver.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DocuSign.MyClick.COVID19Waiver.UnitTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly Mock<IDocuSignAuthenticationService> _docuSignAuthServiceMock;
        private readonly AccountController _sut;

        public AccountControllerTests()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(
                    It.IsAny<HttpContext>(),
                    It.IsAny<string>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            _docuSignAuthServiceMock = new Mock<IDocuSignAuthenticationService>();
            _docuSignAuthServiceMock
                .Setup(_ => _.AuthenticateFromJwt())
                .Returns((new ClaimsPrincipal(), new AuthenticationProperties()));

            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.SetupGet(x => x.IsAuthenticated).Returns(true);
            mockIdentity.SetupGet(x => x.Name).Returns("TestUser");
            mockIdentity.SetupGet(x => x.AuthenticationType).Returns("Test");

            _sut = new AccountController(_docuSignAuthServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = _serviceProviderMock.Object,
                        User = new ClaimsPrincipal(mockIdentity.Object)
                    }
                }
            };
        }

        [Fact]
        public void Login_WhenÑonsentApproved_SignsInUser()
        {
            //Arrange 

            //Act
            var result = _sut.Login().Result;

            //Assert
            result.Should().BeEquivalentTo(new LocalRedirectResult("/"));
        }

        [Fact]
        public void Login_WhenÑonsentNotApproved_UserRedirectedConsentApprovalPage()
        {
            // Arrange
            _docuSignAuthServiceMock
                .Setup(_ => _.AuthenticateFromJwt())
                .Throws<ConsentRequiredException>();
            _docuSignAuthServiceMock
                .Setup(_ => _.GetConsentUrl(It.IsAny<string>()))
                .Returns("http://testConsentUrl");

            //Act
            var result = _sut.Login().Result;

            //Assert
            result.Should().BeEquivalentTo(new LocalRedirectResult("http://testConsentUrl"));
        }

        [Fact]
        public void IsAuthenticated_WhenPrincipalExists_ReturnsTrue()
        {
            //Arrange 

            //Act
            var result = _sut.IsAuthenticated();

            //Assert
            result.Should().BeEquivalentTo(new OkObjectResult(true));
        }
    }
}
