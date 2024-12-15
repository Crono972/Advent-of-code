using System.Numerics;

var lines = File.ReadAllLines(@"../../../../../2024/Exo14/input.txt");
var mapWidth = 101;
var mapHeight = 103;

Console.WriteLine(PartOne(lines));
Console.WriteLine(PartTwo(lines));

long PartOne(string[] input)
{
    var data = Parse(input);
    var endTurnPosition = data.Select(r => GetPositionAfterNSeconds(r.position, r.vector, 100)).ToList();
    var endTurnByQuadrant = endTurnPosition.ToLookup(p => QuadrantPosition(p, mapWidth, mapHeight));
    return endTurnByQuadrant.Where(kv => kv.Key != "invalid").Aggregate(1, (acc, grouping) => acc * grouping.Count());
}

long PartTwo(string[] input)
{
    var data = Parse(input);
    var numberOfPts = data.Count;
    var turn = 0;
    var diffPoint = data.Select(r => r.position).ToHashSet();
    while(diffPoint.Count != numberOfPts)
    {
        turn++;
        data = data.Select(r => (GetPositionAfterNSeconds(r.position, r.vector, 1), r.vector)).ToList();
        diffPoint = data.Select(r => r.position).ToHashSet();
    }

    for (int y = 0; y < mapHeight; y++)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            Console.Write(data.Any(r => r.position == x + y * Complex.ImaginaryOne) ? "#" : ".");
        }
        Console.WriteLine();
    }
    
    return turn;
}

Complex GetPositionAfterNSeconds(Complex position, Complex vector, int seconds)
{
    var endPosition = position;
    for (int i = 0; i < seconds; i++)
    {
        endPosition = ((endPosition.Real + vector.Real + mapWidth) % mapWidth) + ((endPosition.Imaginary + vector.Imaginary + mapHeight) % mapHeight) * Complex.ImaginaryOne;
    }
    return endPosition;
}

string QuadrantPosition(Complex position, int mapWide, int mapHeight)
{
    var halfWide = mapWide / 2;
    var halfHeight = mapHeight / 2;
    if(position.Real < halfWide)
    {
        if (position.Imaginary < halfHeight)
        {
            return "top-left";
        } 
        if(position.Imaginary > halfHeight)
        {
            return "bottom-left";
        }
    }
    if(position.Real > halfWide)
    {
        if (position.Imaginary < halfHeight)
        {
            return "top-right";
        } 
        if(position.Imaginary > halfHeight)
        {
            return "bottom-right";
        }
    }
    
    return "invalid";
}

IList<(Complex position, Complex vector)> Parse(string[] input)
{
    var result = new List<(Complex position, Complex vector)>();
    foreach (var line in input)
    {
        var parts = line.Split(" ");
        var firstPart = parts[0].Replace("p=", "").Split(",");
        var position = new Complex(int.Parse(firstPart[0]), int.Parse(firstPart[1]));
        var secondPart = parts[1].Replace("v=", "").Split(",");
        var vector = new Complex(int.Parse(secondPart[0]), int.Parse(secondPart[1]));
        result.Add((position, vector));
    }
    return result;
}