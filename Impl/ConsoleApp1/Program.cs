using System.Collections.Immutable;
using System.Numerics;
using System.Text.RegularExpressions;
using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);

internal class Program
{
    static object PartOne(string[] input)
    {
        var data = GetBlocks(input).Take(1024).ToList();
        return Distance(data);
    }

    static object PartTwo(string[] input) {
        // find the first block position that will cut off the goal position
        // we can use a binary search for this

        var blocks = GetBlocks(input);
        var (lo, hi) = (0, blocks.Length);
        while (hi - lo > 1) {
            var m = (lo + hi) / 2;
            if (Distance(blocks.Take(m).ToList()) == null) {
                hi = m;
            } else {
                lo = m;
            }
        }
        return $"{blocks[lo].Real},{blocks[lo].Imaginary}";
    }

    static int? Distance(IList<Complex> blocks) {
        // our standard priority queue based path finding
        
        var size = 70;
        var (start, goal) = (0, size + size * Complex.ImaginaryOne);
        var blocked = blocks.Concat([start]).ToHashSet();
    
        var q = new PriorityQueue<Complex, int>();
        q.Enqueue(start, 0);
        while (q.TryDequeue(out var pos, out var dist)) {
            if (pos == goal) {
                return dist;
            } 

            foreach (var dir in new[] { 1, -1, Complex.ImaginaryOne, -Complex.ImaginaryOne }) {
                var posT = pos + dir;
                if (!blocked.Contains(posT) &&
                    0 <= posT.Imaginary && posT.Imaginary <= size &&
                    0 <= posT.Real && posT.Real <= size
                   ) {
                    q.Enqueue(posT, dist + 1);
                    blocked.Add(posT);
                }
            }
        }
        return null;
    }

    static Complex[] GetBlocks(string[] input)
    {
        return input.Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => Regex.Matches(l, @"\d+"))
            .Select(m =>
            {
                
                return int.Parse(m[0].Value) + int.Parse(m[1].Value) * Complex.ImaginaryOne;
            }).ToArray();
    }
        
    public static void Main(string[] args)
    {
        var lines = File.ReadLines(@"../../../../../2024/Exo18/input.txt").ToArray();
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}