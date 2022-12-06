namespace MyProject;
class Program
{
    static readonly Random rand = new Random();
    static void Main(string[] args)
    {
        var outSideCircle = 0;
        var insideCircle = 0;
        for (int i = 0; i < 10_000; i++)
        {
            (double x, double y) point = (RandomGenerator(), RandomGenerator());
            if (Math.Pow(point.x, 2) + Math.Pow(point.y, 2) < 1)
            {
                insideCircle++;
            }
            else
            {
                outSideCircle++;
            }
        }

        var pi = 4d * insideCircle / (insideCircle + outSideCircle);
        Console.WriteLine(pi);
        Console.ReadLine();
    }

    static double RandomGenerator()
    {
        return rand.NextDouble();
    }
}