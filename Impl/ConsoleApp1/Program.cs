using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Numerics;
using System.Text.RegularExpressions;
using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);

internal class Program
{
    static object PartOne(string[] input) => MatchCounts(input).Count(c => c != 0);

    static object PartTwo(string[] input) => MatchCounts(input).Sum();

    static IEnumerable<long> MatchCounts(string[] input) {
        var blocks = input[2..];
        var towels = input[0].Split(", ");
        return 
            from pattern in  blocks 
            select MatchCount(towels, pattern, new ConcurrentDictionary<string, long>());
    }

    // computes the number of ways the pattern can be build up from the towels. 
    // works recursively by matching the prefix of the pattern with each towel.
    // a full match is found when the pattern becomes empty. the cache is applied 
    // to _drammatically_ speed up execution
    static long MatchCount(string[] towels, string pattern, ConcurrentDictionary<string, long> cache) =>
        cache.GetOrAdd(pattern, (pattern) => 
            pattern switch {
                "" => 1,
                _  =>  towels
                    .Where(pattern.StartsWith)
                    .Sum(towel => MatchCount(towels, pattern[towel.Length ..], cache))
            }
        );
        
    public static void Main(string[] args)
    {
        var lines = File.ReadLines(@"../../../../../2024/Exo19/input.txt").ToArray();
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}