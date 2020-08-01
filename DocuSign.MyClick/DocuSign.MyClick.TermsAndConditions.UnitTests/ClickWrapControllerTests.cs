using System.Collections.Generic;
using System.Security.Claims;
using AutoFixture.Xunit2;
using DocuSign.MyClick.TermsAndConditions.Controllers;
using DocuSign.MyClick.TermsAndConditions.Domain;
using DocuSign.MyClick.TermsAndConditions.Models;
using DocuSign.MyClick.TermsAndConditions.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace DocuSign.MyClick.TermsAndConditions.UnitTests
{
    public class ClickWrapControllerTests
    {
        private readonly Mock<IClickWrapService> _clickWrapService;
        private readonly ClickWrapController _sut;

        public ClickWrapControllerTests()
        {
            _clickWrapService = new Mock<IClickWrapService>();
            _sut = new ClickWrapController(_clickWrapService.Object);
        }

        [Theory]
        [AutoData]
        public void Index_WhenGetClickWrap_ReturnsCorrectResult(
            Account account,
            User user)
        {
            // Arrange 
            InitContext(account, user);
            var clickWrap = new ClickWrap
            {
                ClickwrapId = "1",
                ClickwrapName = "Covid19Waiver"
            };
            _clickWrapService
                .Setup(c => c.GetClickWrap(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => clickWrap);

            // Act
            IActionResult result = _sut.Index();

            // Assert
            result.Should().BeEquivalentTo(new OkObjectResult
            (new ResponseClickWrapModel
            {
                AccountId = account.Id,
                ClickWrap = clickWrap,
                UserId = user.Id,
                DocuSignBaseUrl = account.BaseUri
            }));
        }

        private void InitContext(Account account, User user)
        {
            var context = new Context();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            claimsIdentity.AddClaim(new Claim("accounts", JsonConvert.SerializeObject(account)));
            claimsIdentity.AddClaim(new Claim("account_id", account.Id));

            context.Init(new ClaimsPrincipal(claimsIdentity));
        }
    }
}