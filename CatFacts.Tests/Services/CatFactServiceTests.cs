using CatFacts.Api.Clients;
using CatFacts.Api.Models;
using CatFacts.Api.Services;
using Moq;

namespace CatFacts.Tests.Services;

public class CatFactServiceTests
{
    [Fact]
    public async Task GetAndSaveFactAsync_ShouldReturnFactAndSaveIt()
    {
        // Arrange
        var expectedFact = new CatFact("Cats sleep a lot.", 17);
        var catFactClientMock = new Mock<ICatFactClient>();
        var factFileWriterMock = new Mock<IFactFileWriter>();

        catFactClientMock
            .Setup(x => x.GetFactAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedFact);

        var service = new CatFactService(
            catFactClientMock.Object,
            factFileWriterMock.Object);

        // Act
        var result = await service.GetAndSaveFactAsync(
            CancellationToken.None);

        // Assert
        Assert.Equal(expectedFact, result);

        catFactClientMock.Verify(
            x => x.GetFactAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        factFileWriterMock.Verify(
            x => x.AppendAsync(
                expectedFact,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
