var line = File.ReadAllText(@"../../../../../2024/Exo09/input.txt");

var disk = Disk.Parse(line);

// Console.WriteLine(Part1(disk));
Console.WriteLine(Part2(disk));

long Part1(Disk disk)
{
    var head = 0;
    var tail = disk.Blocks.Length - 1;
    while (head < tail)
    {
        if (disk.Blocks[head] != null)
        {
            head++;
            continue;
        }

        while (disk.Blocks[tail] == null) tail--;
        disk.Blocks[head++] = disk.Blocks[tail];
        disk.Blocks[tail--] = null;
    }

    return disk.Checksum();
}

long Part2(Disk disk)
{
    for (var fileId = disk.Allocated.Count - 1; fileId >= 0; fileId--)
    {
        var file = disk.Allocated[fileId];
        for (var j = 0; j < disk.Free.Count; j++)
        {
            var free = disk.Free[j];
            if ((free.max - free.min +1) < file.Length || free.min >= file.Min)
            {
                continue;
            }

            for (var k = 0; k < file.Length; k++)
            {
                disk.Blocks[free.min + k] = fileId;
                disk.Blocks[file.Min + k] = null;
            }
                
            disk.Free.RemoveAt(index: j);
            if ((free.max - free.min +1) > file.Length)
            {
                disk.Free.Insert(index: j, (min: free.min + file.Length, max: free.max));
            }
            break;
        }
    }
        
    return disk.Checksum();
}

public class Disk
{
    public readonly record struct File(int Min, int Length);

    public int?[] Blocks { get; }
    public List<File> Allocated { get; }
    public List<(int min, int max)> Free { get; }

    private Disk(int?[] blocks, List<File> allocated, List<(int min, int max)> free)
    {
        Blocks = blocks;
        Allocated = allocated;
        Free = free;
    }

    public long Checksum()
    {
        return Blocks
            .Select((file, i) => i * (file ?? 0L))
            .Sum();
    }

    public static Disk Parse(string map)
    {
        var volume = map.Sum(c => int.Parse(c.ToString()));
        var blocks = new int?[volume];
        var allocated = new List<File>();
        var free = new List<(int min, int max)>();

        var file = -1;
        var head = 0;

        for (var i = 0; i < map.Length; i++)
        {
            var count = int.Parse(map[i].ToString());
            var empty = i % 2 == 1;

            if (!empty)
            {
                file++;
                allocated.Add(new File(Min: head, Length: count));
            }
            else if (count != 0)
            {
                free.Add((head, head + count - 1));
            }

            for (var j = 0; j < count; j++)
            {
                blocks[head++] = empty
                    ? null
                    : file;
            }
        }

        return new Disk(blocks, allocated, free);
    }
}