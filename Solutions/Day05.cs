namespace Solutions;

public class Day05Solution : Solution
{
    private readonly Dictionary<int, Stack<char>> _stacks = new(); // stack number => [crates]
    private string[] _instructions;
    
    public Day05Solution(string[] input) : base(input)
    {
        TransformInput();
    }

    public override void RunPartOne()
    {
        foreach (var instruction in _instructions)
        {
            ProcessInstruction(instruction);
        }

        Console.WriteLine(String.Join("", _stacks.Select(stack => stack.Value.Peek())));
    }

    public override void RunPartTwo()
    {
        foreach (var instruction in _instructions)
        {
            ProcessInstruction(instruction, 9001);
        }

        Console.WriteLine(String.Join("", _stacks.Select(stack => stack.Value.Peek())));
    }

    private void TransformInput()
    {
        var stacksLine = Input.First(line => line.Contains("1"));
        var stacks = stacksLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < stacks.Length; i++)
        {
            var crates = new Stack<char>();
            for (int j = Array.IndexOf(Input, stacksLine) - 1; j >= 0; j--)
            {
                var crate = Input[j][1 + i * 4];
                if (!char.IsWhiteSpace(crate)) crates.Push(crate);
            }
            _stacks.Add(i + 1, crates);
        }

        _instructions = Input[(Array.IndexOf(Input, stacksLine)+2)..];
    }

    private void ProcessInstruction(string instruction, int craneType = 9000)
    {
        var parts = instruction.Split(' ');
        var amount = int.Parse(parts[1]);
        var fromStack = int.Parse(parts[3]);
        var toStack = int.Parse(parts[5]);

        Stack<char> craneContents = new Stack<char>();
        for (int i = 0; i < amount; i++)
        {
            craneContents.Push(_stacks[fromStack].Pop());
            if (craneType == 9000)
            {
                _stacks[toStack].Push(craneContents.Pop());
            }
        }
        while (craneContents.Any())
        {
            _stacks[toStack].Push(craneContents.Pop());
        }
    }
}