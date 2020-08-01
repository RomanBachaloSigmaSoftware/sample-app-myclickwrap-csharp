using System;
using System.Net;
using System.Security.Authentication;
using DocuSign.MyClick.TermsAndConditions.Domain;
using DocuSign.MyClick.TermsAndConditions.Exceptions;
using DocuSign.MyClick.TermsAndConditions.Services;
using FluentAssertions;
using Moq;
using RestSharp;
using Xunit;

namespace DocuSign.MyClick.TermsAndConditions.UnitTests
{
    public class ClickWrapServiceTests
    {
        public ClickWrapServiceTests()
        {
            var docuSignApiProvider = new Mock<IDocuSignApiProvider>();
            _restClient = new Mock<IRestClient>();

            docuSignApiProvider
                .SetupGet(c => c.DocuSignClickApiRestClient)
                .Returns(_restClient.Object);

            _sut = new ClickWrapService(docuSignApiProvider.Object);
        }

        private const string AccountId = "1";
        private const string UserId = "2";

        private readonly ClickWrapService _sut;
        private readonly Mock<IRestClient> _restClient;

        [Fact]
        public void GetClickWrap_WhenAccountIdIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            //Act
            //Assert 
            Assert.Throws<ArgumentNullException>(
                () => _sut.GetClickWrap(null, UserId));
        }

        [Fact]
        public void GetClickWrap_WhenClickWrapExists_ReturnsCorrectResponse()
        {
            //Arrange
            _restClient
                .Setup(x => x.Execute(It.IsAny<IRestRequest>(), Method.GET))
                .Returns(new RestResponse
                {
                    Content =
                        "{\"clickwraps\":[{\"clickwrapId\":\"1\",\"clickwrapName\":\"TermsAndConditions\",\"status\":\"active\"}]}",
                    StatusCode = HttpStatusCode.OK
                });

            //Act
            ClickWrap response = _sut.GetClickWrap(AccountId, UserId);

            //Assert 
            response.Should().BeEquivalentTo(
                new ClickWrap
                {
                    ClickwrapId = "1",
                    ClickwrapName = "TermsAndConditions",
                    Status = "active"
                });
        }

        [Fact]
        public void GetClickWrap_WhenClickWrapNotExists_ThrowsClickWrapNotFoundException()
        {
            //Arrange
            _restClient
                .Setup(x => x.Execute(It.IsAny<IRestRequest>(), Method.GET))
                .Returns(new RestResponse
                {
                    Content = "{\"clickwraps\":[]}",
                    StatusCode = HttpStatusCode.OK
                });

            //Act
            //Assert 
            Assert.Throws<ClickWrapNotFoundException>(
                () => _sut.GetClickWrap(AccountId, UserId));
        }

        [Fact]
        public void GetClickWrap_WhenClickWrapReturnsBadRequest_ThrowsInvalidOperationException()
        {
            //Arrange
            _restClient
                .Setup(x => x.Execute(It.IsAny<IRestRequest>(), Method.GET))
                .Returns(new RestResponse
                {
                    Content = "",
                    StatusCode = HttpStatusCode.BadRequest
                });

            //Act
            //Assert 
            Assert.Throws<InvalidOperationException>(
                () => _sut.GetClickWrap(AccountId, UserId));
        }

        [Fact]
        public void GetClickWrap_WhenClickWrapReturnsUnauthorized_ThrowsAuthenticationException()
        {
            //Arrange
            _restClient
                .Setup(x => x.Execute(It.IsAny<IRestRequest>(), Method.GET))
                .Returns(new RestResponse
                {
                    Content = "",
                    StatusCode = HttpStatusCode.Unauthorized
                });

            //Act
            //Assert 
            Assert.Throws<AuthenticationException>(
                () => _sut.GetClickWrap(AccountId, UserId));
        }

        [Fact]
        public void GetClickWrap_WhenUserIdIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            //Act
            //Assert 
            Assert.Throws<ArgumentNullException>(
                () => _sut.GetClickWrap(AccountId, null));
        }
    }
}