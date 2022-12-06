namespace Solutions;

public static class SolutionsFactory
{
    public static Solution CreateSolution(int day, string[] input)
    {
        return day switch
        {
            1 => new Day01Solution(input),
            2 => new Day02Solution(input),
            3 => new Day03Solution(input),
            4 => new Day04Solution(input),
            5 => new Day05Solution(input),
            6 => new Day06Solution(input),
            _ => throw new ArgumentOutOfRangeException(nameof(day), day, null)
        };
    }
}