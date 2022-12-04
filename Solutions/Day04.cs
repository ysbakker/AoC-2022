namespace Solutions;

public class Day04Solution : Solution
{
    public Day04Solution(string[] input) : base(input)
    {
    }

    public override void RunPartOne()
    {
        Console.WriteLine(CompareSections((sectionOne, sectionTwo) =>
            !sectionOne.Except(sectionTwo).Any() || !sectionTwo.Except(sectionOne).Any()));
    }

    public override void RunPartTwo()
    {
        Console.WriteLine(CompareSections((sectionOne, sectionTwo) => sectionOne.Intersect(sectionTwo).Any()));
    }

    private int CompareSections(Func<int[], int[], bool> comparer)
    {
        var count = 0;
        foreach (var s in Input)
        {
            var elves = s.Split(",");
            var sectionOne = elves[0].Split("-");
            var sectionTwo = elves[1].Split("-");
            var sectionOneRange = Enumerable
                .Range(int.Parse(sectionOne[0]), int.Parse(sectionOne[1]) - int.Parse(sectionOne[0]) + 1).ToArray();
            var sectionTwoRange = Enumerable
                .Range(int.Parse(sectionTwo[0]), int.Parse(sectionTwo[1]) - int.Parse(sectionTwo[0]) + 1).ToArray();
            if (comparer(sectionOneRange, sectionTwoRange)) count++;
        }

        return count;
    }
}