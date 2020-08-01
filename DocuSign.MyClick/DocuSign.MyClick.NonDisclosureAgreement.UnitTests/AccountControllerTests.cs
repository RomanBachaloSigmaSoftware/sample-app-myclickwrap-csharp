using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DocuSign.MyClick.NonDisclosureAgreement.Controllers;
using DocuSign.MyClick.NonDisclosureAgreement.Exceptions;
using DocuSign.MyClick.NonDisclosureAgreement.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;

namespace DocuSign.MyClick.NonDisclosureAgreement.UnitTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly Mock<IDocuSignAuthenticationService> _docuSignAuthServiceMock;
        private readonly Mock<IUrlHelper> _urlHelperMock;
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
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            _serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.SetupGet(x => x.IsAuthenticated).Returns(true);
            mockIdentity.SetupGet(x => x.Name).Returns("TestUser");
            mockIdentity.SetupGet(x => x.AuthenticationType).Returns("Test");
            _urlHelperMock = new Mock<IUrlHelper>();

            _sut = new AccountController(_docuSignAuthServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = _serviceProviderMock.Object,
                        User = new ClaimsPrincipal(mockIdentity.Object)
                    }
                },
                Url = _urlHelperMock.Object
            };
        }

        [Fact]
        public void Login_WhenÑonsentApproved_SignsInUser()
        {
            //Arrange 

            //Act
            IActionResult result = _sut.Login().Result;

            //Assert
            result.Should().BeEquivalentTo(new LocalRedirectResult("/"));
        }

        [Fact]
        public void Login_WhenReturnUrlProviderAndUrlIsLocal_SignsInUserAndRedirectsToReturnUrl()
        {
            //Arrange 
            _urlHelperMock.Setup(x => x.IsLocalUrl("/returnurl")).Returns(true);

            //Act
            IActionResult result = _sut.Login("/returnurl").Result;

            //Assert
            result.Should().BeEquivalentTo(new LocalRedirectResult("/returnurl"));
        }

        [Fact]
        public void Login_WhenReturnUrlProviderButUrlIsNotLocal_SignsInUserButRedirectsToDefaultUrl()
        {
            //Arrange 
            _urlHelperMock.Setup(x => x.IsLocalUrl("htts://returnurl")).Returns(false);

            //Act
            IActionResult result = _sut.Login("htts://returnurl").Result;

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
            IActionResult result = _sut.Login().Result;

            //Assert
            result.Should().BeEquivalentTo(new LocalRedirectResult("http://testConsentUrl"));
        }

        [Fact]
        public void IsAuthenticated_WhenPrincipalExists_ReturnsTrue()
        {
            //Arrange 

            //Act
            IActionResult result = _sut.IsAuthenticated();

            //Assert
            result.Should().BeEquivalentTo(new OkObjectResult(true));
        }
    }
}