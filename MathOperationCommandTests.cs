using Calculate.Commands;
using Calculate.Loggers;
using Moq;
using System.Collections.Generic;
using Xunit;

public class MathOperationCommandTests
{

    [Fact]
    public void CanExecute_ShouldReturnTrueForValidOperations()
    {
        // Arrange
        var command = new MathOperationCommand(null, null, new List<double>());

        // Act & Assert
        Assert.True(command.CanExecute("+"));
        Assert.True(command.CanExecute("-"));
        Assert.True(command.CanExecute("*"));
        Assert.True(command.CanExecute("/"));
        Assert.False(command.CanExecute("%"));
        Assert.False(command.CanExecute("random"));
    }
    [Fact]
    public void Execute_ShouldLogErrorWhenHistory()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPerformer = new Mock<Performer>();
        var results = new List<double>();
        var command = new MathOperationCommand(mockPerformer.Object, mockLogger.Object, results);

        // Act
        command.Execute("-");

        // Assert
        mockLogger.Verify(l => l.Log("History is empty, input number first"), Times.Once);
    }

    [Fact]
    public void Execute_ShouldLogErrorWhenHistoryIsEmpty()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPerformer = new Mock<Performer>();
        var results = new List<double>();
        var command = new MathOperationCommand(mockPerformer.Object, mockLogger.Object, results);

        // Act
        command.Execute("+");

        // Assert
        mockLogger.Verify(l => l.Log("History is empty, input number first"), Times.Once);
    }

    delegate void PerformDelegate(ref double currentResult, string operation, List<double> results);
}