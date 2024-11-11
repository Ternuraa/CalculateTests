
using Calculate.Common;
using Xunit;

public class GenericMathTests
{
    [Theory]
    [InlineData(5, 3, "+", 8)]
    [InlineData(5, 3, "-", 2)]
    [InlineData(5, 3, "*", 15)]
    [InlineData(9, 3, "/", 3)]
    public void GenericMath_ShouldPerformCorrectOperation(int num1, int num2, string operation, int expectedResult)
    {
        // Arrange
        GenericMath genericMath = new GenericMath();

        // Act
        var result = genericMath.PerformOperation(num1, num2, operation);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(5, 3, "%")]
    public void GenericMath_UnsupportedOperation_ShouldThrowException(int num1, int num2, string operation)
    {
        // Arrange
        GenericMath genericMath = new GenericMath();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => genericMath.PerformOperation(num1, num2, operation));
    }
}
