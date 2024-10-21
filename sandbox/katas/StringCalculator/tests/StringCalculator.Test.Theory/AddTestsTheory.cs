namespace StringCalculator.Test.Theory;

public class AddTestsTheory
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("1", 1)]
    [InlineData("2,4", 6)]
    [InlineData("1,10,100,1,1,1,1", 115)]

    //Metoda_Scenar_Vysledek
    public void Add_UnknownNumberOfIntegers_ReturnsSum(string numbers, int expectedResult)
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        var result = calculator.Add(numbers);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
