using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;

namespace Solutions;

public class Day02Solution : Solution
{
    public Day02Solution(string[] input) : base(input)
    {
    }

    public override void RunPartOne()
    {
        Console.WriteLine(CalculateScore(Input, 1));
    }

    public override void RunPartTwo()
    {
        Console.WriteLine(CalculateScore(Input, 2));
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

        if (opponentShape == Shapes.Paper && selectedShape == Shapes.Scissors ||
            opponentShape == Shapes.Rock && selectedShape == Shapes.Paper ||
            opponentShape == Shapes.Scissors && selectedShape == Shapes.Rock)
        {
            score += 6;
        }

        if (opponentShape == selectedShape)
        {
            score += 3;
        }

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
        {
            return opponentShape switch
            {
                Shapes.Rock => Shapes.Scissors,
                Shapes.Paper => Shapes.Rock,
                Shapes.Scissors => Shapes.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(opponentShape), opponentShape, null)
            };
        }

        if (desiredOutcome == "Y") // draw
        {
            return opponentShape;
        }

        if (desiredOutcome == "Z") // win
        {
            return opponentShape switch
            {
                Shapes.Rock => Shapes.Paper,
                Shapes.Paper => Shapes.Scissors,
                Shapes.Scissors => Shapes.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(opponentShape), opponentShape, null)
            };
        }

        throw new Exception("Invalid outcome");
    }
}

public enum Shapes
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}