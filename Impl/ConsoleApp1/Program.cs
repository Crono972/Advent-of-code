// See https://aka.ms/new-console-template for more information

//var lines = File.ReadAllLines(@"../../../../../2022/Exo08/sample.txt");
var lines = File.ReadAllLines(@"../../../../../2022/Exo08/input.txt");

var visibility = 0;
for (int i = 0; i < lines.Length; i++)
{
    var row = lines[i];
    for (int j = 0; j < row.Length; j++)
    {
        var currentTree = int.Parse(lines[i][j].ToString());
        if (j == 0 || i == 0 || i == lines.Length - 1 || j == row.Length - 1) //edge
        {
            visibility++;
        }
        else
        {
            var currentRowIndex = i;
            var currentColumnIndex = j;
            var hautMax = lines
                .SelectMany((s, rowIndex) => s.Where((c, columnIndex) => columnIndex == currentColumnIndex && rowIndex < currentRowIndex))
                .Select(ch => int.Parse(ch.ToString())).Max();
            var basMax = lines
                .SelectMany((s, rowIndex) => s.Where((c, columnIndex) => columnIndex == currentColumnIndex && rowIndex > currentRowIndex))
                .Select(ch => int.Parse(ch.ToString())).Max();
            var gaucheMax = lines
                .SelectMany((s, rowIndex) => s.Where((c, columnIndex) => columnIndex < currentColumnIndex && rowIndex == currentRowIndex))
                .Select(ch => int.Parse(ch.ToString())).Max();
            var droiteMax = lines
                .SelectMany((s, rowIndex) => s.Where((c, columnIndex) => columnIndex > currentColumnIndex && rowIndex == currentRowIndex))
                .Select(ch => int.Parse(ch.ToString())).Max();
            if (currentTree > hautMax || currentTree > basMax || currentTree > gaucheMax || currentTree > droiteMax)
            {
                visibility++;
            }
        }

    }
}

Console.WriteLine(visibility);
Console.ReadKey();