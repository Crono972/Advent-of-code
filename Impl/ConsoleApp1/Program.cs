// See https://aka.ms/new-console-template for more information

//var lines = File.ReadAllLines(@"../../../../../2022/Exo10/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo10/input.txt");

var cycle = 0;
var currentSignalStrenght = 1;

var signalStrenghts = new Dictionary<int, int>();

for (int i = 0; i < lines.Length; i++)
{
    var currentLine = lines[i];
    if (currentLine.Contains("noop"))
    {
        cycle++;
    }
    else
    {
        if (currentLine.Contains("addx"))
        {
            var valueIncr = int.Parse(currentLine.Split(" ")[1]);
            
            //1st cycle
            cycle++;
            if ((cycle - 20) % 40 == 0)
            {
                if (!signalStrenghts.ContainsKey(cycle))
                {
                    signalStrenghts[cycle] = currentSignalStrenght;
                }
            }
            //2nd cycle
            cycle++;
            if ((cycle - 20) % 40 == 0)
            {
                if (!signalStrenghts.ContainsKey(cycle))
                {
                    signalStrenghts[cycle] = currentSignalStrenght;
                }
            }

            currentSignalStrenght += valueIncr;
        }
    }

    if ((cycle - 20) % 40 == 0)
    {
        if (!signalStrenghts.ContainsKey(cycle))
        {
            signalStrenghts[cycle] = currentSignalStrenght;
        }
    }
}

Console.WriteLine(signalStrenghts.Sum(kp => kp.Key * kp.Value));
Console.ReadKey();