using System.Collections.Frozen;

var lines = File.ReadAllText(@"../../../../../2024/Exo23/input.txt");

Console.WriteLine($"PartOne : {PartOne(lines)}");
Console.WriteLine($"PartTwo : {PartTwo(lines)}");

long PartOne(string input)
{
    var g = GetGraph(input);
    var components = GetSeed(g);
    components = Grow(g, components);
    components = Grow(g, components);
    return components.Count(c => Members(c).Any(m => m.StartsWith("t")));
}

object PartTwo(string input)
{
    var g = GetGraph(input);
    var components = GetSeed(g);
    while (components.Count > 1)
    {
        components = Grow(g, components);
    }

    return components.Single();
}

HashSet<string> GetSeed(Dictionary<string, HashSet<string>> g) => g.Keys.ToHashSet();

HashSet<string> Grow(Dictionary<string, HashSet<string>> g, HashSet<string> components) => (
    from c in components.AsParallel()
    let members = Members(c)
    from neighbour in members.SelectMany(m => g[m]).Distinct()
    where !members.Contains(neighbour)
    where members.All(m => g[neighbour].Contains(m))
    select Extend(c, neighbour)
).ToHashSet();

IEnumerable<string> Members(string c) =>
    c.Split(",");

string Extend(string c, string item) =>
    string.Join(",", Members(c).Append(item).OrderBy(x => x));

Dictionary<string, HashSet<string>> GetGraph(string input)
{
    var edges =
        from line in input.Replace("\r\n", "\n").Split("\n")
        let nodes = line.Split("-")
        from edge in new[] { (nodes[0], nodes[1]), (nodes[1], nodes[0]) }
        select (From: edge.Item1, To: edge.Item2);

    return (
        from e in edges
        group e by e.From
        into g
        select (g.Key, g.Select(e => e.To).ToHashSet())
    ).ToDictionary();
}