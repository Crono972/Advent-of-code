// See https://aka.ms/new-console-template for more information

//var lines = File.ReadAllLines(@"../../../../../2022/Exo10/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo10/input.txt");

var q = new List<(int cycle, int val)>();

for (int i = 0; i < lines.Length; i++)
{
    var prog = lines[i].Split(" ");
    var instr = prog[0];
    var val = prog.Length > 1 ? int.Parse(prog[1]) : 0;
    var cycle = q.Count > 0 ? q[^1].cycle : 0;

    if (instr == "noop")
    {
        q.Add((cycle + 1, 0));
    }
    else
    {
        q.Add((cycle + 1, 0));
        q.Add((cycle + 2, val));
    }
}

var samplecycles = new HashSet<int> { 20, 60, 100, 140, 180, 220 };
int res = 0;
int reg = 1;
bool[,] screen = new bool[6, 40];

for (int i = 0; i < q.Count; i++)
{
    var cur = q[i];

    if (samplecycles.Contains(cur.cycle))
    {
        res += (cur.cycle * reg);
    }

    int curpos = (cur.cycle - 1) % 40;

    if (curpos >= reg - 1 && curpos <= reg + 1)
    {
        screen[(cur.cycle - 1) / 40, (cur.cycle - 1) % 40] = true;
    }

    reg += cur.val;
}

// Part1 result
Console.WriteLine(res);

// Draw part2
for (int i = 0; i < screen.GetLength(0); i++)
{
    for (int j = 0; j < screen.GetLength(1); j++)
    {
        if (screen[i, j])
            Console.Write("#");
        else Console.Write(".");
    }

    Console.WriteLine();
}

Console.ReadKey();