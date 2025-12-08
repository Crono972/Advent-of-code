using AdventOfCode.Impl._2025;
using AdventOfCode.Shared;

var input = File.ReadAllLines(@"Input\2025\Exo08\input.txt");

var solver = new Solver2025Exo8();
var result = solver.SolvePart1(input);
Console.WriteLine("1st part: " + result);
result = solver.SolvePart2(input);
Console.WriteLine("2nd part: " + result);
Console.ReadLine();

public class Solver2025Exo8 : ISolver
{
    record Point(decimal x, decimal y, decimal z);

    public string SolvePart1(string[] lines)
    {
        // Apply 1000 steps of Kruskal's algorithm to the points using the specified
        // metric, then return the product of the sizes of the three largest components.
        var points = Parse(lines);
        var setOf = points.ToDictionary(p => p, p => new HashSet<Point>([p]));
        foreach (var (a, b) in GetOrderedPairs(points).Take(1000))
        {
            if (setOf[a] != setOf[b])
            {
                Connect(a, b, setOf);
            }
        }
        return setOf.Values.Distinct()
            .OrderByDescending(set => set.Count)
            .Take(3)
            .Aggregate(1, (a, b) => a * b.Count).ToString();
    }

    public string SolvePart2(string[] lines)
    {
        // Run Kruskal's algorithm on all points and return the product of the
        // x-coordinates of the last edge added to the spanning tree.
        var points = Parse(lines);
        var componentCount = points.Length;
        var setOf = points.ToDictionary(p => p, p => new HashSet<Point>([p]));
        var res = 0m;
        foreach (var (a, b) in GetOrderedPairs(points).TakeWhile(_ => componentCount > 1))
        {
            if (setOf[a] != setOf[b])
            {
                Connect(a, b, setOf);
                res = a.x * b.x;
                componentCount--;
            }
        }
        return res.ToString();
    }

    void Connect(Point a, Point b, Dictionary<Point, HashSet<Point>> setOf)
    {
        setOf[a].UnionWith(setOf[b]);
        foreach (var p in setOf[b])
        {
            setOf[p] = setOf[a];
        }
    }

    IEnumerable<(Point a, Point b)> GetOrderedPairs(Point[] points) =>
        from a in points
        from b in points
        where (a.x, a.y, a.z).CompareTo((b.x, b.y, b.z)) < 0
        orderby Metric(a, b)
        select (a, b);

    decimal Metric(Point a, Point b) =>
        (a.x - b.x) * (a.x - b.x) +
        (a.y - b.y) * (a.y - b.y) +
        (a.z - b.z) * (a.z - b.z);

    Point[] Parse(string[] lines) => (lines
        .Select(line => line.Split(",").Select(int.Parse).ToArray() )
        .Select(parts => new Point(parts[0], parts[1], parts[2]))).ToArray();
}