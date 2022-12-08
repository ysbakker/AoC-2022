using System.Diagnostics;

namespace Solutions;

public abstract class Solution
{
    private int _day;
    internal string[] Input { get; set; }
    
    public Solution(string[] input, int day)
    {
        Input = input;
        _day = day;
    }

    public void RunDay()
    {
        Console.WriteLine($">> Day {_day}, part one:");
        RunPartOneBenchmarked();
        Console.WriteLine($"\n>> Day {_day}, part two:");
        RunPartTwoBenchmarked();
    }

    public void RunPartOneBenchmarked()
    {
        var sw = new Stopwatch();
        sw.Start();
        RunPartOne();
        sw.Stop();
        Console.WriteLine($"> Run took {sw.ElapsedMilliseconds}ms or {sw.ElapsedTicks} ticks.");
    }
    
    public void RunPartTwoBenchmarked()
    {
        var sw = new Stopwatch();
        sw.Start();
        RunPartTwo();
        sw.Stop();
        Console.WriteLine($"> Run took {sw.ElapsedMilliseconds}ms or {sw.ElapsedTicks} ticks.");
    }
    
    public abstract void RunPartOne();
    public abstract void RunPartTwo();

    internal void Result<T>(T result)
    {
        Console.WriteLine($"> Result: {result}");
    }
}