// See https://aka.ms/new-console-template for more information

var str = File.ReadAllLines(@"/Users/frederichocansung/_Dev/Advent of code/2022/Exo03/input.txt");
var total = 0;

for (int i = 0; i < str.Length; i+=3)
{
    var first = str[i];
    var second = str[i+1];
    var third = str[i+2];
    for (int j = 0; j < first.Length; j++)
    {
        var currentLetter = first[j];
        if (second.Contains(currentLetter) && third.Contains(currentLetter))
        {
            total += AlphabeticalPosition(currentLetter);
            break;
        }
    }
}

int AlphabeticalPosition(char currentLetter)
{
    return (currentLetter < 97 ? currentLetter - 38 : currentLetter - 96);
}
Console.WriteLine(total);