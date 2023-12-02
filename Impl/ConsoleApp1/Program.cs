using System.IO.MemoryMappedFiles;
using System.Text.RegularExpressions;

namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        Solve();
    }

    public static void Solve()
    {
        var lines = File.ReadAllLines(@"../../../../../2023/Exo02/input.txt");

        int sum = 0;

        var configuration = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        foreach (var line in lines)
        {
            var part = line.Split(':');
            var gameNumber = int.Parse(part[0].Replace("Game", ""));
            bool impossibleGame = false;
            var games = part[1].Split(';');
            foreach (var game in games)
            {
                if (impossibleGame)
                {
                    break;
                }
                var cubes = game.Split(",");
                foreach (var cube in cubes)
                {
                    var cubeInfo = cube.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var number = int.Parse(cubeInfo[0]);
                    var cubeName = cubeInfo[1];
                    if (configuration[cubeName] < number)
                    {
                        impossibleGame = true;
                        break;
                    }
                }
            }

            if (!impossibleGame)
            {
                sum += gameNumber;
            }
        }

        Console.WriteLine(sum);
    }

}