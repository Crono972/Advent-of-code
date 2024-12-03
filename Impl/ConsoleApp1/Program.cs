var lines = File.ReadAllLines(@"../../../../../2024/Exo01/input.txt");

var firstList = new List<int>();
var secondList = new List<int>();

foreach (var line in lines)
{
    var data = line.Split("   ");
    firstList.Add(int.Parse(data[0]));
    secondList.Add(int.Parse(data[1]));
}

    
Console.WriteLine(Part1(firstList, secondList));
Console.WriteLine(Part2(firstList, secondList));

static int Part1(List<int> first, List<int> second)
{
    int distance = 0;
    var orderedFirst = first.OrderBy(x => x).ToList();
    var orderedSecond = second.OrderBy(x => x).ToList();
    for (int i = 0; i < orderedFirst.Count; i++)
    {
        distance += Math.Abs(orderedFirst[i] - orderedSecond[i]);
    }

    return distance;
}

static int Part2(List<int> first, List<int> second)
{
    var similarity = 0;
    var occurenceSecondList = second.ToLookup(s => s);
    for (int i = 0; i < first.Count; i++)
    {
        var current = first[i];
        var simScore = occurenceSecondList.FirstOrDefault(kv => kv.Key == current)?.Count() ?? 0;
        similarity += current * simScore;
    }

    return similarity;
}
