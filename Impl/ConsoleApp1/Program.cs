using System.Numerics;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;

var inputData = File.ReadAllLines(@"../../../../../2024/Exo06/input.txt");

Complex Up = Complex.ImaginaryOne;
Complex TurnRight = -Complex.ImaginaryOne;
Console.WriteLine(PartOne(inputData));
Console.WriteLine(PartTwo(inputData));
int PartOne(string[] input) {
    var (map, start) = Parse(input);
    return Walk(map, start).positions.Count();
}

int PartTwo(string[] input) {
    var (map, start) = Parse(input);
    var positions = Walk(map, start).positions;
    var loops = 0;
    // simply try a blocker in each locations visited by the guard and count the loops
    foreach (var block in positions.Where(pos => map[pos] == '.')) {
        map[block] = '#';
        if (Walk(map, start).isLoop) {
            loops++;
        }
        map[block] = '.';
    }
    return loops;
}
// store the grid in a dictionary, to make bounds checks and navigation simple
// start represents the starting postion of the guard
(Map map, Complex start) Parse(string[] lines) {
    var map = (
        from y in Enumerable.Range(0, lines.Length)
        from x in Enumerable.Range(0, lines[0].Length)
        select new KeyValuePair<Complex, char>(-Up * y + x, lines[y][x])
    ).ToDictionary();

    var start = map.First(x => x.Value == '^').Key;
        
    return (map, start);
}

// returns the positions visited when starting from 'pos', isLoop is set if the 
// guard enters a cycle.
(IEnumerable<Complex> positions, bool isLoop) Walk(Map map, Complex pos) {
    var seen = new HashSet<(Complex pos, Complex dir)>();
    var dir = Up;
    while (map.ContainsKey(pos) && !seen.Contains((pos, dir))) {
        seen.Add((pos, dir));
        if (map.GetValueOrDefault(pos + dir) == '#') {
            dir *= TurnRight;
        } else {
            pos += dir;
        }
    }
    return (
        positions: seen.Select(s => s.pos).Distinct(),
        isLoop: seen.Contains((pos, dir))
    );
}