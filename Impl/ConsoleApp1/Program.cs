using System.Text.RegularExpressions;

namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        Solve();
        Console.ReadKey();
    }

    public static void Solve()
    {
        var lines = File.ReadAllLines(@"../../../../../2023/Exo04/input.txt");
        var scratchCard = Enumerable.Repeat(1, lines.Length).ToArray();
        double part1 = 0;
        var regex = new Regex("Card\\s+(?<CardNumber>\\d+):(?:\\s+(?<WinningNumber>\\d+))+ \\|(?:\\s+(?<Number>\\d+))+");
        foreach (var line in lines)
        {
            var m = regex.Matches(line).First();
            if (m.Success)
            {
                var cardNumber = int.Parse(m.Groups["CardNumber"].Value);
                var nbOfCard = scratchCard[cardNumber - 1];
                var winningNumber = m.Groups["WinningNumber"]
                    .Captures.Select(w => int.Parse(w.Value)).ToHashSet();

                var numbers = m.Groups["Number"]
                    .Captures.Select(w => int.Parse(w.Value)).ToList();

                var wonNumbers = numbers.Where(n => winningNumber.Contains(n)).ToList();

                if (wonNumbers.Any())
                {
                    int scratchedCardWon = cardNumber;
                    for (int i = 0; i < wonNumbers.Count; i++)
                    {
                        scratchCard[scratchedCardWon] += nbOfCard;
                        scratchedCardWon++;
                    }
                    var cardValue = Math.Pow(2, wonNumbers.Count - 1);
                    Console.WriteLine($"Card {cardNumber} worth {cardValue}");
                    part1 += cardValue;
                }
            }
        }

        var part2 = scratchCard.Sum();
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
}