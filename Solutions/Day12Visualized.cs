using Spectre.Console;

namespace Solutions;

public class Day12Visualized : Solution
{
    private readonly (int dx, int dy)[] _neighbors = { (0, 1), (0, -1), (1, 0), (-1, 0) };

    public Day12Visualized(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        Console.CursorVisible = false;
        AnsiConsole.Clear();
        (int x, int y)? startPosition = null;
        (int x, int y)? endPosition = null;

        for (var i = 0; i < Input.Length; i++)
        {
            AnsiConsole.MarkupLine($"[gray]{Input[i]}[/]");
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
            Task.Delay(1).Wait();
            Console.SetCursorPosition(pos.x, pos.y);
            var character = Input[pos.y][pos.x];
            AnsiConsole.Markup($"[{CharacterToColor(character)}]{character}[/]");

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

        CalculateSteps(PositionToIndex(startPosition.Value), PositionToIndex(endPosition!.Value), previous);
        Console.ReadKey();
    }

    public override void RunPartTwo()
    {
        Console.CursorVisible = false;
        AnsiConsole.Clear();
        (int x, int y)? startPosition = null;
        (int x, int y)? endPosition = null;

        for (var i = 0; i < Input.Length; i++)
        {
            AnsiConsole.MarkupLine($"[gray]{Input[i]}[/]");
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
            Task.Delay(1).Wait();
            Console.SetCursorPosition(pos.x, pos.y);
            var character = Input[pos.y][pos.x];
            AnsiConsole.Markup($"[{CharacterToColor(character)}]{character}[/]");
            
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

        CalculateSteps(PositionToIndex(startPosition.Value), PositionToIndex(endPosition!.Value), previous);
        Console.ReadKey();
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
            int x = current % Input[0].Length;
            int y = (int)Math.Ceiling(current / (double)Input[0].Length - 1);
            Console.SetCursorPosition(x, y);
            var character = Input[y][x];
            AnsiConsole.Markup($"[bold white]{character}[/]");
            Task.Delay(10).Wait();

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

    private string CharacterToColor(char c)
    {
        return c switch
        {
            'a' => "darkseagreen4",
            'b' => "green",
            'c' => "darkolivegreen3",
            > 'c' and <= 'h' => "deepskyblue4_1",
            > 'h' and <= 'n' => "dodgerblue2",
            > 'n' and <= 'p' => "dodgerblue2",
            > 'p' and <= 'v' => "purple4",
            > 'v' and <= 'z' => "mediumvioletred",
            _ => "gray"
        };
    }
}
