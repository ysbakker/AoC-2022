namespace Solutions;

public class Day18 : Solution
{
    private readonly (int dx, int dy, int dz)[] _neighbors =
    {
        (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1)
    };

    public Day18(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        var totalSurface = 0;

        HashSet<(int x, int y, int z)> cubes = new HashSet<(int x, int y, int z)>();
        foreach (var line in Input)
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            (int x, int y, int z) cube = (coords[0], coords[1], coords[2]);

            totalSurface += 6 - 2 * CountNeighbors(cubes, cube);

            cubes.Add(cube);
        }

        Result(totalSurface);
    }

    public override void RunPartTwo()
    {
        var surfaceArea = 0;
        HashSet<(int x, int y, int z)> cubes = new HashSet<(int x, int y, int z)>();
        foreach (var line in Input)
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            cubes.Add((coords[0], coords[1], coords[2]));
        }

        (int x, int y, int z) min = (-1, -1, -1);
        (int x, int y, int z) max = (cubes.Max(c => c.x) + 1, cubes.Max(c => c.y) + 1, cubes.Max(c => c.z) + 1);
        
        Queue<(int x, int y, int z)> queue = new Queue<(int x, int y, int z)>();
        HashSet<(int x, int y, int z)> visited = new HashSet<(int x, int y, int z)>();
        queue.Enqueue(min);

        while (queue.Any())
        {
            var space = queue.Dequeue();
            visited.Add(space);
            foreach (var (dx, dy, dz) in _neighbors)
            {
                (int x, int y, int z) neighbor = (space.x + dx, space.y + dy, space.z + dz);
                if (cubes.Contains(neighbor)) surfaceArea++;
                else if (!visited.Contains(neighbor) 
                         && neighbor.x <= max.x && neighbor.y <= max.y && neighbor.z <= max.z
                         && neighbor.x >= min.x && neighbor.y >= min.y && neighbor.z >= min.z)
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }

        }
        
        Result(surfaceArea);
    }

    private int CountNeighbors(HashSet<(int x, int y, int z)> cubes, (int x, int y, int z) cube)
    {
        var adjacentCubes = 0;
        foreach (var (dx, dy, dz) in _neighbors)
        {
            if (cubes.Contains((cube.x + dx, cube.y + dy, cube.z + dz))) adjacentCubes++;
        }

        return adjacentCubes;
    }
}