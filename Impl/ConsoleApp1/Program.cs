using System.Numerics;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;

var lines = File.ReadAllLines(@"../../../../../2024/Exo08/input.txt");

var map = new Map();
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        map[x + y * Complex.ImaginaryOne] = lines[y][x];
    }
}

Console.WriteLine(PartOne(map, lines[0].Length, lines.Length));
Console.WriteLine(PartTwo(map, lines[0].Length, lines.Length));

int PartOne(Map map, int maxX, int maxY)
{
    var antiNodesHash = new HashSet<Complex>();
    var symbols = map.Values.Where(Char.IsAsciiLetterOrDigit).Distinct();
    foreach (var symbol in symbols)
    {
        var coordinatesOfSymbol = map.Where(kv => kv.Value == symbol).ToList();
        foreach (var occurence in coordinatesOfSymbol)
        {
            foreach (var secondOccurence in coordinatesOfSymbol)
            {
                if (occurence.Key == secondOccurence.Key)
                {
                    continue;
                }

                var dir = secondOccurence.Key - occurence.Key;
                var firstAntiNode = secondOccurence.Key + dir;
                var secondAntiNode = occurence.Key - dir;
                antiNodesHash.Add(firstAntiNode);
                antiNodesHash.Add(secondAntiNode);
            }
        }
    }

    return antiNodesHash.Count(p => p.Real >= 0 && p.Real < maxX && p.Imaginary >= 0 && p.Imaginary < maxY);
}

int PartTwo(Map map, int maxX, int maxY)
{
    var antiNodesHash = new HashSet<Complex>();
    var symbols = map.Values.Where(Char.IsAsciiLetterOrDigit).Distinct();
    foreach (var symbol in symbols)
    {
        var coordinatesOfSymbol = map.Where(kv => kv.Value == symbol).ToList();
        foreach (var occurence in coordinatesOfSymbol.ToList())
        {
            foreach (var secondOccurence in coordinatesOfSymbol)
            {
                if (occurence.Key == secondOccurence.Key)
                {
                    continue;
                }

                antiNodesHash.Add(occurence.Key);
                antiNodesHash.Add(secondOccurence.Key);
                var dir = secondOccurence.Key - occurence.Key;
                //first dir
                var firstDir = secondOccurence.Key + dir;
                while (isInBound(firstDir, maxX, maxY))
                {
                    antiNodesHash.Add(firstDir);
                    firstDir += dir;
                }

                //second dir
                var secondDir = occurence.Key - dir;
                while (isInBound(secondDir, maxX, maxY))
                {
                    antiNodesHash.Add(secondDir);
                    secondDir -= dir;
                }
            }
        }
    }

    return antiNodesHash.Count;
}

bool isInBound(Complex p, int maxX, int maxY)
{
    return p.Real >= 0 && p.Real < maxX && p.Imaginary >= 0 && p.Imaginary < maxY;
}