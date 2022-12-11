// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

// var lines = File.ReadAllLines(@"../../../../../2022/Exo11/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo11/input.txt");

var reduceInterests = new List<int>();

var monkeys = new List<Monkey>();

for (int i = 0; i < lines.Length; i += 7)
{
    var itemStr = lines[i + 1].Split(":")[1];
    var items = itemStr.Split(",").Select(it => int.Parse(it.Trim()));
    var operationStr = lines[i + 2].Split("=")[1];
    var interest = MatchOperation(operationStr.Trim());

    var moduloValue = int.Parse(Regex.Match(lines[i + 3], @"Test: divisible by (?<value>\d+)")
        .Groups["value"].Value);

    reduceInterests.Add(moduloValue);
    var monkeyTrue = int.Parse(Regex.Match(lines[i + 4], @"If true: throw to monkey (?<value>\d+)")
        .Groups["value"].Value);

    var monkeyFalse = int.Parse(Regex.Match(lines[i + 5], @"If false: throw to monkey (?<value>\d+)")
        .Groups["value"].Value);

    var currentMonkey = new Monkey
    {
        Items = new Queue<long>(),
        NextMonkey = x => x % moduloValue == 0 ? monkeyTrue : monkeyFalse,
        Operation = interest
    };
    foreach (var item in items)
    {
        currentMonkey.Items.Enqueue(item);
    }

    monkeys.Add(currentMonkey);
}

var lcm = LcmOfArrayElements(reduceInterests.ToArray());
var rounds = 10_000;
for (int round = 0; round < rounds; round++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var item))
        {
            monkey.Activity++;
            var increasedValue = monkey.Operation(item);
            var boredValue = increasedValue % lcm;
            var nextMonkey = monkey.NextMonkey(boredValue);
            monkeys[nextMonkey].Items.Enqueue(boredValue);
        }
    }
}

var monkeyBusiness = monkeys
    .OrderByDescending(m => m.Activity).Take(2)
    .Aggregate((long)1, (previous, monkey) => previous * monkey.Activity);
Console.WriteLine(monkeyBusiness);
Console.ReadKey();

Func<long, long> MatchOperation(string operationStr)
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

long LcmOfArrayElements(int[] element_array)
{
    long lcm_of_array_elements = 1;
    int divisor = 2;

    while (true)
    {
        int counter = 0;
        bool divisible = false;
        for (int i = 0; i < element_array.Length; i++)
        {
            // lcm_of_array_elements (n1, n2, ... 0) = 0.
            // For negative number we convert into
            // positive and calculate lcm_of_array_elements.
            if (element_array[i] == 0)
            {
                return 0;
            }
            else if (element_array[i] < 0)
            {
                element_array[i] *= (-1);
            }

            if (element_array[i] == 1)
            {
                counter++;
            }

            // Divide element_array by devisor if complete
            // division i.e. without remainder then replace
            // number with quotient; used for find next factor
            if (element_array[i] % divisor == 0)
            {
                divisible = true;
                element_array[i] /= divisor;
            }
        }

        // If divisor able to completely divide any number
        // from array multiply with lcm_of_array_elements
        // and store into lcm_of_array_elements and continue
        // to same divisor for next factor finding.
        // else increment divisor
        if (divisible)
        {
            lcm_of_array_elements *= divisor;
        }
        else
        {
            divisor++;
        }

        // Check if all element_array is 1 indicate
        // we found all factors and terminate while loop.
        if (counter == element_array.Length)
        {
            return lcm_of_array_elements;
        }
    }
}

class Monkey
{
    public long Activity { get; set; } = 0;
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public Func<long, int> NextMonkey { get; set; }
}