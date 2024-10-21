namespace StringCalculator;

using global::StringCalculator.Helpers;

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var inputParser = new InputParser();
        var delimiters = inputParser.GetDelimiter(numbers);
        var numbersString = inputParser.GetNumbersString(numbers);
        var numbersList = inputParser.ParseStringToIntegerList(numbersString, delimiters);

        var result = 0;
        foreach (var addend in numbersList)
        {
            result += addend;
        }

        return result;
    }
}
