using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo7 : ISolver
{
    public string SolvePart1(string[] lines)
    {
        var res = RunManifold(lines);
        // Implementation for part 1
        return res.splits.ToString();
    }
    public string SolvePart2(string[] lines)
    {
        var res = RunManifold(lines);
        // Implementation for part 1
        return res.timelines.ToString();
    }

    public (int splits, long timelines) RunManifold(string[] input)
    {
        // Dynamic programming over the grid:
        //
        // Each cell in row i depends only on the values from row i-1 directly above it.
        // We propagate a vector of "timeline counts" downward row by row instead of
        // keeping a full 2D DP table, which reduces memory to O(columns).
        //
        // At forks ('^'), a timeline splits into left/right branches, and we count a
        // "split" whenever an active timeline actually forks (>0 incoming paths).

        var lines = input.Select(line => line.ToCharArray()).ToArray();
        var crow = lines.Length;
        var ccol = lines[0].Length;
        var splits = 0;
        var timelines = new long[ccol];

        for (int irow = 0; irow < crow; irow++)
        {
            var nextTimelines = new long[ccol];
            for (var icol = 0; icol < ccol; icol++)
            {
                if (lines[irow][icol] == 'S')
                {
                    nextTimelines[icol] = 1;
                }
                else if (lines[irow][icol] == '^')
                {
                    splits += timelines[icol] > 0 ? 1 : 0;
                    nextTimelines[icol - 1] += timelines[icol];
                    nextTimelines[icol + 1] += timelines[icol];
                }
                else
                {
                    nextTimelines[icol] += timelines[icol];
                }
            }
            timelines = nextTimelines;
        }
        return (splits, timelines.Sum());
    }
}