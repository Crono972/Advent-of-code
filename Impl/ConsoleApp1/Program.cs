// See https://aka.ms/new-console-template for more information

var str = File.ReadAllLines(@"/Users/frederichocansung/_Dev/Advent of code/2022/Exo04/input.txt");
var total = 0;
foreach (var line in str)
{
    var elfWorkload = line.Split(',').Select(w => w.Split('-').Select(int.Parse).ToArray()).ToArray();

    if ((elfWorkload[0][0] <= elfWorkload[1][0] && elfWorkload[0][1] >= elfWorkload[1][1]) ||
        (elfWorkload[1][0] <= elfWorkload[0][0] && elfWorkload[1][1] >= elfWorkload[0][1]))
    {
        total++;
    }
}
Console.WriteLine(total);