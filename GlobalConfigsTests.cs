
using Calculate.Common;
using Calculate.Loggers;
using Moq;
using Xunit;

public class GlobalConfigsTests
{
    [Fact]
    public void GlobalConfigs_ShouldReturnCorrectLoggerAndPath()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        string path = "some/path";
        var configs = new GlobalConfigs(mockLogger.Object, path);

        // Act
        ILogger logger = configs.GetLogger();
        string storagePath = configs.GetStoragePath();

        // Assert
        Assert.Equal(mockLogger.Object, logger);
        Assert.Equal(path, storagePath);
    }
}
