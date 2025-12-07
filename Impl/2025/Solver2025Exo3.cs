using AdventOfCode.Shared;

namespace AdventOfCode.Impl._2025;

public class Solver2025Exo3 : ISolver
{
    public string SolvePart1(string[] lines)
    {
        //List<int> jolt = new();
        //foreach (var line in lines)
        //{
        //    var volt = line.Select(s => int.Parse(s.ToString())).ToArray();
        //    int max = 0;
        //    for (int i = 0; i < volt.Length-1; i++)
        //    {
        //        for (int j = i+1; j < volt.Length; j++)
        //        {
        //            var currentNumber = int.Parse(volt[i].ToString() + volt[j].ToString());
        //            max = Math.Max(max, currentNumber);
        //        }
        //    }
        //    jolt.Add(max);
        //}

        //return jolt.Sum().ToString();
        return MaxJoltSum(lines, 2).ToString();
    }

    public long MaxJoltSum(string[] input, int batteryCount) =>
        input.Select(bank => MaxJolt(bank, batteryCount)).Sum();


    public string SolvePart2(string[] lines)
    {
        //List<long> jolt = new();
        //foreach (var line in lines)
        //{
        //    var volt = line.Select(s => int.Parse(s.ToString())).ToArray();

        //    // Approche gloutonne : pour chaque position, choisir le meilleur chiffre
        //    var result = FindLargest12Digits(volt);

        //    var numberStr = string.Concat(result);
        //    var currentNumber = long.Parse(numberStr);
        //    jolt.Add(currentNumber);
        //}

        //return jolt.Sum().ToString();
        return MaxJoltSum(lines, 12).ToString();
    }

    long MaxJolt(string bank, int batteryCount)
    {
        long res = 0L;
        for (; batteryCount > 0; batteryCount--)
        {
            // jump forward to the highest available digit in the bank, but keep 
            // batteryCount digits in the suffix, so that we can select something 
            // for those remaining batteries as well.
            char jolt = bank[..^(batteryCount - 1)].Max();
            bank = bank[(bank.IndexOf(jolt) + 1)..];
            res = 10 * res + (jolt - '0');
        }
        return res;
    }

    private int[] FindLargest12Digits(int[] digits)
    {
        int n = digits.Length;
        int toSelect = 12;
        int toRemove = n - toSelect;

        var stack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            // Tant qu'on peut encore supprimer des éléments et que le sommet de la pile
            // est plus petit que le chiffre courant, on enlève le sommet
            while (toRemove > 0 && stack.Count > 0 && stack.Peek() < digits[i])
            {
                stack.Pop();
                toRemove--;
            }

            stack.Push(digits[i]);
        }

        // Supprimer les éléments restants à la fin si nécessaire
        while (toRemove > 0)
        {
            stack.Pop();
            toRemove--;
        }

        return stack.Reverse().ToArray();
    }
}