var input = File.ReadAllLines(@"../../../../../2023/Exo07/input.txt");

var parseLine = (string line, string cardOrder, string jokers) =>
{
    var parts = line.Split(' ');
    var hand = parts[0];
    var bid = int.Parse(parts[1]);

    HandType handType = HandType.FiveOfAKind;
    var handWithoutJokers = jokers != "" ? hand.Replace(jokers, "") : hand;
    var numJokers = hand.Length - handWithoutJokers.Length;
    var groups =
        handWithoutJokers
            .GroupBy(x => x)
            .Select(x => x.Count())
            .OrderByDescending(x => x)
            .Concat(new[] {0})
            .ToArray();
    groups[0] += numJokers;
    handType = groups switch
    {
        [5, ..] => HandType.FiveOfAKind,
        [4, ..] => HandType.FourOfAKind,
        [3, 2, ..] => HandType.FullHouse,
        [3, ..] => HandType.ThreeOfAKind,
        [2, 2, ..] => HandType.TwoPair,
        [2, ..] => HandType.OnePair,
        [..] => HandType.HighCard,
    };

    var weight = hand.Select((card,index) => cardOrder.IndexOf(card) << (4*(5-index))).Sum();

    return (hand, handType, weight, bid);
};

var solve = (string cardOrder, string jokers) =>
{
    var hands = input.Select(line => parseLine(line, cardOrder, jokers));
    var orderedHands = hands.OrderBy(x => x.handType).ThenBy(x => x.weight);
    var result = orderedHands.Select((hand, index) => hand.bid * (index + 1)).Sum();
    return result;
};

var result1 = solve("23456789TJQKA", "");
Console.WriteLine($"Result1 = {result1}");

var result2 = solve("J23456789TQKA", "J");
Console.WriteLine($"Result2 = {result2}");

enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
};

//
// public class Program
// {
//     static void Main(string[] args)
//     {
//         Solve();
//         Console.ReadKey();
//     }
//
//     public static void Solve()
//     {
//         var lines = File.ReadAllLines(@"../../../../../2023/Exo07/input.txt");
//
//         
        // var sw = Stopwatch.StartNew();
        // var hands = new List<Bid>();
        // //parsing
        // foreach (var line in lines)
        // {
        //     if (!string.IsNullOrWhiteSpace(line))
        //     {
        //         var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //         hands.Add(new Bid
        //         {
        //             Hand = parts[0],
        //             Bidding = int.Parse(parts[1])
        //         });
        //     }
        // }
        //
        // Console.WriteLine($"Parsing took : {sw.Elapsed}");
        // sw.Restart();
        // var winningsPart1 = 0;
        // var orderedHands = hands
        //     .OrderBy(c => HandQualifierPart1(c.Hand))
        //     .ThenByDescending(c => c.Hand, new CardValueComparerPart1()).ToList();
        //
        // Console.WriteLine($"Ordering took : {sw.Elapsed}");
        // sw.Restart();
        // for (int i = 0; i < orderedHands.Count(); i++)
        // {
        //     winningsPart1 += (i+1) * orderedHands[i].Bidding;
        // }
        //
        // Console.WriteLine($"Part1 : {winningsPart1} , bidding compute took {sw.Elapsed}");
        //
        // sw.Restart();
        // long winningsPart2 = 0;
        // var orderedHandsP2 = hands
        //     .OrderBy(c => HandQualifierPart2(c.Hand))
        //     .ThenByDescending(c => c.Hand, new CardValueComparerPart2()).ToList();
        //
        // Console.WriteLine($"Ordering took : {sw.Elapsed}");
        // sw.Restart();
        // for (int i = 0; i < orderedHandsP2.Count(); i++)
        // {
        //     winningsPart2 += (i+1) * orderedHandsP2[i].Bidding;
        // }
        //
        // Console.WriteLine($"Part2 : {winningsPart2} , bidding compute took {sw.Elapsed}");
    // }
    // static int HandQualifierPart1(string hands)
    // {
    //     var pairs = hands.GroupBy(c => c).OrderByDescending(p => p.Count()).ToList();
    //     var maxPair = pairs[0].Count();
    //     if (maxPair == 5)
    //     {
    //         return 6; //Five of a kind
    //     }
    //
    //     if (maxPair == 4)
    //     {
    //         return 5; //four of a kind
    //     }
    //
    //     if (maxPair == 3)
    //     {
    //         if (pairs[1].Count() == 2)
    //         {
    //             return 4; //Full house
    //         }
    //
    //         return 3; //three of a kind
    //     }
    //
    //     if (maxPair == 2)
    //     {
    //         if (pairs[1].Count() == 2)
    //         {
    //             return 2; //Two pair
    //         }
    //
    //         return 1; //One pair
    //     }
    //
    //     return 0; //HighCard
    // }
    //
    // static int HandQualifierPart2(string hands)
    // {
    //     var pairs = hands.GroupBy(c => c).OrderByDescending(p => p.Count()).ToList();
    //     var numberOfJ = pairs.SingleOrDefault(p => p.Key == 'J')?.Count() ?? 0;
    //     var maxPair = pairs[0].Key != 'J' ? pairs[0].Count() + numberOfJ : pairs[0].Count();
    //     if (maxPair == 5)
    //     {
    //         return 6; //Five of a kind
    //     }
    //
    //     if (maxPair == 4)
    //     {
    //         return 5; //four of a kind
    //     }
    //
    //     if (maxPair == 3)
    //     {
    //         if (pairs[1].Count() == 2)
    //         {
    //             return 4; //Full house
    //         }
    //
    //         return 3; //three of a kind
    //     }
    //
    //     if (maxPair == 2)
    //     {
    //         if (pairs[1].Count() == 2)
    //         {
    //             return 2; //Two pair
    //         }
    //
    //         return 1; //One pair
    //     }
    //
    //     return 0; //HighCard
    // }
