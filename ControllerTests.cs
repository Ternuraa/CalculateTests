using Calculate.Common;
using Calculate.Commands;
using Calculate.Loggers;
using Moq;
using Xunit;

public class ControllerTests
{
    [Fact]
    public void Controller_ShouldLogWelcomeMessageOnStart()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockCommand = new Mock<ICommand>();
        var mockUserInput = new Mock<IUserInput>();

        mockCommand.Setup(c => c.CanExecute(It.IsAny<string>())).Returns(false);
        mockUserInput.Setup(ui => ui.ReadLine()).Returns("exit");

        var controller = new Controller(new List<ICommand> { mockCommand.Object }, mockLogger.Object, mockUserInput.Object);

        // Act
        controller.Run();

        // Assert
        mockLogger.Verify(l => l.Log("Welcome to the Calculator!"), Times.Once);
    }
}
