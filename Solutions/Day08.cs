namespace Solutions;

public class Day08 : Solution
{
    public Day08(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        int treesVisible = (Input.Length - 1) * 4;
        for (int y = 1; y < Input.Length - 1; y++)
        {
            for (int x = 1; x < Input[y].Length - 1; x++)
            {
                var isTreeVisible = IsTreeVisibleFromDirection(y, x, -1, 0) ||
                                    IsTreeVisibleFromDirection(y, x, 0, 1) ||
                                    IsTreeVisibleFromDirection(y, x, 1, 0) ||
                                    IsTreeVisibleFromDirection(y, x, 0, -1); 
                if (isTreeVisible) treesVisible++;
            }
        }

        Result(treesVisible);
    }

    public override void RunPartTwo()
    {
        int highestScore = 0;
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[y].Length; x++)
            {
                int totalScenicScore = DetermineScenicScoreForDirection(y, x, -1, 0) *
                                       DetermineScenicScoreForDirection(y, x, 0, 1) *
                                       DetermineScenicScoreForDirection(y, x, 1, 0) *
                                       DetermineScenicScoreForDirection(y, x, 0, -1);
                highestScore = totalScenicScore > highestScore ? totalScenicScore : highestScore;
            }
        }

        Result(highestScore);
    }

    private bool IsTreeVisibleFromDirection(int y, int x, int dy, int dx)
    {
        int curX = x + dx;
        int curY = y + dy;

        for (; curY >= 0 && curY < Input.Length && curX >= 0 && curX < Input[y].Length; curX += dx, curY += dy)
        {
            if (Input[y][x] <= Input[curY][curX]) return false;
        }

        return true;
    }

    private int DetermineScenicScoreForDirection(int y, int x, int dy, int dx)
    {
        int curX = x + dx;
        int curY = y + dy;

        var score = 0;
        for (; curY >= 0 && curY < Input.Length && curX >= 0 && curX < Input[y].Length; curX += dx, curY += dy)
        {
            score++;
            if (Input[y][x] <= Input[curY][curX]) break;
        }

        return score;
    }
}
