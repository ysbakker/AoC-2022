namespace Solutions;

public class Day04 : Solution
{
    public Day04(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        Result(CompareSections((sectionOne, sectionTwo) =>
            !sectionOne.Except(sectionTwo).Any() || !sectionTwo.Except(sectionOne).Any()));
    }

    public override void RunPartTwo()
    {
        Result(CompareSections((sectionOne, sectionTwo) => sectionOne.Intersect(sectionTwo).Any()));
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