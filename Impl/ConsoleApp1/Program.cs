var inputData = File.ReadAllLines(@"../../../../../2024/Exo05/input.txt");

Console.WriteLine(PartOne(inputData));
Console.WriteLine(PartTwo(inputData));

(Comparer<string>, List<string[]>) ParseData(string[] input)
{
    var comparerData = new HashSet<(string, string)>();
    var updates = new List<string[]>();
    for (int i = 0; i < input.Length; i++)
    {
        var line = input[i];
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        if (line.Contains('|'))
        {
            var split = line.Split('|');
            comparerData.Add((split[0], split[1]));
        }
        else
        {
            updates.Add(line.Split(",", StringSplitOptions.RemoveEmptyEntries));
        }
    }

    var customComparer = Comparer<string>
        .Create((a, b) => comparerData.Contains((a, b)) ? -1 : 1);
    return (customComparer, updates);
}

int PartOne(string[] input)
{
    var (customComparer, updates) = ParseData(input);
    var validesUpdate = updates.Where(u => u.OrderBy(u => u, customComparer).SequenceEqual(u)).ToList();
    var middleNumber = validesUpdate.Select(u => int.Parse(GetMiddle(u)));
    return middleNumber.Sum();
}

int PartTwo(string[] input)
{
    var (customComparer, updates) = ParseData(input);
    var nonValidesUpdate = updates
        .Where(u => !u.OrderBy(u => u, customComparer).SequenceEqual(u));
    var partTwo = nonValidesUpdate
        .Select(u => u.OrderBy(v => v, customComparer))
        .Select(u => int.Parse(GetMiddle(u.ToArray()))).Sum();
    return partTwo;
}

string GetMiddle(string[] arr)
{
    var middleIdx = arr.Length / 2;
    return arr[middleIdx];
}