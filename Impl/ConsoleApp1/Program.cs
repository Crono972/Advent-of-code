using System.Numerics;
using Map = System.Collections.Immutable.ImmutableDictionary<System.Numerics.Complex, char>;
using System.Collections.Immutable;

Complex Up = -Complex.ImaginaryOne;
Complex Down = Complex.ImaginaryOne;
Complex Left = -1;
Complex Right = 1;

var inputData = File.ReadAllText(@"../../../../../2024/Exo04/input.txt");

Console.WriteLine(PartOne(inputData));
Console.WriteLine(PartTwo(inputData));

object PartOne(string input) {
    var mat = GetMap(input);
    int count = 0;
    foreach (Complex pt in mat.Keys)
    foreach (var dir in new[] { Right, Right + Down, Down + Left, Down })
    {
        if (Matches(mat, pt, dir, "XMAS"))
        {
            count++;
        }
    }

    return count;
}

object PartTwo(string input) {
    var mat = GetMap(input);
    int count = 0;
    foreach (var pt in mat.Keys)
    {
        if (Matches(mat, pt + Up + Left, Down + Right, "MAS") && Matches(mat, pt + Down + Left, Up + Right, "MAS"))
        {
            count++;
        }
    }
    return count;
}

// check if the pattern (or its reverse) can be read in the given direction 
// starting from pt
bool Matches(Map map, Complex pt, Complex dir, string pattern) {
    var chars = Enumerable.Range(0, pattern.Length)
        .Select(i => map.GetValueOrDefault(pt + i * dir))
        .ToArray();
    return
        chars.SequenceEqual(pattern) ||
        chars.SequenceEqual(pattern.Reverse());
}

// store the points in a dictionary so that we can iterate over them and 
// to easily deal with points outside the area using GetValueOrDefault
static Map GetMap(string input) {
    var map = input.Split("\n");
    return (
        from y in Enumerable.Range(0, map.Length)
        from x in Enumerable.Range(0, map[0].Length)
        select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, map[y][x])
    ).ToImmutableDictionary();
}