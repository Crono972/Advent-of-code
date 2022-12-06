// See https://aka.ms/new-console-template for more information

var str = File.ReadAllLines(@"../../../../../2022/Exo06/input.txt");

var line = str.Single();
//var line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
for (int i = 0; i < line.Length; i++)
{
    var hashSet = new HashSet<char>();
    for (int j = 0; j < 14; j++)
    {
        hashSet.Add(line[i + j]);
    }
    if (hashSet.Count == 14)
    {
        Console.WriteLine(i + 14);
        break;
    }
}
Console.ReadKey();