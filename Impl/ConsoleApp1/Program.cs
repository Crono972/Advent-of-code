// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var str = File.ReadAllLines(@"../../../../../2022/Exo05/input.txt");

var towers = new List<Stack<char>>();

var position = str[8];
for (int i = 0; i < position.Length; i++)
{
    var currentChar = position[i];
    if (Char.GetNumericValue(currentChar) != -1.0d)
    {
        var stack = new Stack<char>();
        int j = 7;
        var towerChar = str[j][i];
        while (j >= 0 && towerChar != ' ')
        {
            stack.Push(towerChar);
            j--;
            if (j >= 0)
            {
                towerChar = str[j][i];
            }
        }
        towers.Add(stack);
    }
}

foreach (var movement in str.Skip(10))
{
    if (!string.IsNullOrEmpty(movement))
    {
        var instr = Regex.Match(movement, @"move (\d+) from (\d+) to (\d+)");
        var qty = int.Parse(instr.Groups[1].Value);
        var from = int.Parse(instr.Groups[2].Value);
        var toward = int.Parse(instr.Groups[3].Value);
        Move(towers, qty, from, toward);
    }
}


var result = string.Empty;
foreach (var tower in towers)
{
    result += tower.Peek();
}

Console.WriteLine(result);

void Move(List<Stack<char>> towers, int qty, int from, int to)
{
    var fromTower = towers[from - 1];
    var toTower = towers[to - 1];
    for (int i = 0; i < qty; i++)
    {
        var elt = fromTower.Pop();
        toTower.Push(elt);
    }
}