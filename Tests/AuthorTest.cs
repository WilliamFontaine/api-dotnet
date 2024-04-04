using API.Controllers;
using Business.Author;
using Entities.Library;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

[TestClass]
public class AuthorTest
{
    private AuthorsController _authorsController;
    private Mock<IAuthorService> _authorService;
    private Mock<ILogger<AuthorsController>> _logger;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorService = new Mock<IAuthorService>();
        _logger = new Mock<ILogger<AuthorsController>>();
        _authorsController = new AuthorsController(_authorService.Object, _logger.Object);
    }

    [TestMethod]
    public async Task Get_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var authors = new List<Author>
        {
            new() { Id = 1, Name = "Author 1" },
            new() { Id = 2, Name = "Author 2" }
        };
        _authorService.Setup(s => s.Get()).ReturnsAsync(authors);

        // Act
        var result = await _authorsController.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
}