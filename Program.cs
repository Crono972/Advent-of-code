using AdventOfCode.Impl._2025;

var input = File.ReadAllLines(@"Input\2025\Exo3\Sample.txt");

var solver = new Solver2025Exo3();
var result = solver.SolvePart1(input);
Console.WriteLine("1st part: " + result);
result = solver.SolvePart2(input);
Console.WriteLine("2nd part: " + result);
Console.ReadLine();