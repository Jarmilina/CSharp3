// namespace ToDoList.Test;

// public class UnitTest1
// {
//     [Theory]
//     [InlineData(10, 2, 5)]
//     [InlineData(2, 2, 1)]
//     //Metoda_Scenar_Vysledek
//     public void Divide_Without_Remainder_Suceeds(int dividend, int divisor, int expectedResult)
//     {
//         // Arrange
//         var calculator = new Calculator();

//         // Act
//         var result = calculator.Divide(10, 5);

//         // Assert
//         Assert.Equal(2, result);

//     }

//     [Fact]
//     public void DevideInt_ByZero_ThrowsException()
//     {
//         // Arrange
//         var calculator = new Calculator();

//         // Act
//         var divideAction = () => calculator.Divide(10, 0);

//         // Assert
//         Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));

//     }

//     [Fact]
//     public void Calculator_IsNull()
//     {
//         // Arrange
//         var calculator = new Calculator();

//         // Act
//         var divideAction = () => calculator.Divide(10, 0);

//         // Assert
//         Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));

//     }
// }

// public class Calculator
// {
//     public int Divide(int dividend, int divisor)
//     {
//         return dividend / divisor;
//     }
// }
