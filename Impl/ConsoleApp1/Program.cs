using System.Numerics;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, int>;

Complex up = -Complex.ImaginaryOne;
Complex down = Complex.ImaginaryOne;
Complex left = -1;
Complex right = 1;

var lines = File.ReadAllLines(@"../../../../../2024/Exo10/input.txt");
Console.WriteLine(Part1(lines));
Console.WriteLine(Part2(lines));

int Part1(string[] lines)
{
    var map = GenerateMap(lines);
    var scoresOfTrailHead = new List<int>();
    var trailHeads = map.Where(kv => kv.Value == 0).Select(kv => kv.Key).ToList();
    var pathsRoad = GetCombinations([up, down, left, right], 9).ToList();
    foreach (var trailHead in trailHeads)
    {
        var summits = new HashSet<Complex>();
        foreach (var path in pathsRoad)
        {
            var currentStep = trailHead;
            var level = 0;
            foreach (var step in path)
            {
                var nextStep = currentStep + step;
                if (!map.TryGetValue(nextStep, out var nextLevel))
                {
                    break;
                }

                if (nextLevel - level != 1)
                {
                    break;
                }
                level++;
                currentStep = nextStep;
            }

            if (level == 9)
            {
                summits.Add(currentStep);
            }
        }
        scoresOfTrailHead.Add(summits.Count);
    }
    return scoresOfTrailHead.Sum();
}

int Part2(string[] lines)
{
    var map = GenerateMap(lines);
    var ratingsOfTrailHead = new List<int>();
    var trailHeads = map.Where(kv => kv.Value == 0).Select(kv => kv.Key).ToList();
    var pathsRoad = GetCombinations([up, down, left, right], 9).ToList();
    foreach (var trailHead in trailHeads)
    {
        var ratings = 0;
        foreach (var path in pathsRoad)
        {
            var currentStep = trailHead;
            var level = 0;
            foreach (var step in path)
            {
                var nextStep = currentStep + step;
                if (!map.TryGetValue(nextStep, out var nextLevel))
                {
                    break;
                }

                if (nextLevel - level != 1)
                {
                    break;
                }
                level++;
                currentStep = nextStep;
            }

            if (level == 9)
            {
                ratings++;
            }
        }
        ratingsOfTrailHead.Add(ratings);
    }
    return ratingsOfTrailHead.Sum();
}



Map GenerateMap(string[] lines)
{
    var map = new Map();
    for (int y = 0; y < lines.Length; y++)
    {
        for (int x = 0; x < lines[y].Length; x++)
        {
            map[x + y * Complex.ImaginaryOne] = int.Parse(lines[y][x].ToString());
        }
    }

    return map;
}

static IEnumerable<IEnumerable<T>>
    GetCombinations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new T[] { t });

    return GetCombinations(list, length - 1)
        .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
}