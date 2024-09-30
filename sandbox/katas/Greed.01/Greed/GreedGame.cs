public class GreedGame
{
    public GreedGame()
    {
    }

    public List<int> ReturnDices()
    {
        int diceNumber = 5;
        int diceCounter = 1;
        var diceList = new List<int>();
        for (int i = 1; diceCounter <= diceNumber; diceCounter++)
        {
            Random random = new();
            int diceValue = random.Next(1, 7);
            diceList.Add(diceValue);
        }
        Console.WriteLine(diceList);
        return diceList;
    }

    public Dictionary<int, int> CountNumbers(List<int> diceList)
    {
        Dictionary<int, int> results = new();
        for (int i = 1; i <= 6; i++)
        {
            int onesCount = diceList.Count(die => die == i);
            results.Add(i, onesCount);
        }

        return results;
    }

    public int CountScore(Dictionary<int, int> results)
    {
        int score = 0;
        foreach (KeyValuePair<int, int> number in results)
        {
            switch (number.Key)
            {
                case 1:
                    score += (number.Value / 3) * 1000 + (number.Value % 3) * 100;
                    continue;
                case 2:
                    score += (number.Value / 3) * 200;
                    continue;
                case 3:
                    score += (number.Value / 3) * 300;
                    continue;
                case 4:
                    score += (number.Value / 3) * 400;
                    continue;
                case 5:
                    score += (number.Value / 3) * 500 + (number.Value % 3) * 50;
                    continue;
                case 6:
                    score += (number.Value / 3) * 600;
                    break;
            }
        }

        return score;
    }
}
