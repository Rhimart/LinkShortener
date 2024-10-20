using LinkShortener.Application;
using LinkShortener.Domain.Constants;
using LinkShortener.Persistence.Database.ShortenLink;
using LinkShortener.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.TestSuite.UnitTests.Services
{
    [TestClass]
    public class LinkShortenerServiceTests
    {
        private Mock<IShortenedLinkRepo> _mockRepo;
        private LinkShortenerService _linkShortenerService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IShortenedLinkRepo>();
            _linkShortenerService = new LinkShortenerService(_mockRepo.Object);
        }

        [TestMethod]
        public void ShortenLink_ShouldReturnShortenedLink()
        {
            // Arrange
            string url = "https://www.example.com";
            var expectedShortenedLink = new ShortenedLink
            {
                OriginalUrl = url,
                ShortUrl = $"{Constants.linkPrefix}/{Guid.NewGuid().ToString().Substring(0, 8)}"
            };

            // Act
            var result = _linkShortenerService.ShortenLink(url);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(url, result.OriginalUrl);
            Assert.IsTrue(result.ShortUrl.StartsWith(Constants.linkPrefix));
            _mockRepo.Verify(repo => repo.AddShortenedLink(It.IsAny<ShortenedLink>()), Times.Once);
        }

        [TestMethod]
        public void GetShortenedLinks_ShouldReturnListOfShortenedLinks()
        {
            // Arrange
            var expectedLinks = new List<ShortenedLink>
            {
                new ShortenedLink { Id = 1, OriginalUrl = "https://www.example1.com", ShortUrl = "short1" },
                new ShortenedLink { Id = 2, OriginalUrl = "https://www.example2.com", ShortUrl = "short2" }
            };

            _mockRepo.Setup(repo => repo.GetAllShortenedLink()).Returns(expectedLinks);

            // Act
            var result = _linkShortenerService.GetShortenedLinks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEqual(expectedLinks, result);
            _mockRepo.Verify(repo => repo.GetAllShortenedLink(), Times.Once);
        }
    }
}
