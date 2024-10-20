
using LinkShortener.Api.Controllers;
using LinkShortener.Application;
using LinkShortener.Domain.Constants;
using LinkShortener.Domain.Model;
using LinkShortener.Domain.Request;
using LinkShortener.Persistence.Database.ShortenLink;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LinkShortener.TestSuites.UnitTests.Controllers
{
    [TestClass]
    public class LinkShortenerControllerTests
    {
        private Mock<ILinkShortenerService> _linkShortenerServiceMock;
        private LinkShortenerController _controller;
        private DefaultHttpContext _httpContext;

        [TestInitialize]
        public void Setup()
        {
            _linkShortenerServiceMock = new Mock<ILinkShortenerService>();
            _httpContext = new DefaultHttpContext();
            _controller = new LinkShortenerController(_linkShortenerServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContext
                }
            };
        }

        [TestMethod]
        public async Task ShortenLink_ShouldReturnOkResult_WithShortenedLink()
        {
            var request = new ShortenLinkRequest { url = "https://example.com" };
            var shortenedLink = new ShortenedLink
            {
                OriginalUrl = request.url,
                ShortUrl = "https://short.ly/abcd1234"
            };

            _linkShortenerServiceMock
                .Setup(s => s.ShortenLink(request.url))
                .Returns(shortenedLink);

            var expectedMessage = SysMsg.GetMsg(MsgCodes.LS_SUC_001_Success);

            var result = await _controller.ShortenLink(request);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResponseMessageSchema;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedMessage, response.Message);

            var returnedLink = response.Data as ShortenedLink;
            Assert.IsNotNull(returnedLink);
            Assert.AreEqual(shortenedLink.ShortUrl, returnedLink.ShortUrl);
            Assert.AreEqual(shortenedLink.OriginalUrl, returnedLink.OriginalUrl);

            _linkShortenerServiceMock.Verify(s => s.ShortenLink(request.url), Times.Once);
        }

        [TestMethod]
        public async Task GetShortenedLinks_ShouldReturnOkResult_WithListOfShortenedLinks()
        {
            var shortenedLinks = new List<ShortenedLink>
            {
                new ShortenedLink { OriginalUrl = "https://example1.com", ShortUrl = "https://short.ly/abcd1234" },
                new ShortenedLink { OriginalUrl = "https://example2.com", ShortUrl = "https://short.ly/efgh5678" }
            };

            _linkShortenerServiceMock
                .Setup(s => s.GetShortenedLinks())
                .Returns(shortenedLinks);

            var expectedMessage = SysMsg.GetMsg(MsgCodes.LS_SUC_001_Success);

            var result = await _controller.GetShortenedLinks();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResponseMessageSchema;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedMessage, response.Message);

            var returnedLinks = response.Data as List<ShortenedLink>;
            Assert.IsNotNull(returnedLinks);
            Assert.AreEqual(2, returnedLinks.Count);
            Assert.AreEqual(shortenedLinks[0].ShortUrl, returnedLinks[0].ShortUrl);
            Assert.AreEqual(shortenedLinks[1].ShortUrl, returnedLinks[1].ShortUrl);

            _linkShortenerServiceMock.Verify(s => s.GetShortenedLinks(), Times.Once);
        }
    }
}