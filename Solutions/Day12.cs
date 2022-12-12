namespace Solutions;

public class Day12 : Solution
{
    private readonly (int dx, int dy)[] _neighbors = { (0, 1), (0, -1), (1, 0), (-1, 0) };

    public Day12(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        (int x, int y)? startPosition = null;
        (int x, int y)? endPosition = null;

        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i].Contains('S'))
            {
                startPosition = (Input[i].IndexOf('S'), i);
                Input[i] = Input[i].Replace('S', 'a');
            }
            if (Input[i].Contains('E'))
            {
                endPosition = (Input[i].IndexOf('E'), i);
                Input[i] = Input[i].Replace('E', 'z');
            }
        }

        int numberOfNodes = Input.Length * Input[0].Length;
        int[] previous = new int[numberOfNodes];
        bool[] visited = new bool[numberOfNodes];
        Queue<(int x, int y)> placesToVisit = new();
        placesToVisit.Enqueue(startPosition!.Value);
        visited[PositionToIndex(startPosition.Value)] = true;

        while (placesToVisit.Any())
        {
            var pos = placesToVisit.Dequeue();

            foreach (var (dx, dy) in _neighbors)
            {
                (int x, int y) newPos = (pos.x + dx, pos.y + dy);
                if (!IsValidLocation(newPos) || visited[PositionToIndex(newPos)] || !CanReach(newPos, pos)) continue;
                
                placesToVisit.Enqueue(newPos);
                visited[PositionToIndex(newPos)] = true;
                previous[PositionToIndex(newPos)] = PositionToIndex(pos);
            }
            if (pos == endPosition) break;
        }

        Result(CalculateSteps(PositionToIndex(startPosition.Value), PositionToIndex(endPosition!.Value), previous));
    }

    public override void RunPartTwo()
    {
        (int x, int y)? startPosition = null;
        (int x, int y)? endPosition = null;

        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i].Contains('S'))
            {
                Input[i] = Input[i].Replace('S', 'a');
            }
            if (Input[i].Contains('E'))
            {
                startPosition = (Input[i].IndexOf('E'), i);
                Input[i] = Input[i].Replace('E', 'z');
            }
        }
        
        int numberOfNodes = Input.Length * Input[0].Length;
        int[] previous = new int[numberOfNodes];
        bool[] visited = new bool[numberOfNodes];
        Queue<(int x, int y)> placesToVisit = new();
        placesToVisit.Enqueue(startPosition!.Value);
        visited[PositionToIndex(startPosition.Value)] = true;

        while (placesToVisit.Any())
        {
            var pos = placesToVisit.Dequeue();

            foreach (var (dx, dy) in _neighbors)
            {
                (int x, int y) newPos = (pos.x + dx, pos.y + dy);
                
                if (!IsValidLocation(newPos) || visited[PositionToIndex(newPos)] || !CanReach(pos, newPos)) continue;
                
                placesToVisit.Enqueue(newPos);
                visited[PositionToIndex(newPos)] = true;
                previous[PositionToIndex(newPos)] = PositionToIndex(pos);
                if (!endPosition.HasValue && Input[newPos.y][newPos.x] == 'a') endPosition = newPos;
            }
            if (pos == endPosition) break;
        }

        Result(CalculateSteps(PositionToIndex(startPosition.Value), PositionToIndex(endPosition!.Value), previous));
    }
    
    private bool CanReach((int x, int y) newPos, (int x, int y) pos)
    {
        return Input[newPos.y][newPos.x] - Input[pos.y][pos.x] < 2;
    }

    private int CalculateSteps(int start, int end, int[] previous)
    {
        var steps = 0;
        var current = end;
        while (current != start)
        {
            current = previous[current];
            steps++;
        }

        return steps;
    }

    private bool IsValidLocation((int x, int y) pos)
    {
        return pos.x >= 0 && pos.x < Input[0].Length && pos.y >= 0 && pos.y < Input.Length; 
    }

    private int PositionToIndex((int x, int y) pos)
    {
        return pos.y * Input[0].Length + pos.x;
    }
}