// }
//
// public class Bid
// {
//     public string Hand { get; set; }
//     public int Bidding { get; set; }
// }
//
// public class CardValueComparerPart1 : IComparer<string>
// {
//     public const string Order = "AKQJT98765432";
//
//     public int Compare(string? a, string? b)
//     {
//         if (a == null)
//         {
//             return b == null ? 0 : -1;
//         }
//
//         if (b == null)
//         {
//             return 1;
//         }
//
//         int minLength = Math.Min(a.Length, b.Length);
//
//         for (int i = 0; i < minLength; i++)
//         {
//             int i1 = Order.IndexOf(a[i]);
//             int i2 = Order.IndexOf(b[i]);
//
//             if (i1 == -1)
//             {
//                 throw new Exception(a);
//             }
//
//             if (i2 == -1)
//             {
//                 throw new Exception(b);
//             }
//
//             int cmp = i1.CompareTo(i2);
//
//             if (cmp != 0)
//             {
//                 return cmp;
//             }
//         }
//
//         return a.Length.CompareTo(b.Length);
//     }
// }
//
//
// public class CardValueComparerPart2 : IComparer<string>
// {
//     public const string Order = "AKQT98765432J";
//
//     public int Compare(string? a, string? b)
//     {
//         if (a == null)
//         {
//             return b == null ? 0 : -1;
//         }
//
//         if (b == null)
//         {
//             return 1;
//         }
//
//         int minLength = Math.Min(a.Length, b.Length);
//
//         for (int i = 0; i < minLength; i++)
//         {
//             int i1 = Order.IndexOf(a[i]);
//             int i2 = Order.IndexOf(b[i]);
//
//             if (i1 == -1)
//             {
//                 throw new Exception(a);
//             }
//
//             if (i2 == -1)
//             {
//                 throw new Exception(b);
//             }
//
//             int cmp = i1.CompareTo(i2);
//
//             if (cmp != 0)
//             {
//                 return cmp;
//             }
//         }
//
//         return a.Length.CompareTo(b.Length);
//     }
// }