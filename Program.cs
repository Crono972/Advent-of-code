using AdventOfCode.Impl._2025;
using AdventOfCode.Shared;

var input = File.ReadAllLines(@"Input/2025/Exo11/input.txt");

var solver = new Solver2025Exo11();
var result = solver.SolvePart1(input);
Console.WriteLine("1st part: " + result);
result = solver.SolvePart2(input);
Console.WriteLine("2nd part: " + result);
Console.ReadLine();

public class Solver2025Exo11 : ISolver
{
    public string SolvePart1(string[] lines)
    {
        var res = PathCount(Parse(lines), "you", "out", new Dictionary<string, long>());
        return res.ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var g = Parse(lines);
        var res= 
            PathCount(g, "svr", "fft", new Dictionary<string, long>()) *
            PathCount(g, "fft", "dac", new Dictionary<string, long>()) *
            PathCount(g, "dac", "out", new Dictionary<string, long>()) +

            PathCount(g, "svr", "dac", new Dictionary<string, long>()) *
            PathCount(g, "dac", "fft", new Dictionary<string, long>()) *
            PathCount(g, "fft", "out", new Dictionary<string, long>());
        return res.ToString();
    }
    
    long PathCount(
        Dictionary<string, string[]> g,
        string from, string to,
        Dictionary<string, long> cache
    ) {
        if (!cache.ContainsKey(from)) {
            if (from == to) {
                cache[from] = 1;
            } else {
                var res = 0L;
                foreach (var next in g.GetValueOrDefault(from) ?? []) {
                    res += PathCount(g, next, to, cache);
                }
                cache[from] = res;
            }
        }
        return cache[from];
    }
    Dictionary<string, string[]> Parse(string[] lines) => (
        from line in lines
        let parts = line.Split(" ")
        let frm = parts[0].TrimEnd(":").ToString()
        let to = parts[1..].ToArray()
        select new KeyValuePair<string, string[]>(frm, to)
    ).ToDictionary();

}