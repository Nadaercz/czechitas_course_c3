namespace ToDoList.Test;

public class UnitTest1
{
    [Fact]
    public void Divide_WithoutRemainder_Success()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Divide(10, 5);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Divide_ByZero_Throw()
    {
        // Arrange
        var calculator = new Calculator();

        // Act && Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }

    [Theory]
    [InlineData(10, 3)]
    [InlineData(10, 4)]
    [InlineData(5, 2)]
    [InlineData(-5, -2)]
    [InlineData(-5, 2)]
    public void Divide_TakeTwoNumber_Success_Parametrized(int value1, int value2)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actualResult = calculator.Divide(value1, value2);
        var expectedResult = value1 / value2;

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

public class Calculator
{
    public int Divide(int dividend, int divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException();
        }
        return dividend / divisor;
    }
}
