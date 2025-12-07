using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo5 : ISolver
{
    public string SolvePart1(string[] lines)
    {
        var inputData = ParseInput(lines);
        var count = 0;
        foreach (var ingredient in inputData.ingredients)
        {
            if (inputData.freshRanges.Any(fresh => fresh.start <= ingredient && fresh.end >= ingredient))
            {
                count++;
            }
        }
        return count.ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var inputData = ParseInput(lines);
        long count = 0;
        var ranges = inputData.freshRanges.OrderBy(x => x.start).ToArray();
        for (var i = 0; i < ranges.Length - 1; i++)
        {
            if (ranges[i + 1].start <= ranges[i].end)
            {
                var end = Math.Max(ranges[i].end, ranges[i + 1].end);
                ranges[i] = (ranges[i].start, ranges[i + 1].start - 1);
                ranges[i + 1] = (ranges[i + 1].start, end);
            }
        }
        return ranges.Sum(range => range.end - range.start + 1).ToString();
    }

    private (IList<(long start, long end)> freshRanges, IList<long> ingredients) ParseInput(string[] lines)
    {
        var isAfterFreshRange = false;
        var freshRanges = new List<(long start, long end)>();
        var ingredients = new List<long>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                isAfterFreshRange = true;
                continue;
            }
            if (!isAfterFreshRange)
            {
                var parts = line.Split("-", StringSplitOptions.TrimEntries);
                var start = long.Parse(parts[0]);
                var end = long.Parse(parts[1]);
                freshRanges.Add((start, end));
            }
            else
            {
                var ingredient = long.Parse(line);
                ingredients.Add(ingredient);
            }
        }
        return (freshRanges, ingredients);
    }
}