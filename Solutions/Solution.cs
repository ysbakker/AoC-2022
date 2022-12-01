using System.Diagnostics;

namespace Solutions;

public abstract class Solution
{
    private string[] Input { get; }
    
    public Solution(string[] input)
    {
        Input = input;
    }

    public void RunPartOneBenchmarked()
    {
        var sw = new Stopwatch();
        sw.Start();
        RunPartOne();
        sw.Stop();
        Console.WriteLine($"\n---\nRun took {sw.ElapsedMilliseconds}ms.");
    }
    
    public void RunPartTwoBenchmarked()
    {
        var sw = new Stopwatch();
        sw.Start();
        RunPartTwo();
        sw.Stop();
        Console.WriteLine($"\n---\nRun took {sw.ElapsedMilliseconds}ms.");
    }
    
    public abstract void RunPartOne();
    public abstract void RunPartTwo();
}