using System.Text.RegularExpressions;

Regex regex = new Regex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled);

var lines = File.ReadAllLines(@"../../../../../2024/Exo03/input.txt");
var line = string.Concat(lines);

Console.WriteLine(PartOne(line));
Console.WriteLine(PartTwo(line));

int PartOne(string line)
{
    var matchCollection = regex.Matches(line);
    var result = matchCollection.Select(s => int.Parse(s.Groups[1].Value) * int.Parse(s.Groups[2].Value)).Sum();
    return result;
}

int PartTwo(string line)
{
    var splitFirst = line.Split("don't");
    var activated = splitFirst.Skip(1).SelectMany(s => s.Split("do").Skip(1)).ToList();
    activated.Add(splitFirst[0]);
    var totalActivated = string.Concat(activated);
    return PartOne(totalActivated);
}