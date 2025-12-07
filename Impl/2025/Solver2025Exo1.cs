using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo1 : ISolver
{

    public string SolvePart1(string[] lines)
    {
        throw new NotImplementedException();
    }

    public string SolvePart2(string[] lines)
    {
        int countTimeReach0 = 0;
        int currentValue = 50;
        foreach (var line in lines)
        {
            var dir = line[0];
            var distance = line.Substring(1);
            if (dir == 'L')
            {
                currentValue -= int.Parse(distance);
            }
            else
            {
                currentValue += int.Parse(distance);
            }

            if (currentValue == 0)
            {
                countTimeReach0++;
            }

            while (currentValue >= 100)
            {
                currentValue -= 100;
                countTimeReach0++;
            }

            while (currentValue < 0)
            {
                currentValue += 100;
                countTimeReach0++;
            }

        }
        return countTimeReach0.ToString();
    }
}