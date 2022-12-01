using System.Text;
using Solutions;

var argument = args[0].Split("-");
var day = int.Parse(argument[0]);
var part = int.Parse(argument[1]);

var input = File.ReadAllLines("input.txt");

Solution solution = SolutionsFactory.CreateSolution(day, input);

if (part == 1)
{
    solution.RunPartOneBenchmarked();
}
else if (part == 2)
{
    solution.RunPartTwoBenchmarked();
}
