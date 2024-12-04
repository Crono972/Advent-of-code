using System.Text.RegularExpressions;

var lines = File.ReadAllLines(@"../../../../../2024/Exo03/input.txt");

var line = string.Concat(lines);

var regex = new Regex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled);

var matchCollection = regex.Matches(line);
var result = matchCollection.Select(s => int.Parse(s.Groups[1].Value) * int.Parse(s.Groups[2].Value)).Sum();
Console.WriteLine(result);