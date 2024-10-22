namespace StringCalculator.Test;

public class AddTests
{
    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_SingleNumber_ReturnsSameInteger()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "1";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void Add_MultipleNumbers_NoExceptionThrown()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "1,2,3,4";

        //Act
        var exception = Record.Exception(() => calculator.Add(numbers));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "1,2";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_InputWithWhitespace_ReturmsCorrectSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "\n1,\n2\n\n\n";

        //Act
        var exception = Record.Exception(() => calculator.Add(numbers));
        var result = calculator.Add(numbers);

        //Assert
        Assert.Null(exception);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_WithCustomDelimiter_ReturnsSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "//;1;2";

        //Act
        var exception = Record.Exception(() => calculator.Add(numbers));
        var result = calculator.Add(numbers);

        //Assert
        Assert.Null(exception);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_WithNegativeNumbers_ThrowsCustomException()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "2,-4,3,-5";

        //Act
        var exception = Record.Exception(() => calculator.Add(numbers));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal("Negatives are not allowed: -4,-5", exception.Message);
    }

    [Fact]
    public void Add_NumbersGreaterThen1000_AreIgnored()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "1001,2";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Add_CustomDelimiterOfAnyLength_ReturnsSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "//[|||]\n1|||2|||3";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_MultipleDelimiters_ReturnsSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        var numbers = "//[|][%][#]\n1|2%3#1";

        //Act
        var result = calculator.Add(numbers);

        //Assert
        Assert.Equal(7, result);
    }
}
