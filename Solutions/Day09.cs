namespace Solutions;

public class Day09 : Solution
{
    public Day09(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        SimulateRope(1);
    }
    
    public override void RunPartTwo()
    {
        SimulateRope(9);
    }

    public void SimulateRope(int amountOfKnots)
    {
        var head = (x: 0,y: 0);
        (int x, int y)[] knots = new (int x, int y)[amountOfKnots];
        
        HashSet<(int x, int y)> visited = new();
        
        foreach (var motion in Input)
        {
            var parts = motion.Split(" ");
            var direction = parts[0];
            var distance = int.Parse(parts[1]);

            for (int i = 0; i < distance; i++)
            {
                MoveHead(direction, ref head);
                for (int j = 0; j < knots.Length; j++)
                {
                    MoveTail(j == 0 ? head : knots[j-1], ref knots[j]);
                }
                visited.Add(knots[amountOfKnots - 1]);
            }
        }
        
        Result(visited.Count);
    }
    
    public void MoveHead(string direction, ref (int x, int y) head)
    {
        switch (direction)
        {
            case "U":
                head.y++;
                break;
            case "R":
                head.x++;
                break;
            case "D":
                head.y--;
                break;
            case "L":
                head.x--;
                break;
        }
    }

    public void MoveTail((int x, int y) head, ref (int x, int y) tail)
    {
        (int dx, int dy) = (head.x - tail.x, head.y - tail.y);
        if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2) return;

        if (dx > 0) tail.x++;
        else if (dx < 0) tail.x--;
        if (dy > 0) tail.y++;
        else if (dy < 0) tail.y--;
    }
}
