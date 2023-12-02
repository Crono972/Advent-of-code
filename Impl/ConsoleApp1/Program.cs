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

        foreach (var line in lines)
        {
            var part = line.Split(':');
            //var gameNumber = int.Parse(part[0].Replace("Game", ""));
            var configuration = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            var games = part[1].Split(';');
            foreach (var game in games)
            {
                var cubes = game.Split(",");
                foreach (var cube in cubes)
                {
                    var cubeInfo = cube.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var number = int.Parse(cubeInfo[0]);
                    var cubeName = cubeInfo[1];
                    if (configuration.ContainsKey(cubeName))
                    {
                        configuration[cubeName] = Math.Max(number, configuration[cubeName]);
                    }
                    else
                    {
                        configuration[cubeName] = number;
                    }
                }
            }

            var power = 1;
            foreach (var arg in configuration)
            {
                power *= arg.Value;
            }

            sum += power;
        }

        Console.WriteLine(sum);
    }

}