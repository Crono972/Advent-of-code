var inputData = File.ReadAllLines(@"../../../../../2024/Exo07/input.txt");

var data = inputData.Select(line => line.Split(':'))
    .Select(kv
        => (long.Parse(kv[0]), 
            kv[1].Trim().Split(" ").Select(long.Parse).ToList() ))
    .ToList();
Console.WriteLine(PartOne(data));
Console.WriteLine(PartTwo(data));

long PartOne(List<(long Result, List<long> Numbers)> list)
{
    List<string> operations = ["mult", "add"];
    var validRow = list.Where(l => IsValidRow(l.Result, l.Numbers, operations)).ToList();
    return validRow.Sum(r => r.Result);
}

long PartTwo(List<(long Result, List<long> Numbers)> list)
{
    List<string> operations = ["mult", "add", "concat"];
    var validRow = list.Where(l => IsValidRow(l.Result, l.Numbers, operations)).ToList();
    return validRow.Sum(r => r.Result);
}

bool IsValidRow(long result, List<long> numbers, List<string> operations)
{
    var possibleOperation = GetCombinations(operations, numbers.Count -1);
    foreach (var operation in possibleOperation)
    {
        var chain = operation.ToArray();
        var curr = numbers[0];
        for (int i = 0; i < chain.Length; i++)
        {
            var operand = chain[i];
            switch (operand)
            {
                case "mult":
                    curr *= numbers[i + 1];
                    break;
                case "add":
                    curr += numbers[i + 1];
                    break;
                case "concat":
                    curr = long.Parse($"{curr.ToString()}" + numbers[i + 1].ToString());
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        if (result == curr)
        {
            return true;
        }
    }
    return false;
}

static IEnumerable<IEnumerable<T>>
    GetPermutations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new[] { t });

    return GetPermutations(list, length - 1)
        .SelectMany(t => list.Where(e => !t.Contains(e)),
            (t1, t2) => t1.Concat([t2]));
}


static IEnumerable<IEnumerable<T>>
    GetCombinations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new T[] { t });

    return GetCombinations(list, length - 1)
        .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
}