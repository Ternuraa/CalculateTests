using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Calculate.Loggers;
using Calculate.Storage;
using Moq;
using Xunit;

public class FileStorageTests
{
    // Тестирование метода Save
    [Fact]
    public async Task Save_ShouldWriteDataToFile()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var tempFilePath = Path.GetTempFileName();
        var storage = new FileStorage(mockLogger.Object, tempFilePath);

        var data = new List<double> { 1.0, 2.0, 3.0 };

        // Act
        await storage.Save(data);

        // Assert
        var writtenData = await File.ReadAllTextAsync(tempFilePath);
        var deserializedData = JsonSerializer.Deserialize<List<double>>(writtenData);
        Assert.Equal(data, deserializedData);

        // Clean up
        File.Delete(tempFilePath);
    }

    // Тестирование метода Load
    [Fact]
    public async Task Load_ShouldReturnDataFromFile()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var tempFilePath = Path.GetTempFileName();
        var storage = new FileStorage(mockLogger.Object, tempFilePath);

        var data = new List<double> { 1.0, 2.0, 3.0 };
        var jsonData = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(tempFilePath, jsonData);

        // Act
        var loadedData = await storage.Load();

        // Assert
        Assert.Equal(data, loadedData);

        // Clean up
        File.Delete(tempFilePath);
    }

    // Тестирование обработки ошибки при загрузке
    [Fact]
    public async Task Load_FileNotFound_ShouldLogErrorAndReturnEmptyList()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var nonexistentFilePath = "nonexistentfile.json";
        var storage = new FileStorage(mockLogger.Object, nonexistentFilePath);

        // Act
        var result = await storage.Load();

        // Assert
        Assert.Empty(result);
        mockLogger.Verify(logger => logger.Log("File not found."), Times.Once);
    }

    // Тестирование обработки ошибки при сохранении
    [Fact]
    public async Task Save_IOException_ShouldLogError()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var readOnlyFilePath = Path.GetTempFileName();
        File.WriteAllText(readOnlyFilePath, "Initial Content");
        File.SetAttributes(readOnlyFilePath, FileAttributes.ReadOnly);

        var storage = new FileStorage(mockLogger.Object, readOnlyFilePath);
        var data = new List<double> { 1.0, 2.0, 3.0 };

        // Act
        await storage.Save(data);

        // Assert
        mockLogger.Verify(logger => logger.Log(It.Is<string>(s => s.Contains("An error occurred while saving the list asynchronously"))), Times.Once);

        // Clean up
        File.SetAttributes(readOnlyFilePath, FileAttributes.Normal);
        File.Delete(readOnlyFilePath);
    }
}
