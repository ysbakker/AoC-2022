namespace Solutions;

public class Day06 : Solution
{
    public Day06(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        Result(FindDistinctCharactersMarker(4));
    }

    public override void RunPartTwo()
    {
        Result(FindDistinctCharactersMarker(14));
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