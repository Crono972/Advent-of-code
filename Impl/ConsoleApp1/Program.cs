var input = File.ReadAllLines(@"../../../../../2023/Exo09/input.txt");
var watch = System.Diagnostics.Stopwatch.StartNew();

var values = input
    .Select(s => s.Split(' ').Select(int.Parse).ToArray())
    .ToArray();
    
Console.WriteLine(Part1(values));
Console.WriteLine(Part2(values));

static int Part1(int[][] values)
{
    var diffs = values.Select(GetDiffs).ToList();
    return diffs
        .Sum(aa => aa.Sum(a => a[^1]));
}

static int Part2(int[][] values) =>
    values.Select(GetDiffs)
        .Sum(aa => aa.Reverse().Aggregate(0, (a, v) => v[0] - a));

static IEnumerable<int[]> GetDiffs(int[] a)
{
    yield return a;
    while (a.Any(v => v != 0))
        yield return a = a[1..].Select((v, i) => v - a[i]).ToArray();
}