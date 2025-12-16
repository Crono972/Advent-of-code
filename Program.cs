using System.Text.RegularExpressions;
using AdventOfCode.Impl._2025;
using AdventOfCode.Shared;

var input = File.ReadAllText(@"Input/2025/Exo12/input.txt");

var blocks = input.Split("\n\n");

var areas = (
    from block in blocks[..^1]
    let area = block.Count(ch => ch == '#')
    select area
).ToArray();

var temp = blocks.Last()
    .Split("\n");
var todos = temp
    .Select(line => new { line, nums = Regex.Matches(line, @"\d+").Select(m => int.Parse(m.Value)).ToArray() })
    .Select(@t => new Todo(@t.nums[0], @t.nums[1], @t.nums[2..])).ToArray();
       
var res = 0;
foreach(var todo in todos) {
    var areaNeeded = Enumerable.Zip(todo.counts, areas).Sum(p => p.First * p.Second);
    if (areaNeeded <= todo.w * todo.h) {
        res++;
    }
}

Console.WriteLine(res);
Console.ReadLine();

record Todo(int w, int h, int[] counts);