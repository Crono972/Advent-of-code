// See https://aka.ms/new-console-template for more information

var str = File.ReadAllLines(@"../../../../../2022/Exo06/input.txt");

var line = str.Single();
for (int i = 0; i < line.Length; i++)
{
    var a = line[i];
    var b = line[i + 1];
    var c = line[i + 2];
    var d = line[i + 3];
    var hashSet = new HashSet<char>();
    hashSet.Add(a);
    hashSet.Add(b);
    hashSet.Add(c);
    hashSet.Add(d);
    if (hashSet.Count == 4)
    {
        Console.WriteLine(i + 4);
        break;
    }
}
Console.ReadKey();