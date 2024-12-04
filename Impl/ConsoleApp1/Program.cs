var lines = File.ReadAllLines(@"../../../../../2024/Exo02/input.txt");

Console.WriteLine(Part1(lines));
Console.WriteLine(Part2(lines));
Console.WriteLine(PartTwo(lines));

static int Part1(string[] lines)
{
    int safeReport = 0;
    foreach (var line in lines)
    {
        var parsed = line.Split(' ').Select(int.Parse).ToArray();

        // var goodAlgo = false;
        // var pairs = parsed.Zip(parsed.Skip(1)).ToList();
        // if (pairs.All(p => 1 <= p.Second - p.First && p.Second - p.First <= 3) ||
        //     pairs.All(p => 1 <= p.First - p.Second && p.First - p.Second <= 3))
        // {
        //     goodAlgo = true;
        // }
        var lineIsSafe = true;
        var direction = 0;
        for (int i = 0; i <= parsed.Length - 2; i++)
        {
            var firstNum = parsed[i];
            var secondNum = parsed[i + 1];
            var currentSign = secondNum - firstNum == 0 ? 0 : secondNum - firstNum > 0 ? 1 : -1;
            var difference = Math.Abs(secondNum - firstNum);
            if (i == 0)
            {
                direction = currentSign;
            }

            lineIsSafe = lineIsSafe && currentSign == direction && currentSign != 0 && difference is <= 3 and >= 1;
        }

        // if (goodAlgo != lineIsSafe)
        // {
        //     Console.WriteLine(line );
        // }
        safeReport += lineIsSafe ? 1 : 0;
    }

    return safeReport;
}

static int Part2(string[] lines)
{
    int safeReport = 0;
    foreach (var line in lines)
    {
        var parsed = line.Split(' ').Select(int.Parse).ToArray();
        var lineIsSafe = true;
        var direction = 0;
        for (int i = 0; i <= parsed.Length - 3; i++)
        {
            var firstNum = parsed[i];
            var secondNum = parsed[i + 1];
            var thirdNum = parsed[i + 2];

            var currentSign = secondNum - firstNum == 0 ? 0 : secondNum - firstNum > 0 ? 1 : -1;
            var secondSign = thirdNum - firstNum == 0 ? 0 : thirdNum - firstNum > 0 ? 1 : -1;
            var difference = Math.Abs(secondNum - firstNum);
            var secondDifference = Math.Abs(thirdNum - firstNum);
            if (i == 0)
            {
                direction = currentSign;
            }

            lineIsSafe = lineIsSafe && (
                (currentSign == direction && currentSign != 0)
                || (secondSign == direction && secondSign != 0)) && difference is <= 3 and >= 1;
        }

        safeReport += lineIsSafe ? 1 : 0;
    }

    return safeReport;
}

static object PartTwo(string[] dataLines)
{
    return dataLines.Select(s => s.Split(' ').Select(int.Parse).ToArray()).Count(samples => Attenuate(samples).Any(Valid));
}
//
// public object PartTwo(string input) => 
//     ParseSamples(input).Count(samples => Attenuate(samples).Any(Valid));

IEnumerable<int[]> ParseSamples(string input) =>
    input.Split("\n")
        .Select(line => new { line, samples = line.Split(" ").Select(int.Parse) })
        .Select(@t => @t.samples.ToArray());

// Generates all possible variations of the input sequence by omitting 
// either zero or one element from it.
static IEnumerable<int[]> Attenuate(int[] samples) =>
    from i in Enumerable.Range(0, samples.Length + 1)
    let before = samples.Take(i - 1)
    let after = samples.Skip(i)
    select Enumerable.Concat(before, after).ToArray();

// Checks the monothinicity condition by examining consecutive elements
static bool Valid(int[] samples)
{
    var pairs = Enumerable.Zip(samples, samples.Skip(1));
    return
        pairs.All(p => 1 <= p.Second - p.First && p.Second - p.First <= 3) ||
        pairs.All(p => 1 <= p.First - p.Second && p.First - p.Second <= 3);
}