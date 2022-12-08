namespace Solutions;

public class Day01 : Solution
{
    public Day01(string[] input, int day) : base(input, day)
    {
        var list = new List<string>(input);
        list.Add("");
        Input = list.ToArray();
    }

    public override void RunPartOne()
    {
        Result(GetMaximumCalories(Input).Max());
    }

    public override void RunPartTwo()
    {
        Result(GetMaximumCalories(Input).Sum());
    }

    private int[] GetMaximumCalories(string[] input)
    {
        var maximumCalories = new int[3];
        var currentCalories = 0;
        
        foreach (var s in input)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                var minMaxCalories = maximumCalories.Min();
                if (currentCalories > minMaxCalories)
                {
                    maximumCalories[Array.IndexOf(maximumCalories, minMaxCalories)] = currentCalories;
                }
                currentCalories = 0;
                continue;
            }
            currentCalories += int.Parse(s);
        }

        return maximumCalories;
    }
}