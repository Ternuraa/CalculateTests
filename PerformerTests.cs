using Calculate.Common;
using Calculate.Loggers;
using Moq;
using Xunit;
using System.Collections.Generic;

public class PerformerTests
{
    [Fact]
    public void Performer_ShouldLogErrorWhenNoResults()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var genericMath = new GenericMath();
        var performer = new Performer(mockLogger.Object, genericMath);
        double currentResult = 0;
        var results = new List<double>();  // Пустой список результатов

        // Act
        performer.Perform(ref currentResult, "+", results);

        // Assert
        mockLogger.Verify(l => l.Log("No previous results to operate on."), Times.Once);
    }
}