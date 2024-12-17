using System.Collections.Immutable;
using System.Numerics;
using System.Text.RegularExpressions;
using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);

internal class Program
{
    enum Opcode
    {
        Adv,
        Bxl,
        Bst,
        Jnz,
        Bxc,
        Out,
        Bdv,
        Cdv
    }

    static string Run(string input)
    {
        var (state, program) = Parse(input);
        var combo = (long op) => op < 4 ? (int)op : (int)state[op - 4];
        var res = new List<int>();
        for (var ip = 0; ip < program.Length; ip += 2)
        {
            switch ((Opcode)program[ip], program[ip + 1])
            {
                case (Opcode.Adv, var op): state[0] = state[0] >> combo(op); break;
                case (Opcode.Bdv, var op): state[1] = state[0] >> combo(op); break;
                case (Opcode.Cdv, var op): state[2] = state[0] >> combo(op); break;
                case (Opcode.Bxl, var op): state[1] = state[1] ^ op; break;
                case (Opcode.Bst, var op): state[1] = combo(op) % 8; break;
                case (Opcode.Jnz, var op): ip = state[0] == 0 ? ip : (int)op - 2; break;
                case (Opcode.Bxc, var op): state[1] = state[1] ^ state[2]; break;
                case (Opcode.Out, var op): res.Add(combo(op) % 8); break;
            }
        }

        return string.Join(",", res);
    }

    /*
        Determines register A for the given output. The search works recursively and in reverse order,
        starting from the last number to be printed and ending with the first.
    */
    static IEnumerable<long> GenerateA(List<long> output)
    {
        if (!output.Any())
        {
            yield return 0;
            yield break;
        }

        // this loop is pretty much the assembly code of the program reimplemented in c#
        foreach (var ah in GenerateA(output[1..]))
        {
            for (var al = 0; al < 8; al++)
            {
                var a = ah * 8 + al;
                long b = a % 8;
                b = b ^ 3;
                var c = a >> (int)b;
                b = b ^ c;
                b = b ^ 5;
                if (output[0] == b % 8)
                {
                    yield return a;
                }
            }
        }
    }

    static List<long> CursedDfs(List<long> operands, long curVal, int depth)
    {
        List<long> res = new();
        if (depth > operands.Count) return res;
        var tmp = curVal << 3;
        for (int i = 0; i < 8; i++)
        {
            var tmpRes = RunProgram(tmp + i, operands);
            if (tmpRes.SequenceEqual(operands.TakeLast(depth + 1)))
            {
                if (depth + 1 == operands.Count) res.Add(tmp + i);
                res.AddRange(CursedDfs(operands, tmp + i, depth + 1));
            }
        }

        return res;
    }

    static List<long> RunProgram(long regA, List<long> operands)
    {
        long regB = 0;
        long regC = 0;
        List<long> output = new();
        int pc = 0;
        while (pc < operands.Count)
        {
            long combo = (operands[pc + 1]) switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => regA,
                5 => regB,
                6 => regC,
                _ => long.MinValue
            };

            long literal = operands[pc + 1];
            long res = 0;
            bool jumped = false;
            switch (operands[pc])
            {
                case 0:
                    res = (long)(regA / Math.Pow(2, combo));
                    regA = res;
                    break;
                case 1:
                    res = regB ^ literal;
                    regB = res;
                    break;
                case 2:
                    res = combo % 8;
                    regB = res;
                    break;
                case 3:
                    if (regA != 0)
                    {
                        pc = (int)literal;
                        jumped = true;
                    }

                    break;
                case 4:
                    res = regB ^ regC;
                    regB = res;
                    break;
                case 5:
                    output.Add(combo % 8);
                    break;
                case 6:
                    res = (long)(regA / Math.Pow(2, combo));
                    regB = res;
                    break;
                case 7:
                    res = (long)(regA / Math.Pow(2, combo));
                    regC = res;
                    break;
                default: break;
            }

            if (!jumped) pc += 2;
            if (output.Count > operands.Count) break;
        }

        return output;
    }

    static (long[] state, long[] program) Parse(string input)
    {
        var blocks = input.Replace("\r\n", "\n").Split("\n\n").Select(ParseNums).ToArray();
        return (blocks[0], blocks[1]);
    }

    static long[] ParseNums(string st) =>
        Regex.Matches(st, @"\d+", RegexOptions.Multiline)
            .Select(m => long.Parse(m.Value))
            .ToArray();

    static object PartOne(string input) => Run(input);
    // static object PartTwo(string input) => GenerateA(Parse(input).program.ToList()).Min();

    static long PartTwo(string input)
    {
        var (state, program) = Parse(input);
        return CursedDfs(program.ToList(), 0, 0).Min();
    }

    public static void Main(string[] args)
    {
        var lines = File.ReadAllText(@"../../../../../2024/Exo17/input.txt");
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}