namespace Solutions;

public class Day12 : Solution
{
    public Day12(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        (int x, int y) startPosition = (0, 0);
        List<List<(int x, int y)>> possiblePaths = new List<List<(int x, int y)>>();
        List<List<(int x, int y)>> pathsToEnd = new List<List<(int x, int y)>>();
        
        for (int i = 0; i < Input.Length; i++)
        {
            if (Input[i].IndexOf('S') >= 0)
            {
                startPosition = (Input[i].IndexOf('S'), i);
                break;
            }
        }
        
        possiblePaths.Add(new List<(int x, int y)> { startPosition });
        (int x, int y)[] permutations = new (int x, int y)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        
        while (true)
        {
            if (!possiblePaths.Any())
            {
                break;
            }
            foreach (var path in new List<List<(int x, int y)>>(possiblePaths))
            {
                var (curX, curY) = path[^1];
                foreach (var (dx, dy) in permutations)
                {
                    if (curX + dx < 0 || curX + dx >= Input[0].Length || curY + dy < 0 || curY + dy >= Input.Length)
                    {
                        // Out of bounds
                        continue;
                    }
                    
                    var currentHeight = Input[curY][curX];
                    if (currentHeight == 'S') currentHeight = 'a';
                    var nextHeight = Input[curY + dy][curX + dx];
                    if (nextHeight == 'E') nextHeight = 'z';

                    if (nextHeight - currentHeight < 0) continue;
                    
                    if (nextHeight - currentHeight <= 1 && !path.Contains((curX + dx, curY + dy)))
                    {
                        if (Input[curY + dy][curX + dx] == 'E')
                        {
                            pathsToEnd.Add(new List<(int x, int y)>(path) { (curX + dx, curY + dy) });
                        }
                        else
                        {
                            possiblePaths.Add(new List<(int x, int y)>(path) { (curX + dx, curY + dy) });
                        }
                    }
                }

                possiblePaths.Remove(path);
            }
        }

        Result(pathsToEnd.Select(p => p.Count).Min() - 1);
    }

    public override void RunPartTwo()
    {
        throw new NotImplementedException();
    }
}
