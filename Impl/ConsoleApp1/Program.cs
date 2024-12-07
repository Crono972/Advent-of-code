using System.Numerics;

var inputData = File.ReadAllLines(@"../../../../../2024/Exo06/sample.txt");

Complex Up = -Complex.ImaginaryOne;
Complex Down = Complex.ImaginaryOne;
Complex Left = -1;
Complex Right = 1;

var guardPosition = new Complex(0, 0);
var map = new Dictionary<Complex, char>();
for (int i = 0; i < inputData.Length; i++)
{
    for (int j = 0; j < inputData[i].Length; j++)
    {
        var charact = inputData[i][j];
        map[new Complex(j, i)] = charact;
        if (charact is not ('#' or '.'))
        {
            guardPosition = new Complex(j, i);
        }
    }
}

// Console.WriteLine(PartOne(map, guardPosition));
Console.WriteLine(PartTwo(map, guardPosition));

int PartOne(Dictionary<Complex, char> map, Complex guardPosition)
{
    var visitedPosition = new HashSet<Complex>();
    visitedPosition.Add(guardPosition);
    int turn = 0;
    while (true)
    {
        turn++;
        var guardMarker = map[guardPosition];
        var facingDirection = GuardFacingDirection(guardMarker);
        var nextPosition = guardPosition + facingDirection;
        if (map.TryGetValue(nextPosition, out var positionMarker))
        {
            // Console.WriteLine();
            // Console.WriteLine($"Turn {turn}");
            // DebugPrintMap(map);
            if (positionMarker == '#')
            {
                nextPosition = guardPosition + GuardObstacleEvitment(guardMarker);
            }

            if (!map.TryGetValue(nextPosition, out var nextPositionMarker))
            {
                break;
            }

            map[guardPosition] = '.';
            var direction = nextPosition - guardPosition;
            var newGuardMarker = NewGuardMarker(direction);
            guardPosition = nextPosition;
            map[guardPosition] = newGuardMarker;
            visitedPosition.Add(guardPosition);
        }
        else
        {
            break;
        }
    }

    Console.WriteLine(turn);
    return visitedPosition.Count;
}

bool IsItAloop(Dictionary<Complex, char> map, Complex guardPosition)
{
    var visitedPosition = new HashSet<Complex>();
    visitedPosition.Add(guardPosition);
    int turn = 0;
    while (true)
    {
        turn++;
        var guardMarker = map[guardPosition];
        var facingDirection = GuardFacingDirection(guardMarker);
        var nextPosition = guardPosition + facingDirection;
        if (map.TryGetValue(nextPosition, out var positionMarker))
        {
            // Console.WriteLine();
            // Console.WriteLine($"Turn {turn}");
            // DebugPrintMap(map);
            if (positionMarker is '#' or 'O')
            {
                nextPosition = guardPosition + GuardObstacleEvitment(guardMarker);
            }

            if (!map.TryGetValue(nextPosition, out var nextPositionMarker))
            {
                return false;
            }

            map[guardPosition] = '.';
            var direction = nextPosition - guardPosition;
            var newGuardMarker = NewGuardMarker(direction);
            guardPosition = nextPosition;
            map[guardPosition] = newGuardMarker;
            visitedPosition.Add(guardPosition);
        }
        else
        {
            return false;
        }
        if(turn > 2 * visitedPosition.Count)
        {
            return true;
        }
    }
}

int PartTwo(Dictionary<Complex, char> map, Complex guardPosition)
{
    var maxX = (int)map.Keys.Max(x => x.Real);
    var maxY = (int)map.Keys.Max(x => x.Imaginary);
    var possibleObstaclePositions = Enumerable.Range(0, maxX).Zip(Enumerable.Range(0, maxY), (x, y) => x + Complex.ImaginaryOne * y)
        .Where(x => x != guardPosition).ToList();
    var alteredMaps = possibleObstaclePositions.Select(obstacle =>
    {
        var newMap = new Dictionary<Complex, char>(map);
        newMap[obstacle] = 'O';
        return newMap;
    }).ToList();
    return alteredMaps.Count(m => IsItAloop(m, guardPosition));
}

Complex GuardFacingDirection(char marker)
{
    return marker switch
    {
        '^' => Up,
        'v' => Down,
        '<' => Left,
        '>' => Right,
        _ => throw new ArgumentException("Invalid marker"),
    };
}

Complex GuardObstacleEvitment(char marker)
{
    return marker switch
    {
        '^' => Right,
        'v' => Left,
        '<' => Up,
        '>' => Down,
        _ => throw new ArgumentException("Invalid marker"),
    };
}

char NewGuardMarker(Complex direction)
{
    if (direction == Up)
        return '^';
    if (direction == Down)
        return 'v';
    if (direction == Left)
        return '<';
    if (direction == Right)
        return '>';
    throw new ArgumentException("Invalid marker");
}

void DebugPrintMap(Dictionary<Complex, char> map)
{
    var maxX = map.Keys.Max(x => x.Real);
    var maxY = map.Keys.Max(x => x.Imaginary);
    for (int i = 0; i <= maxY; i++)
    {
        for (int j = 0; j <= maxX; j++)
        {
            var position = new Complex(j, i);
            if (map.TryGetValue(position, out var marker))
            {
                Console.Write(marker);
            }
            else
            {
                Console.Write('.');
            }
        }

        Console.WriteLine();
    }
}