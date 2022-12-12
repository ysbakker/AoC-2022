using System.Reflection;

namespace Solutions;

public static class SolutionsFactory
{
    public static Solution CreateSolution(int day, string[] input, bool visualized = false)
    {
        Assembly assembly = Assembly.Load("Solutions");
        var s = visualized ? "Visualized" : "";
        Type? solutionType = assembly.GetType($"Solutions.Day{day:D2}{s}");
        if (solutionType is null) throw new NotImplementedException($"Solution for day {day} not found");
        return (Solution)Activator.CreateInstance(solutionType, input, day)!;
    }
}
