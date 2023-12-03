namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        Solve();
    }

    record Num(int Value, int ColStart, int ColEnd, int Row);
    record Symbol(char Value, int Row, int Col);

    public static void Solve()
    {
        var lines = File.ReadAllLines(@"../../../../../2023/Exo03/input.txt");

        int sum = 0;

        var nums = new List<Num>();
        var syms = new List<Symbol>();

        for (int row = 0; row < lines.Length; row++)
        {
            var currentLine = lines[row];
            string bufferValue = string.Empty;
            int xStart = -1;
            int xEnd = -1;

            for (int col = 0; col < currentLine.Length; col++)
            {
                if (char.IsDigit(currentLine[col]))
                {
                    if (bufferValue == string.Empty)
                    {
                        xStart = col;
                    }

                    bufferValue += currentLine[col];
                    xEnd = col;
                }
                else if (currentLine[col] == '.')
                {
                    if (bufferValue != string.Empty)
                    {
                        var num = new Num(int.Parse(bufferValue), xStart, xEnd, row);
                        nums.Add(num);
                        bufferValue = string.Empty;
                    }
                }
                else
                {
                    if (bufferValue != string.Empty)
                    {
                        var num = new Num(int.Parse(bufferValue), xStart, xEnd, row);
                        nums.Add(num);
                        bufferValue = string.Empty;
                    }
                    syms.Add(new Symbol(currentLine[col], row, col));
                }
            }

            if (bufferValue != string.Empty)
            {
                nums.Add(new Num(int.Parse(bufferValue), xStart, xEnd, row));
            }
        }

        var part1 = nums.Where(n => syms.Any(s => AreAdjacent(n, s))).Select(n => n.Value).Sum();
        Console.WriteLine(part1);
    }

    static bool AreAdjacent(Num number, Symbol symbol)
    {
        return Math.Abs(symbol.Row - number.Row) <= 1
               && symbol.Col >= number.ColStart - 1
               && symbol.Col <= number.ColEnd + 1;
    }
}

public static class ExtensionsClass
{
    public static bool IsBetween<T>(this T item, T start, T end)
    {
        return Comparer<T>.Default.Compare(item, start) >= 0
               && Comparer<T>.Default.Compare(item, end) <= 0;
    }
}