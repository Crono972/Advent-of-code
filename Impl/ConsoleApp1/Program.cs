// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

// var lines = File.ReadAllLines(@"../../../../../2022/Exo11/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo11/input.txt");

var monkeys = new List<Monkey>();
for (int i = 0; i < lines.Length; i += 7)
{
    var itemStr = lines[i + 1].Split(":")[1];
    var items = itemStr.Split(",").Select(it => int.Parse(it.Trim()));
    var operationStr = lines[i + 2].Split("=")[1];
    var interest = MatchOperation(operationStr.Trim());

    var moduloValue = int.Parse(Regex.Match(lines[i + 3], @"Test: divisible by (?<value>\d+)")
        .Groups["value"].Value);

    var monkeyTrue = int.Parse(Regex.Match(lines[i + 4], @"If true: throw to monkey (?<value>\d+)")
        .Groups["value"].Value);

    var monkeyFalse = int.Parse(Regex.Match(lines[i + 5], @"If false: throw to monkey (?<value>\d+)")
        .Groups["value"].Value);

    var currentMonkey = new Monkey
    {
        Items = new Queue<int>(),
        NextMonkey = x => x % moduloValue == 0 ? monkeyTrue : monkeyFalse,
        Operation = interest
    };
    foreach (var item in items)
    {
        currentMonkey.Items.Enqueue(item);
    }
    monkeys.Add(currentMonkey);
}

var rounds = 20;
for (int round = 0; round < rounds; round++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var item))
        {
            monkey.Activity++;
            var increasedValue = monkey.Operation(item);
            var boredValue = increasedValue / 3;
            var nextMonkey = monkey.NextMonkey(boredValue);
            monkeys[nextMonkey].Items.Enqueue(boredValue);
            
        }
    }
}

var monkeyBusiness = monkeys
    .OrderByDescending(m => m.Activity).Take(2)
    .Aggregate(1, (previous, monkey) => previous * monkey.Activity);
Console.WriteLine(monkeyBusiness);
Console.ReadKey();

Func<int, int> MatchOperation(string operationStr)
{
    if (operationStr.Equals("old * old"))
    {
        return x => x * x;
    }

    var regex = Regex.Match(operationStr, @"old (?<operator>\S) (?<value>\d+)");
    if (regex.Success)
    {
        var op = regex.Groups["operator"].Value;
        var value = int.Parse(regex.Groups["value"].Value);
        if (op == "+")
        {
            return x => x + value;
        }

        if (op == "*")
        {
            return x => x * value;
        }
    }

    throw new ArgumentException("invalid operationStr");
}

class Monkey
{
    public int Activity { get; set; } = 0;
    public Queue<int> Items { get; set; }
    public Func<int, int> Operation { get; set; }
    public Func<int, int> NextMonkey { get; set; }
}