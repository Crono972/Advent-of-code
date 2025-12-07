using System.Collections.Immutable;
using System.Numerics;
using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo4 : ISolver
{
    Complex Up = -Complex.ImaginaryOne;
    Complex Down = Complex.ImaginaryOne;
    Complex Left = -1;
    Complex Right = 1;

    public string SolvePart1(string[] lines)
    {
        var map = GetMap(lines);
        int count = 0;
        foreach (var point in map.Keys)
        {
            if (IsMovableRoll(map, point))
            {
                count++;
            };
        }
        return count.ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var map = GetMap(lines);
        int count = 0;

        var currentMap = map;
        HashSet<Complex> removed = [];
        do
        {
            foreach (var point in removed)
            {
                currentMap = currentMap.SetItem(point, '.');
            }

            removed.Clear();
            foreach (var point in map.Keys)
            {
                if (IsMovableRoll(currentMap, point))
                {
                    count++;
                    removed.Add(point);
                }
            }
        } while (removed.Any());

        return count.ToString();
    }

    private bool IsMovableRoll(ImmutableDictionary<Complex, char> map, Complex point)
    {
        if (map[point] == '.')
        {
            return false;
        }

        var rolls = 0;
        foreach (var direction in new[] { Up, Down, Left, Right, Up + Left, Up + Right, Down + Left, Down + Right })
        {
            var current = point + direction;
            if (!map.TryGetValue(current, out var value))
            {
                continue;
            }

            if (value == '@')
            {
                rolls++;
                if (rolls >= 4)
                {
                    break;
                }
            }
        }

        return rolls < 4;
    }

    private static ImmutableDictionary<Complex, char> GetMap(string[] lines)
    {
        return (
            from y in Enumerable.Range(0, lines.Length)
            from x in Enumerable.Range(0, lines[0].Length)
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, lines[y][x])
        ).ToImmutableDictionary();
    }
}