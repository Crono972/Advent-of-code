using System.Collections.Immutable;
using System.Numerics;
using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);
internal class Program
{
    static Complex North = -Complex.ImaginaryOne;
    static Complex South = Complex.ImaginaryOne;
    static Complex East = -1;
    static Complex West = 1;
    static readonly Complex[] Dirs = { North, East, West, South };

    static long PartOne(string[] input)
    {
        return FindDistance(Parse(input));
    }

    static long PartTwo(string[] input)
    {
        return FindBestSpots(Parse(input));
    }
    
  
    static int FindDistance(Map map) {
        var dist = DistancesTo(map, Goal(map));
        return dist[Start(map)];
    }
    
    // determines the number tiles that are on one of the shortest paths in the race.
    static int FindBestSpots(Map map) {
        var dist = DistancesTo(map, Goal(map));
        var start = Start(map);

        // flood fill algorithm determines the best spots by following the shortest paths 
        // using the distance map as guideline.

        var q = new PriorityQueue<State, int>();
        q.Enqueue(start, dist[start]);
        var bestSpots = new HashSet<State> { start };

        while (q.TryDequeue(out var state, out var remainingScore)) {
            foreach (var (next, score) in Steps(map, state, forward: true)) {
                if (bestSpots.Contains(next)) {
                    continue;
                }
                var nextScore = remainingScore - score;
                if (dist[next] == nextScore) {
                    bestSpots.Add(next);
                    q.Enqueue(next, nextScore);
                }
            }
        }
        return bestSpots.DistinctBy(state => state.pos).Count();
    }
    
    static Dictionary<State, int> DistancesTo(Map map, Complex goal) {
        var res = new Dictionary<State, int>();

        // a flood fill algorithm, works backwards from the goal, and 
        // computes the distances between any location in the map and the goal
        var q = new PriorityQueue<State, int>();
        foreach (var dir in Dirs) {
            q.Enqueue((goal, dir), 0);
            res[(goal, dir)] = 0;
        }

        while (q.TryDequeue(out var state, out var totalScore)) {
            if (totalScore != res[state]) {
                continue;
            }
            foreach (var (next, score) in Steps(map, state, forward: false)) {
                var nextCost = totalScore + score;
                if (res.ContainsKey(next) && res[next] < nextCost) {
                    continue;
                }

                res[next] = nextCost;
                q.Enqueue(next, nextCost);
            }
        }
        return res;
    }
    
    // returns the possible next or previous states and the associated costs for a given state.
    // in forward mode we scan the possible states from the start state towards the goal.
    // in backward mode we are working backwards from the goal to the start.
    static IEnumerable<(State, int cost)> Steps(Map map, State state, bool forward) {
        foreach (var dir in Dirs) {
            if (dir == state.dir) {
                var pos = forward ? state.pos + dir : state.pos - dir;
                if (map.GetValueOrDefault(pos) != '#') {
                    yield return ((pos, dir), 1);
                }
            } else if (dir != -state.dir) {
                yield return ((state.pos, dir), 1000);
            }
        }
    }

    static Map Parse(string[] input) {
        var map = Enumerable.Range(0, input.Length)
            .SelectMany(y => Enumerable.Range(0, input[0].Length),
                (y, x) => new KeyValuePair<Complex, char>(x + y * South, input[y][x])).ToImmutableDictionary();

        return map;
    }
    
    static Complex Goal(Map map) => map.Keys.Single(k => map[k] == 'E');
    static State Start(Map map) => (map.Keys.Single(k => map[k] == 'S'), East);
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines(@"../../../../../2024/Exo16/input.txt");
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}