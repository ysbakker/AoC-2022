using System.Text.Json.Nodes;

namespace Solutions;

public class Day13 : Solution
{
    public Day13(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        int sum = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            var one = JsonNode.Parse(Input[i])!;
            var two = JsonNode.Parse(Input[++i])!;
            i++;
            if (CompareNodes(one, two) > 0)
            {
                sum += i / 3 + 1;
            }
        }

        Result(sum);
    }
    
    public override void RunPartTwo()
    {
        List<JsonNode> nodes = new List<JsonNode>();
        for (int i = 0; i < Input.Length; i++)
        {
            nodes.Add(JsonNode.Parse(Input[i])!);
            nodes.Add(JsonNode.Parse(Input[++i])!);
            i++;
        }

        var controlPacket1 = JsonNode.Parse("[[2]]")!;
        var controlPacket2 = JsonNode.Parse("[[6]]")!;
        nodes.Add(controlPacket1);
        nodes.Add(controlPacket2);
        
        nodes.Sort((node1, node2) => CompareNodes(node2, node1));

        Result((nodes.IndexOf(controlPacket1) + 1)*(nodes.IndexOf(controlPacket2) + 1));
    }
    
    public int CompareNodes(JsonNode one, JsonNode two)
    {
        if (one is JsonValue && two is JsonValue)
        {
            return two.GetValue<int>() - one.GetValue<int>();
        }

        JsonArray arrayOne = one as JsonArray ?? new JsonArray(one.GetValue<int>());
        JsonArray arrayTwo = two as JsonArray ?? new JsonArray(two.GetValue<int>());

        for (int i = 0; i < Math.Min(arrayOne.Count, arrayTwo.Count); i++)
        {
            var result = CompareNodes(arrayOne[i]!, arrayTwo[i]!);
            if (result != 0) return result;
        }
        
        if (arrayOne.Count > arrayTwo.Count) return -1;
        if (arrayOne.Count < arrayTwo.Count) return 1;
        return 0;
    }

}