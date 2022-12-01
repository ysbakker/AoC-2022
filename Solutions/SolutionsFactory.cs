namespace Solutions;

public static class SolutionsFactory
{
    public static Solution CreateSolution(int day, string[] input)
    {
        return day switch
        {
            1 => new Day01Solution(input),
            _ => throw new ArgumentOutOfRangeException(nameof(day), day, null)
        };
    }
}