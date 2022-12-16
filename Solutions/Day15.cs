namespace Solutions;

public class Day15 : Solution
{
    public Day15(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        List<Sensor> sensors = new List<Sensor>();
        var row = 2_000_000;
        foreach (var sensor in Input)
        {
            var sensorPosition = sensor[12..sensor.IndexOf(':')].Replace("y=", "").Split(',');
            var beaconPosition = sensor[(sensor.LastIndexOf('x') + 2)..].Replace("y=", "").Split(',');
            sensors.Add(new Sensor()
            {
                Coordinates = (int.Parse(sensorPosition[0]), int.Parse(sensorPosition[1])),
                Beacon = (int.Parse(beaconPosition[0]), int.Parse(beaconPosition[1]))
            });
            
        }

        HashSet<(int, int)> positions = new HashSet<(int, int)>();
        foreach (var sensor in sensors)
        {
            if (sensor.Coordinates.y > row
                    ? sensor.Coordinates.y - row > row
                    : sensor.Coordinates.y + row < row) continue;
            var rowDistance = row > sensor.Coordinates.y ? sensor.Coordinates.y + sensor.Radius - row : row - sensor.Coordinates.y - sensor.Radius;
            for (var x = sensor.Coordinates.x - rowDistance; x < sensor.Coordinates.x + rowDistance; x++) positions.Add((x, row));
        }

        Result(positions.Count);
    }

    public override void RunPartTwo()
    {
        throw new NotImplementedException();
    }
}

public class Sensor
{
    public (int x, int y) Beacon { get; set; }
    public (int x, int y) Coordinates { get; set; }
    public int Radius => Math.Abs(Coordinates.x - Beacon.x) + Math.Abs(Coordinates.y - Beacon.y);

    public bool IsInRange(int x, int y)
    {
        return Math.Abs(Coordinates.x - x) + Math.Abs(Coordinates.y - y) <= Radius;
    }
}