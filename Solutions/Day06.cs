namespace Solutions;

public class Day06Solution : Solution
{
    public Day06Solution(string[] input) : base(input)
    {
    }

    public override void RunPartOne()
    {
        Console.WriteLine(FindDistinctCharactersMarker(4));
    }

    public override void RunPartTwo()
    {
        Console.WriteLine(FindDistinctCharactersMarker(14));
    }

    public int FindDistinctCharactersMarker(int n)
    {
        for (int i = 0; i < Input[0].Length; i++)
        {
            if (Input[0][i..(i + n)].Distinct().Count() == n)
            {
                return i + n;
            }
        }

        return -1;
    }
}