// See https://aka.ms/new-console-template for more information

//var lines = File.ReadAllLines(@"../../../../../2022/Exo10/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo09/input.txt");
Dictionary<string, int> tailCoordsVisitedP1 = new Dictionary<string, int>();
Dictionary<string, int> tailCoordsVisitedP2 = new Dictionary<string, int>();
Dictionary<int, int[]> knotPositions = new Dictionary<int, int[]>() {
            {0, new int[] { 0,0} }, {1, new int[] { 0,0} }, {2, new int[] { 0,0} }, {3, new int[] { 0,0} }, {4, new int[] { 0,0} },
            {5, new int[] { 0,0} }, {6, new int[] { 0,0} }, {7, new int[] { 0,0} }, {8, new int[] { 0,0} }, {9, new int[] { 0,0} }
        };

tailCoordsVisitedP1.Add("0,0", 1);
tailCoordsVisitedP2.Add("0,0", 1);

foreach (string s in lines)
{
    string[] direcitons = s.Split();
    int distance = int.Parse(direcitons[1]);
    for (int x = 0; x < distance; x++)
    {
        knotPositions[0][0] -= direcitons[0] == "L" ? 1 : 0;
        knotPositions[0][0] += direcitons[0] == "R" ? 1 : 0;
        knotPositions[0][1] += direcitons[0] == "U" ? 1 : 0;
        knotPositions[0][1] -= direcitons[0] == "D" ? 1 : 0;
        for (int i = 1; i < 10; i++)
        {
            if ((Math.Abs(knotPositions[i][0] - knotPositions[i - 1][0]) + Math.Abs(knotPositions[i][1] - knotPositions[i - 1][1])) == 2)
            {
                if (!(Math.Abs(knotPositions[i][0] - knotPositions[i - 1][0]) == 1 || Math.Abs(knotPositions[i][1] - knotPositions[i - 1][1]) == 1))
                {
                    knotPositions[i][0] += knotPositions[i][0] < knotPositions[i - 1][0] ? 1 : 0;
                    knotPositions[i][0] -= knotPositions[i][0] > knotPositions[i - 1][0] ? 1 : 0;
                    knotPositions[i][1] += knotPositions[i][1] < knotPositions[i - 1][1] ? 1 : 0;
                    knotPositions[i][1] -= knotPositions[i][1] > knotPositions[i - 1][1] ? 1 : 0;
                }
            }
            else
            {// tail moves one step diagonally to keep up if not on the name row/col
             // Bottom Left: move up and right
                knotPositions[i] = (knotPositions[i][0] < knotPositions[i - 1][0] && knotPositions[i][1] < knotPositions[i - 1][1]) ? Add(knotPositions[i], new int[] { 1, 1 }) : knotPositions[i];
                // Bottom Right: move up and left
                knotPositions[i] = (knotPositions[i][0] > knotPositions[i - 1][0] && knotPositions[i][1] < knotPositions[i - 1][1]) ? Add(knotPositions[i], new int[] { -1, 1 }) : knotPositions[i];
                // Top Left: move down and right
                knotPositions[i] = (knotPositions[i][0] < knotPositions[i - 1][0] && knotPositions[i][1] > knotPositions[i - 1][1]) ? Add(knotPositions[i], new int[] { 1, -1 }) : knotPositions[i];
                // Top Right: move down and left
                knotPositions[i] = (knotPositions[i][0] > knotPositions[i - 1][0] && knotPositions[i][1] > knotPositions[i - 1][1]) ? Add(knotPositions[i], new int[] { -1, -1 }) : knotPositions[i];
            }
        }
        if (!tailCoordsVisitedP1.ContainsKey(knotPositions[1][0] + "," + knotPositions[1][1]))
            tailCoordsVisitedP1.Add(knotPositions[1][0] + "," + knotPositions[1][1], 1);

        if (!tailCoordsVisitedP2.ContainsKey(knotPositions[9][0] + "," + knotPositions[9][1]))
            tailCoordsVisitedP2.Add(knotPositions[9][0] + "," + knotPositions[9][1], 1);
    }
}
Console.WriteLine("=" +
                  "======================");
Console.WriteLine("Day 9");
Console.WriteLine("Part 1: {0}", tailCoordsVisitedP1.Count());
Console.WriteLine("Part 2: {0}", tailCoordsVisitedP2.Count());
Console.ReadKey();
static int[] Add(int[] a, int[] b)
{
    return new[] { a[0] + b[0], a[1] + b[1] };
}