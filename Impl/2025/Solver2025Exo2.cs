using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo2 : ISolver
{
    public string SolvePart1(string[] lines)
    {
        var elts = ParseData(lines);

        List<long> invalidIds = new();
        foreach (var elt in elts)
        {
            for (var i = elt.Start; i <= elt.End; i++)
            {
                var nb = i.ToString();

                var nbLength = nb.Length;
                if (nbLength % 2 != 0)
                {
                    continue;
                }
                var part1 = nb[..(nbLength / 2)];
                var part2 = nb[^(nbLength / 2)..];
                if (part1 == part2)
                {
                    invalidIds.Add(i);
                }
            }
        }

        return invalidIds.Sum().ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var elts = ParseData(lines);
        List<long> invalidIds = new();
        foreach (var elt in elts)
        {
            for (var i = elt.Start; i <= elt.End; i++)
            {

                var nb = i.ToString();
                var nbLength = nb.Length;

                for (int seqLength = 1; seqLength <= nbLength / 2; seqLength++)
                {
                    if (nbLength % seqLength != 0)
                    {
                        continue;
                    }

                    var sequence = nb[..seqLength];
                    var repetitions = nbLength / seqLength;

                    var isRepeated = true;
                    for (int rep = 1; rep < repetitions; rep++)
                    {
                        var start = rep * seqLength;
                        var currentPart = nb.Substring(start, seqLength);

                        if (currentPart != sequence)
                        {
                            isRepeated = false;
                            break;
                        }
                    }

                    if (isRepeated && repetitions >= 2)
                    {
                        invalidIds.Add(i);;
                        break;
                    }
                }
            }
        }
        return invalidIds.Sum().ToString();
        
    }

    private static List<(long Start, long End)> ParseData(string[] lines)
    {
        var data = lines[0];
        var elts = data.Split(',')
            .Select(e =>
            {
                var parts = e.Split('-');
                return (Start: long.Parse(parts[0]), End: long.Parse(parts[1]));
            }).ToList();
        return elts;
    }
}