using System;
using Xunit; // Убедитесь, что xUnit установлен
using Calculate.Operations; // Пространства имен для операций

public class OperationTests
{
    [Fact]
    public void AdditionOperation_ShouldReturnCorrectSum()
    {
        // Arrange
        IOperation operation = new AdditionOperation();

        // Act
        double result = operation.Execute(3, 5);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void SubtractionOperation_ShouldReturnCorrectDifference()
    {
        // Arrange
        IOperation operation = new SubtractionOperation();

        // Act
        double result = operation.Execute(10, 4);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void MultiplicationOperation_ShouldReturnCorrectProduct()
    {
        // Arrange
        IOperation operation = new MultiplicationOperation();

        // Act
        double result = operation.Execute(4, 2.5);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void DivisionOperation_ShouldReturnCorrectQuotient()
    {
        // Arrange
        IOperation operation = new DivisionOperation();

        // Act
        double result = operation.Execute(8, 2);

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void DivisionOperation_DivideByZero_ShouldThrowException()
    {
        // Arrange
        IOperation operation = new DivisionOperation();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => operation.Execute(8, 0));
    }
}
