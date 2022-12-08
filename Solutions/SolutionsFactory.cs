using System.Reflection;

namespace Solutions;

public static class SolutionsFactory
{
    public static Solution CreateSolution(int day, string[] input)
    {
        Assembly assembly = Assembly.Load("Solutions");
        Type? solutionType = assembly.GetType($"Solutions.Day{day:D2}");
        if (solutionType is null) throw new NotImplementedException($"Solution for day {day} not found");
        return (Solution)Activator.CreateInstance(solutionType, input, day)!;
    }
}
