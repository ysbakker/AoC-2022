namespace Solutions;

public class Day14 : Solution
{
    public Day14(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        SimulateSand(1);
    }

    public override void RunPartTwo()
    {
        SimulateSand(2);
    }

    public void SimulateSand(int part)
    {
        var (blocked, lowestRock) = CreateRocks();
        var floor = lowestRock + 2;
        var count = 0;

        while (!blocked.Contains((500, 0)))
        {
            // Create new sand unit
            (int x, int y) sandPosition = (500, 0);

            while (true)
            {
                (int x, int y) nextSandPosition = (sandPosition.x, ++sandPosition.y);
                if (nextSandPosition.y > lowestRock && part == 1)
                {
                    Result(count);
                    return;
                }

                // Position is not blocked, continue falling
                if (!blocked.Contains(nextSandPosition) && nextSandPosition.y < floor) continue;

                // Check diagonal left
                nextSandPosition = (sandPosition.x - 1, sandPosition.y);
                if (!blocked.Contains(nextSandPosition) && nextSandPosition.y < floor)
                {
                    sandPosition.x--;
                    continue;
                }

                // Check diagonal right
                nextSandPosition = (sandPosition.x + 1, sandPosition.y);
                if (!blocked.Contains(nextSandPosition) && nextSandPosition.y < floor)
                {
                    sandPosition.x++;
                    continue;
                }

                // Can't fall any further, this is the final sand position
                count++;
                sandPosition.y--;
                blocked.Add(sandPosition);
                break;
            }
        }

        Result(count);
    }

    public (HashSet<(int x, int y)>, int lowest) CreateRocks()
    {
        int lowestRock = 0;
        var rocks = new HashSet<(int x, int y)>();
        foreach (var shape in Input)
        {
            var segments = shape.Split("->", StringSplitOptions.TrimEntries);
            var segmentPositions = new (int x, int y)[segments.Length];

            for (int i = 0; i < segments.Length; i++)
            {
                var coords = segments[i].Split(",");
                segmentPositions[i] = (int.Parse(coords[0]), int.Parse(coords[1]));
            }

            for (int i = 0; i < segmentPositions.Length - 1; i++)
            {
                var (fromx, fromy) = segmentPositions[i];
                var (tox, toy) = segmentPositions[i + 1];

                lowestRock = Math.Max(lowestRock, Math.Max(fromy, toy));
                IEnumerable<(int x, int y)> currentRocks = null!;
                if (fromx < tox) currentRocks = Enumerable.Range(fromx, tox - fromx + 1).Select(x => (x, fromy));
                else if (fromx > tox) currentRocks = Enumerable.Range(tox, fromx - tox + 1).Select(x => (x, fromy));
                else if (fromy < toy) currentRocks = Enumerable.Range(fromy, toy - fromy + 1).Select(y => (fromx, y));
                else if (fromy > toy) currentRocks = Enumerable.Range(toy, fromy - toy + 1).Select(y => (fromx, y));

                foreach (var (x, y) in currentRocks) rocks.Add((x, y));
            }
        }

        return (rocks, lowestRock);
    }
}