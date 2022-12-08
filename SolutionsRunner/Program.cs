using System.Diagnostics;
using Solutions;

int day, part = 0;

if (args[0].Contains("-"))
{
    var argument = args[0].Split("-");
    day = int.Parse(argument[0]);
    part = int.Parse(argument[1]);
}
else
{
    day = int.Parse(args[0]);
}

var input = File.ReadAllLines("input.txt");

if (!input.Any())
{
    input = File.ReadAllLines($"Input/day{day:D2}.txt");
}

Solution solution = SolutionsFactory.CreateSolution(day, input);

if (part == 1)
{
    solution.RunPartOneBenchmarked();
}
else if (part == 2)
{
    solution.RunPartTwoBenchmarked();
}
else if (part == 0)
{
    solution.RunDay();
}
