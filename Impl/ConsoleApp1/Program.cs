using System.IO.MemoryMappedFiles;

namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        Solve();
    }

    public static void Solve()
    {
        var lines = File.ReadAllLines(@"../../../../../2023/Exo01/input.txt");

        int sum = 0;

        var args = new Dictionary<string, char>(StringComparer.OrdinalIgnoreCase)
        {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' }
        };

        var argsByLenghts = args.ToLookup(a => a.Key.Length);

        foreach (var line in lines)
        {
            var calibrationValue = string.Empty;
            char lastValue = 'a';

            for (int i = 0; i < line.Length; i++)
            {
                var character = line[i];
                foreach (var arg in argsByLenghts)
                {
                    var left = line.Substring(i, Math.Min(line.Length - i, arg.Key));
                    if (left.Length == arg.Key)
                    {
                        foreach (var pair in arg)
                        {
                            if (left.Equals(pair.Key))
                            {
                                character = pair.Value;
                            }
                        }
                    }
                }

                if (char.IsNumber(character))
                {
                    if (lastValue == 'a')
                    {
                        calibrationValue += character;
                    }
                    lastValue = character;
                }

            }

            calibrationValue += lastValue;
            sum += int.Parse(calibrationValue);
        }

        Console.WriteLine(sum);
    }

}