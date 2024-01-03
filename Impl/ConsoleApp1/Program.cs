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
        var lines = File.ReadAllLines(@"../../../../../2023/Exo06/input.txt");
        var times = lines[0].Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
        
        var distances = lines[1].Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();

        var sw = Stopwatch.StartNew();
        int part1Resultat = 1;
        for (int i = 0; i < times.Length; i++)
        {
            var courseTime = times[i];
            var distanceToBeat = distances[i];
            int wayToWinRace = 0;
            for (int t = 0; t < courseTime; t++)
            {
                var acheviableDistance = t * (courseTime - t);
                if (acheviableDistance > distanceToBeat)
                {
                    wayToWinRace++;
                }
            }

            part1Resultat *= wayToWinRace;
        }

        Console.WriteLine($"Part1 : {part1Resultat} , took {sw.Elapsed}");
        sw.Restart();
        var part2Time = long.Parse(lines[0].Split(':')[1]
            .Replace(" ", ""));
        var part2Distance = long.Parse(lines[1].Split(':')[1]
            .Replace(" ", ""));

        long part2Resultat = 0;
        for (int i = 0; i < part2Time; i++)
        {
            var acheviableDistance = i * (part2Time - i);
            if (acheviableDistance > part2Distance)
            {
                part2Resultat++;
            }
        }
        Console.WriteLine($"Part2 : {part2Resultat} , took {sw.Elapsed}");

    }
}