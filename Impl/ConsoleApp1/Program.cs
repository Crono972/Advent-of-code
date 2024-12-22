using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Numerics;
using System.Text.RegularExpressions;
using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);

internal class Program
{
    static object PartOne(string[] input) => Solve(input, 2);

    static object PartTwo(string[] input) => Solve(input, 20);

    static int Solve(string[] input, int cheat) {
        var path = GetPath(input);
        var indices = Enumerable.Range(0, path.Length).ToArray();

        // sum up the worthy cheats for each index along the path
        var cheatsFromI = (int i) => (
            from j in indices[0..i]
            let dist = Manhattan(path[i], path[j])
            let saving = i - (j + dist) 
            where dist <= cheat && saving >= 100
            select 1
        ).Sum();

        // parallel is gold today, it gives us an 3-4x boost
        return indices.AsParallel().Select(cheatsFromI).Sum();
    }

    static int Manhattan(Complex a, Complex b) =>
        (int)(Math.Abs(a.Imaginary - b.Imaginary) + Math.Abs(a.Real - b.Real));

    // Follow the path from finish to start, supposed that there is a single track in the input.
    // The index of a position in the returned array equals to its distance from the finish
    static Complex[] GetPath(string[] input) {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(x + y * Complex.ImaginaryOne, input[y][x])
        ).ToDictionary();

        Complex[] dirs = [-1, 1, Complex.ImaginaryOne, -Complex.ImaginaryOne];

        var start = map.Keys.Single(k => map[k] == 'S');
        var goal = map.Keys.Single(k => map[k] == 'E');

        var prev = (Complex?)null;
        var cur = goal;
        var res = new List<Complex> { cur };

        while (cur != start) {
            var dir = dirs.Single(dir => map[cur + dir] != '#' && cur + dir != prev);
            prev = cur;
            cur += dir;
            // (prev, cur) = (cur, cur + dir);
            res.Add(cur);
        }
        return res.ToArray();
    }
    public static void Main(string[] args)
    {
        var lines = File.ReadLines(@"../../../../../2024/Exo20/input.txt").ToArray();
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}