namespace Solutions;

public class Day11 : Solution
{
    private Dictionary<int, Monkey> _monkeys = new();
    
    public Day11(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        ParseInput();
        for (int i = 0; i < 20; i++)
        {
            foreach ((var id, var monkey) in _monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    var newItem = ExecuteOperation(monkey.Operation, item);
                    monkey.InspectionCount++;
                    newItem /= 3;
                    if (newItem % monkey.TestDivisibleBy == 0)
                    {
                        _monkeys[monkey.MonkeyIfTestTrue].Items.Add(newItem);
                    }
                    else
                    {
                        _monkeys[monkey.MonkeyIfTestFalse].Items.Add(newItem);
                    }
                }
                monkey.Items.Clear();
            }
        }

        var inspectionCounts = _monkeys.Values
            .Select(m => m.InspectionCount)
            .OrderByDescending(i => i)
            .ToArray();
        Result(inspectionCounts[0] * inspectionCounts[1]);
    }

    public override void RunPartTwo()
    {
        ParseInput();
        int commonDenominator = _monkeys.Values
            .Select(m => m.TestDivisibleBy)
            .Aggregate(1, (acc, item) => acc * item);
        for (int i = 0; i < 10_000; i++)
        {
            foreach ((var id, var monkey) in _monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    var newItem = ExecuteOperation(monkey.Operation, item);
                    monkey.InspectionCount++;
                    newItem %= commonDenominator;
                    if (newItem % monkey.TestDivisibleBy == 0)
                    {
                        _monkeys[monkey.MonkeyIfTestTrue].Items.Add(newItem);
                    }
                    else
                    {
                        _monkeys[monkey.MonkeyIfTestFalse].Items.Add(newItem);
                    }
                }
                monkey.Items.Clear();
            }
        }

        var inspectionCounts = _monkeys.Values
            .Select(m => m.InspectionCount)
            .OrderByDescending(i => i)
            .ToArray();
        Result(inspectionCounts[0] * inspectionCounts[1]);
    }

    private void ParseInput()
    {
        _monkeys = new();
        
        for (int i = 0; i < Input.Length; i++)
        {
            var id = Input[i][^2] - 48;
            _monkeys[id] = new Monkey();

            var items = Input[i + 1][17..].Split(",", StringSplitOptions.TrimEntries);
            _monkeys[id].Items = items.Select(item => long.Parse(item)).ToList();
            _monkeys[id].Operation = Input[i + 2][19..];
            _monkeys[id].TestDivisibleBy = int.Parse(Input[i + 3][21..]);
            _monkeys[id].MonkeyIfTestTrue = Input[i + 4][^1] - 48;
            _monkeys[id].MonkeyIfTestFalse = Input[i + 5][^1] - 48;

            i += 6;
        }
    }
    
    private long ExecuteOperation(string operation, long item)
    {
        string finalOperation = operation.Replace("old", item.ToString());
        if (operation.Contains("*"))
        {
            var numbers = finalOperation.Split("*");
            return long.Parse(numbers[0]) * long.Parse(numbers[1]);
        }
        else
        {
            var numbers = finalOperation.Split("+");
            return long.Parse(numbers[0]) + long.Parse(numbers[1]);
        }
    }
}

public class Monkey
{
    public List<long> Items { get; set; }= new List<long>();
    public string Operation { get; set; } = string.Empty;
    public int TestDivisibleBy { get; set; }
    public int MonkeyIfTestTrue { get; set; }
    public int MonkeyIfTestFalse { get; set; }
    public long InspectionCount { get; set; }
}