// See https://aka.ms/new-console-template for more information

//var lines = File.ReadAllLines(@"../../../../../2022/Exo07/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo07/input.txt");
var currentDirectoryPath = new Stack<string>();
var files = new List<(string path, int size)>();

foreach (var line in lines)
{
    if (line.StartsWith("$ cd"))
    {
        var directoryArg = line.Substring(5);
        if (directoryArg == "..")
        {
            currentDirectoryPath.Pop();
        }
        else
        {
            currentDirectoryPath.Push(directoryArg);
        }
        continue;
    }

    if (line.StartsWith("$ ls") || line.StartsWith("dir"))
    {
        continue;
    }
    var split = line.Split(' ');
    var size = int.Parse(split[0]);
    var path = $"{Pwd(currentDirectoryPath)}/{split[1]}";
    files.Add((path, size));
}

var folderSize = new Dictionary<string, int>();
foreach (var file in files)
{
    var folders = file.path.Substring(1).Split('/');
    foreach (var folder in folders.SkipLast(1))
    {
        if (!folderSize.ContainsKey(folder))
        {
            folderSize.Add(folder, file.size);
        }
        else
        {
            folderSize[folder] += file.size;
        }
    }
}

var exo1 = folderSize.Where(v => v.Value <= 100_000).Sum(d => d.Value);
Console.WriteLine(exo1);
Console.ReadKey();

string Pwd(Stack<string> currentDirectoryPathStack)
{
    var path = currentDirectoryPathStack.ToArray().Reverse();
    return string.Join("/", path);
}