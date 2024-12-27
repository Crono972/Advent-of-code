using System.Text.RegularExpressions;

var lines = File.ReadAllText(@"../../../../../2024/Exo25/input.txt");

Console.WriteLine($"PartOne : {PartOne(lines.Replace("\r\n", "\n"))}");
// Console.WriteLine($"PartTwo : {PartTwo(lines.Replace("\r\n", "\n"))}");


static object PartOne(string input) {
    var patterns = input.Split("\n\n").Select(b => b.Split("\n")).ToList();
    var keys = patterns.Where(p => p[0][0] == '.').Select(ParsePattern).ToList();
    var locks = patterns.Where(p => p[0][0] == '#').Select(ParsePattern).ToList();

    return keys.Sum(k => locks.Count(l => Match(l, k)));

    bool Match(int[] k, int[] l) =>
        Enumerable.Range(0, k.Length).All(i => k[i] + l[i] <= 7);

    int[] ParsePattern(string[] lines) =>
        Enumerable.Range(0, lines[0].Length).Select(x =>
            Enumerable.Range(0, lines.Length).Count(y => lines[y][x] == '#')
        ).ToArray();
}
