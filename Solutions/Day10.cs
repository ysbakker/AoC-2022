namespace Solutions;

public class Day10 : Solution
{
    public Day10(string[] input, int day) : base(input, day)
    {
    }

    public override void RunPartOne()
    {
        Run(1);
    }

    public override void RunPartTwo()
    {
        Run(2);
    }

    public void Run(int part)
    {
        int currentCycle = 0;
        int currentInstruction = 0;
        int instructionCycles = 0;
        int x = 1;

        var crtOutput = string.Empty;
        int totalSignalStrength = 0;
        
        while (currentInstruction < Input.Length-1)
        {
            currentCycle++;
            instructionCycles++;
            
            // Signal strength calculation
            if ((currentCycle - 20) % 40 == 0) totalSignalStrength += currentCycle * x;

            // CRT output
            var crtPosition = (currentCycle - 1) % 40;
            if (crtPosition == 0) crtOutput += "\n";
            if (crtPosition >= x - 1 && crtPosition <= x + 1) crtOutput += "█";
            else crtOutput += " ";

            if (Input[currentInstruction][0] == 'a')
            {
                if (instructionCycles < 2) continue;
                x += int.Parse(Input[currentInstruction].Split(" ")[1]);
            }

            instructionCycles = 0;
            currentInstruction++;
        }

        if (part == 1) Result(totalSignalStrength);
        else Result(crtOutput);
    }
}