namespace Solutions;

public class Day02 : Solution
{
    // shape => [winning counter, losing counter]
    Dictionary<Shapes, Shapes[]> shapes = new()
    {
        { Shapes.Rock, new[] { Shapes.Paper, Shapes.Scissors } },
        { Shapes.Paper, new[] { Shapes.Scissors, Shapes.Rock } },
        { Shapes.Scissors, new[] { Shapes.Rock, Shapes.Paper } }
    };

    public Day02(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        Result(CalculateScore(Input, 1));
    }

    public override void RunPartTwo()
    {
        Result(CalculateScore(Input, 2));
    }

    private int CalculateScore(string[] input, int part)
    {
        int score = 0;
        foreach (var s in input)
        {
            var parts = s.Split(" ");
            score += part == 1
                ? CalculateScore(TranslateToShape(parts[1]), TranslateToShape(parts[0]))
                : CalculateScore(DetermineOutcomeShape(TranslateToShape(parts[0]), parts[1]),
                    TranslateToShape(parts[0]));
        }

        return score;
    }

    private int CalculateScore(Shapes selectedShape, Shapes opponentShape)
    {
        var score = (int)selectedShape;

        if (shapes[selectedShape][1] == opponentShape) score += 6;

        if (opponentShape == selectedShape) score += 3;

        return score;
    }

    private Shapes TranslateToShape(string input)
    {
        return input switch
        {
            "A" or "X" => Shapes.Rock,
            "B" or "Y" => Shapes.Paper,
            "C" or "Z" => Shapes.Scissors,
            _ => throw new Exception("Invalid input")
        };
    }

    private Shapes DetermineOutcomeShape(Shapes opponentShape, string desiredOutcome)
    {
        if (desiredOutcome == "X") // lose
            return shapes[opponentShape][1];

        if (desiredOutcome == "Y") // draw
            return opponentShape;

        if (desiredOutcome == "Z") // win
            return shapes[opponentShape][0];
        
        throw new Exception("Invalid outcome");
    }
}

public enum Shapes
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}