namespace AdventOfCode._2025;

public class Day10 : AdventBase
{
    private List<string> input;

    public Day10() : base(2025, 10)
    {
        input = base.Input.ToList();
    }

    public override long PartOne()
    {
        List<Machine> machines = ParseInput();

        int sum = 0;
        for (int i = 0; i < machines.Count; i++)
        {
            sum += FindLeastButtonPresses(machines[i]);
        }

        return sum;
    }

    private int FindLeastButtonPresses(Machine machine)
    {
        bool[] targetState = machine.IndicatorLightsTarget;

        Queue<(bool[] state, Button button, int count)> queue = [];
        for (int i = 0; i < machine.Buttons.Count; i++)
        {
            queue.Enqueue((new bool[targetState.Length], machine.Buttons[i], 0));
        }

        while (queue.TryDequeue(out (bool[] state, Button button, int count) result))
        {
            bool[] newState = result.button.ToggleLights(result.state);
            if (newState.SequenceEqual(targetState))
            {
                return result.count + 1;
            }

            for (int i = 0; i < machine.Buttons.Count; i++)
            {
                queue.Enqueue((newState, machine.Buttons[i], result.count + 1));
            }
        }

        throw new Exception("No such state");
    }

    public override long PartTwo()
    {
        return long.MaxValue;
    }

    private List<Machine> ParseInput()
    {
        List<Machine> machines = [];
        var targetLights = ParseLights();
        var joltageTargets = ParseJoltages();
        var buttons = ParseButtons();

        machines.AddRange(targetLights.Select((t, i) => new Machine(t.ToArray(), buttons[i], joltageTargets[i].ToArray())));

        return machines;
    }

    private List<List<bool>> ParseLights()
    {
        List<List<bool>> result = [];
        foreach (var line in input)
        {
            List<bool> lightTargets = [];
            int curPos = 1;
            while (line[curPos] != ']')
            {
                if (line[curPos] == '.')
                {
                    lightTargets.Add(false);
                } else if (line[curPos] == '#')
                {
                    lightTargets.Add(true);
                }
                else
                {
                    throw new Exception("Invalid input");
                }

                curPos++;
            }

            result.Add(lightTargets);
        }

        return result;
    }

    private List<List<int>> ParseJoltages()
    {
        List<List<int>> result = [];

        foreach (var line in input)
        {
            result.Add(line.Substring(line.IndexOf('{')).Replace("}", "").Replace("{", "").Split(",").Select(int.Parse).ToList());
        }

        return result;
    }

    private List<List<Button>> ParseButtons()
    {
        List<List<Button>> result = [];

        foreach (var line in input)
        {
            List<Button> toggleIndices = [];

            int i1 = line.IndexOf(']') + 2;
            int i2 = line.IndexOf('{') - 1;
            string s = line.Substring(i1, i2 - i1);
            var s1 = s.Split(' ');
            foreach (var b in s1)
            {
                string n1 = b.Replace("(", "").Replace(")", "");
                toggleIndices.Add(new Button(n1.Split(',').Select(int.Parse).ToList()));
            }
            result.Add(toggleIndices);
        }

        return result;
    }
}

internal class Machine(bool[] lightTargets, List<Button> buttons, int[] joltageTarget)
{
    public bool[] IndicatorLightsTarget { get; init; } = lightTargets;
    public List<Button> Buttons { get; init; } = buttons;
    public int[] JoltageTarget { get; init; } = joltageTarget;
}

internal class Button(List<int> indicesToApply)
{
    public bool[] ToggleLights(bool[] lights)
    {
        bool[] copy = new bool[lights.Length];
        Array.Copy(lights, copy, lights.Length);
        foreach (var index in indicesToApply)
        {
            copy[index] = !copy[index];
        }
        return copy;
    }

    public int[] IncreaseCounters(int[] counters)
    {
        int[] copy = new int[counters.Length];
        Array.Copy(counters, copy, counters.Length);
        foreach (var index in indicesToApply)
        {
            copy[index]++;
        }
        return copy;
    }
}