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

        foreach (var line in lines)
        {
            var calibrationValue = string.Empty;
            char lastValue = 'a';

            foreach (var character in line)
            {
                if(Char.IsNumber(character))
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