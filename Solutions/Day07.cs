namespace Solutions;

public class Day07Solution : Solution
{
    private string CurrentPath { get; set; } = string.Empty;
    private readonly Dictionary<string, int> _directorySizes = new();
    private readonly Dictionary<string, int> _totalDirectorySizes = new();
    
    public Day07Solution(string[] input) : base(input)
    {
        ExecuteCommands();
    }

    public override void RunPartOne()
    {
        Console.WriteLine(_totalDirectorySizes.Sum(v => v.Value <= 100_000 ? v.Value : 0));
    }

    public override void RunPartTwo()
    {
        var totalDiskSpace = 70_000_000;
        var requiredFreeDiskSpace = 30_000_000;
        
        var currentSpaceInUse = _totalDirectorySizes.Max(v => v.Value);
        var spaceToFree = requiredFreeDiskSpace - (totalDiskSpace - currentSpaceInUse);
        var smallestPossibleDirectory = _totalDirectorySizes.Values.Where(v => v > spaceToFree).Min();
        
        Console.WriteLine(smallestPossibleDirectory);
    }

    private void ExecuteCommands()
    {
        for (int i = 0; i < Input.Length; i++)
        {
            if (Input[i].StartsWith('$')) ExecuteCommand(Input[i], i);
        }
    }
    
    private void ExecuteCommand(string command, int index)
    {
        var parts = command.Split(" ");

        if (parts[1] == "cd") ChangeDirectory(parts[2]);
        else if (parts[1] == "ls") CalculateDirectorySizes(index);
    }

    private void ChangeDirectory(string directory)
    {
        if (directory == "/") CurrentPath = "/";
        else if (directory == "..") CurrentPath = CurrentPath.Substring(0, CurrentPath.LastIndexOf('/') - 1);
        else CurrentPath += $"{directory}/";
    }
    
    private void CalculateDirectorySizes(int index)
    {
        int size = 0;
        for (int i = index + 1; i < Input.Length; i++)
        {
            if (Input[i].StartsWith('$')) break;
            var parts = Input[i].Split(" ");
            if (parts[0] != "dir") size += int.Parse(parts[0]);
        }
        _directorySizes.Add(CurrentPath, size);
        foreach (var dir in _directorySizes.Keys.Where(dir => CurrentPath.StartsWith(dir)))
        {
            _totalDirectorySizes[dir] = _totalDirectorySizes.GetValueOrDefault(dir) + size;
        }
    }
}