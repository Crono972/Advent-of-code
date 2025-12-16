using System.Text.RegularExpressions;
using AdventOfCode.Impl._2025;
using AdventOfCode.Shared;

var input = File.ReadAllLines(@"Input\2025\Exo10\input.txt");

var solver = new Solver2025Exo10();
var result = solver.SolvePart1(input);
Console.WriteLine("1st part: " + result);
result = solver.SolvePart2(input);
Console.WriteLine("2nd part: " + result);
Console.ReadLine();

public class Solver2025Exo10 : ISolver
{
    
    record Problem(int[] target, int[] buttons, int[] joltage);

    record SinglePress(int buttonCount, int[] joltageChange);
    
    public string SolvePart1(string[] lines)
    {
        var res = 0;
        foreach (var p in Parse(lines)) {
            var minPressCount = SinglePresses(p)
                .Where(press => Enumerable.SequenceEqual(p.target, press.joltageChange.Select(i => i % 2)))
                .Min(press => press.buttonCount);
            res += minPressCount;
        }
        return res.ToString();
        
    }

    // Implements the idea presented by tenthmascot
    // https://www.reddit.com/r/adventofcode/comments/1pk87hl/
    public string SolvePart2(string[] lines)
    {
        var res = 0;
        foreach (var p in Parse(lines)) {
            res += Solve(p.joltage, SinglePresses(p), new Dictionary<string, int>());
        }
        return res.ToString();
    }

    // Collect changes in joltages when each button is pressed at most once
    List<SinglePress> SinglePresses(Problem p) {
        var presses = new List<SinglePress>();

        foreach (var buttonMask in Enumerable.Range(0, 1 << p.buttons.Length)) {
            var joltageChange = new int[p.joltage.Length];
            var buttonCount = 0;

            for (int ibutton = 0; ibutton < p.buttons.Length; ibutton++) {
                if ((buttonMask >> ibutton) % 2 == 1) {
                    buttonCount++;
                    var button = p.buttons[ibutton];
                    for (int ijoltage = 0; ijoltage < p.joltage.Length; ijoltage++) {
                        if ((button >> ijoltage) % 2 == 1) {
                            joltageChange[ijoltage]++;
                        }
                    }
                }
            }
            presses.Add(new SinglePress(buttonCount, joltageChange));
        }
        return presses;
    }

    int Solve(int[] joltages, List<SinglePress> singlePresses, Dictionary<string, int> cache) {

        if (joltages.All(jolt => jolt == 0)) {
            return 0;
        }

        var key = string.Join("-", joltages);
        if (!cache.ContainsKey(key)) {
            var best = 10_000_000;
            foreach (var singlePress in singlePresses) {

                var buttonCount = singlePress.buttonCount;
                var joltageChange = singlePress.joltageChange;

                var evens =
                    Enumerable.Range(0, joltages.Length)
                    .All(i => joltages[i] >= joltageChange[i] && (joltages[i] - joltageChange[i]) % 2 == 0);

                if (evens) {
                    var subProblem =
                        Enumerable.Range(0, joltages.Length)
                        .Select(i => (joltages[i] - joltageChange[i]) / 2)
                        .ToArray();

                    best = Math.Min(best, buttonCount + 2 * Solve(subProblem, singlePresses, cache));
                }
            }
            cache[key] = best;
        }
        return cache[key];
    }

    IEnumerable<Problem> Parse(string [] lines) {
        // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
        foreach (var line in lines) {
            var parts = line.Split(" ");

            var target = parts[0].Trim("[]".ToCharArray()).Select(ch => ch == '#' ? 1 : 0);
            // var buttons =
            //     from part in parts[1..^1]
            //     let digits = Regex.Matches(part, @"\d+").Select(m => int.Parse(m.Value))
            //     let mask = digits.Aggregate(0, (acc, d) => acc | (1 << d))
            //     select mask;
            // var joltage = 
            //     from m in Regex.Matches(parts[^1], @"\d+")
            //     select int.Parse(m.Value);

            // yield return new Problem([.. target], [.. buttons], [.. joltage]);
            yield return new Problem([0], [0], [0]);
        }
    }
}