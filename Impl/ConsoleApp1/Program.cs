using System.Diagnostics;

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
        var lines = File.ReadAllLines(@"../../../../../2023/Exo08/input.txt");

        var sw = Stopwatch.StartNew();

        var directions = lines[0];

        // var nodeMaps = new Dictionary<string, string[]>();

        var nodeMaps = lines[2..]
            .Select(l =>
            {
                var parts = l.Split("=", StringSplitOptions.TrimEntries
                                         | StringSplitOptions.RemoveEmptyEntries);

                var nodes = parts[1]
                    .Replace("(", "")
                    .Replace(")", "").Split(",", StringSplitOptions.TrimEntries
                                                 | StringSplitOptions.RemoveEmptyEntries);

                return (parts[0], nodes);
            })
            .ToDictionary(p => p.Item1, p => p.nodes);

        var part1 = 0;
        var currentPoint = "AAA";
        var positionDirection = 0;
        // while (currentPoint != "ZZZ")
        // {
        //     var nextDirection = directions[positionDirection] == 'R' ? 1 : 0;
        //     part1++;
        //     currentPoint = nodeMaps[currentPoint][nextDirection];
        //     positionDirection = (positionDirection + 1) % directions.Length;
        // }
        //
        // Console.WriteLine($"Part 1: {part1}, took {sw.Elapsed}");
        sw.Restart();
        long part2 = 0;
        positionDirection = 0;
        var currentPoints = nodeMaps.Keys.Where(k => k.EndsWith("A")).ToList();
        while (!currentPoints.All(p => p.EndsWith("Z")))
        {
            var nextDirection = directions[positionDirection] == 'R' ? 1 : 0;
            part2++;
            currentPoints = currentPoints.Select(p => nodeMaps[p][nextDirection]).ToList();
            positionDirection = (positionDirection + 1) % directions.Length;
        }

        Console.WriteLine($"Part 2: {part2}, took {sw.Elapsed}");
    }
}