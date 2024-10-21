namespace StringCalculator.Helpers
{
    public class InputParser
    {
        public List<string> GetDelimiter(string input)
        {
            List<string> delimiters = [];

            if (!input.StartsWith("//"))
            {
                delimiters.Add(",");
                return delimiters;
            }

            input = input[2..];

            if (!(input.StartsWith('[') && input.Contains(']')))
            {
                delimiters.Add(input[0].ToString());
                return delimiters;
            }

            while (input.StartsWith('[') && input.Contains(']'))
            {
                var delimiter = ExtractDelimiter(input);

                delimiters.Add(delimiter);
                input = TrimDelimiter(input);
            }

            return delimiters;
        }

        public string ExtractDelimiter(string input)
        {
            var startIndex = input.IndexOf('[');
            var endIndex = input.IndexOf(']');
            var delimiter = input[(startIndex + 1)..endIndex];

            return delimiter;
        }

        public string TrimDelimiter(string input)
        {
            var startIndex = input.IndexOf('[');
            var endIndex = input.IndexOf(']');

            return input.Remove(startIndex, endIndex + 1 - startIndex);
        }

        public string GetNumbersString(string input)
        {
            if (!input.StartsWith("//"))
            {
                return input;
            }

            if (input.Contains('[') && input.Contains(']'))
            {
                if (input.Count(c => c == '[') == 1)
                {
                    return input[(input.IndexOf(']') + 1)..];
                }
                else
                {
                    input = TrimDelimiter(input);
                    return GetNumbersString(input);
                }
            }

            return input[3..];
        }

        public List<int> ParseStringToIntegerList(string input, List<string> delimiters)
        {
            foreach (var delimiter in delimiters)
            {
                input = input.Replace(delimiter, ",");
            }

            var inputArray = input.Split(",");
            List<int> intList = [];
            List<int> negativeInts = [];

            try
            {
                foreach (var member in inputArray)
                {
                    var integer = int.Parse(member.Trim());

                    if (integer < 0)
                    {
                        negativeInts.Add(integer);
                    }

                    if (integer <= 1000)
                    {
                        intList.Add(integer);
                    }
                }
            }
            catch
            {
                throw new Exception($"{input} does not match correct format.");
            }

            if (negativeInts.Count > 0)
            {
                throw new Exception($"Negatives are not allowed: {string.Join(",", negativeInts)}");
            }

            return intList;
        }
    }
}
