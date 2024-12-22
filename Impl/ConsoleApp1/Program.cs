internal class Program
{
    static object PartOne(string input)
    {
        return GetNums(input).Select(x => (long)SecretNumbers(x).Last()).Sum();
    }

    static object PartTwo(string input)
    {
        // create a dictionary of all buying options then select the one with the most banana:

        var buyingOptions = new Dictionary<string, int>();
        foreach (var num in GetNums(input))
        {
            var optionsBySeller = BuyingOptions(num);
            foreach (var seq in optionsBySeller.Keys)
            {
                buyingOptions[seq] = buyingOptions.GetValueOrDefault(seq) + optionsBySeller[seq];
            }
        }

        return buyingOptions.Values.Max();
    }

    static Dictionary<string, int> BuyingOptions(int seed)
    {
        var bananasSold = Bananas(seed).ToArray();

        var buyOptions = new Dictionary<string, int>();

        // a sliding window of 5 elements over the sold bananas defines the sequence the monkey 
        // will recognize. add the first occurrence of each sequence to the buyOptions dictionary 
        // with the corresponding banana count
        for (var i = 5; i < bananasSold.Length; i++)
        {
            var slice = bananasSold[(i - 5) .. i];
            var seq = string.Join(",", Diff(slice));
            if (!buyOptions.ContainsKey(seq))
            {
                buyOptions[seq] = slice[^1];
            }
        }

        return buyOptions;
    }

    static int[] Bananas(int seed) => SecretNumbers(seed).Select(n => n % 10).ToArray();

    static int[] Diff(IEnumerable<int> x) => x.Zip(x.Skip(1)).Select(p => p.Second - p.First).ToArray();

    static IEnumerable<int> SecretNumbers(int seed)
    {
        var mixAndPrune = (int a, long b) => (int)((a ^ b) % 16777216);

        yield return seed;
        for (var i = 0; i < 2000; i++)
        {
            seed = mixAndPrune(seed, seed * 64L);
            seed = mixAndPrune(seed, seed / 32L);
            seed = mixAndPrune(seed, seed * 2048L);
            yield return seed;
        }
    }

    static IEnumerable<int> GetNums(string input) => input.Replace("\r\n", "\n").Split("\n").Select(int.Parse);

    public static void Main(string[] args)
    {
        var lines = File.ReadAllText(@"../../../../../2024/Exo22/input.txt");
        Console.WriteLine(PartOne(lines));
        Console.WriteLine(PartTwo(lines));
    }
}