namespace Solutions;

public class Day03Solution : Solution
{
    public Day03Solution(string[] input) : base(input)
    {
    }

    public override void RunPartOne()
    {
        var sum = 0;
        foreach (var s in Input)
        {
            sum += ProductToPriority(FindDuplicateProduct(s));
        }

        Console.WriteLine(sum);
    }

    public override void RunPartTwo()
    {
        var sum = 0;
        for (int i = 0; i < Input.Length / 3; i++)
        {
            sum += ProductToPriority(FindBadge(Input[(i*3)..(i*3+3)]));
        }

        Console.WriteLine(sum);
    }

    private char FindDuplicateProduct(string rucksack)
    {
        var set = new HashSet<char>();
        for (var i = 0; i < rucksack.Length; i++)
        {
            if (i < rucksack.Length / 2) set.Add(rucksack[i]);
            else if (set.Contains(rucksack[i])) return rucksack[i];
        }
        throw new Exception("No duplicates found");
    }

    private char FindBadge(string[] group)
    {
        return group[0].Intersect(group[1]).Intersect(group[2]).First();
    }

    private int ProductToPriority(char c)
    {
        return (int)c switch
        {
            >= 65 and <= 90 => c - 38, // A through Z
            >= 97 and <= 122 => c - 96, // a through z
            _ => throw new Exception("Product not in range")
        };
    }
}