using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo6 : ISolver
{
    record Problem(char op, IEnumerable<long> nums);

    public string SolvePart1(string[] lines)
    {
        var problems = ParseBlocks(lines).Select(rows => new Problem(rows.Last()[0], rows[..^1].Select(long.Parse)));
        var result = Solve(problems);
        return result.ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var problems = ParseBlocks(lines)
            .Select(Transpose)
            .Select(cols => new Problem(cols.First()[^1],
                cols.Select(col => long.Parse(col[..^1]))));
        var result = Solve(problems);
        return result.ToString();
    }

    long Solve(IEnumerable<Problem> problems)
    {
        var res = 0L;
        foreach (var problem in problems)
        {
            if (problem.op == '+')
            {
                res += problem.nums.Sum();
            }
            else
            {
                res += problem.nums.Aggregate(1L, (a, b) => a * b);
            }
        }
        return res;
    }


    IEnumerable<string[]> ParseBlocks(string[] lines)
    {
        int ccol = lines[0].Length;
        var blockStart = 0;
        for (int icol = 0; icol < ccol; icol++)
        {
            if (GetColumn(lines, icol).Trim() == "")
            {
                yield return GetBlock(lines, blockStart, icol);
                blockStart = icol + 1;
            }
        }
        yield return GetBlock(lines, blockStart, ccol);
    }

    string[] GetBlock(string[] lines, int icolFrom, int icolTo) => (
        from line in lines
        select line[icolFrom..icolTo]
    ).ToArray();

    string[] Transpose(string[] lines) => (
        from icol in Enumerable.Range(0, lines[0].Length)
        select GetColumn(lines, icol)
    ).ToArray();

    string GetColumn(string[] lines, int icol) =>
        string.Join("", from line in lines select line[icol]);
}