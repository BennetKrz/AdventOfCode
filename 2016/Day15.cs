namespace AdventOfCode._2016;

using Disc = (byte Positions, byte CurrentPosition);

public class Day15 : AdventBase
{
    private readonly string[] input;

    public Day15() : base(2016, 15)
    {
        input = Input;
    }

    private readonly Dictionary<int, Disc[]> storagePartOne = [];
    private readonly Dictionary<int, Disc[]> storagePartTwo = [];

    public override long PartOne()
    {
        Disc[] discs = ParseInputPartOne();
        storagePartOne.Add(0, discs);
        CalculatePartOne(discs);
        int time = 0;
        while (true)
        {
            bool crash = false;
            for (int t = time; t < time + discs.Length; t++)
            {
                //check current
                if (storagePartOne[t][t - time].CurrentPosition != 0)
                {
                    crash = true;
                    break;
                }
            }

            if (crash)
            {
                time++;
            }
            else
            {
                break;
            }
        }

        return time - 1;
    }

    public override long PartTwo()
    {
        Disc[] discs = ParseInputPartTwo();
        storagePartTwo.Add(0, discs);
        CalculatePartTwo(discs);
        int time = 0;
        while (true)
        {
            bool crash = false;
            for (int t = time; t < time + discs.Length; t++)
            {
                //check current
                if (storagePartTwo[t][t - time].CurrentPosition != 0)
                {
                    crash = true;
                    break;
                }
            }

            if (crash)
            {
                time++;
            }
            else
            {
                break;
            }
        }

        return time - 1;
    }

    private void CalculatePartOne(Disc[] discs, int limit = 10_000_000)
    {
        for (int t = 1; t < limit; t++)
        {
            discs = storagePartOne[t - 1];
            Disc[] tmp = new Disc[discs.Length];
            Disc disc = (0, 0);
            for (int i = 0; i < discs.Length; i++)
            {
                byte Positions = discs[i].Positions;
                byte CurrentPosition = discs[i].CurrentPosition;
                CurrentPosition++;
                if (CurrentPosition >= Positions)
                {
                    CurrentPosition = 0;
                }

                disc = (Positions, CurrentPosition);
                tmp[i] = disc;
            }

            storagePartOne.Add(t, tmp);
        }
    }

    private void CalculatePartTwo(Disc[] discs, int limit = 10_000_000)
    {
        for (int t = 1; t < limit; t++)
        {
            discs = storagePartTwo[t - 1];
            Disc[] tmp = new Disc[discs.Length];
            Disc disc;
            for (int i = 0; i < discs.Length; i++)
            {
                byte positions = discs[i].Positions;
                byte currentPosition = discs[i].CurrentPosition;
                currentPosition++;
                if (currentPosition >= positions)
                {
                    currentPosition = 0;
                }

                disc = (positions, currentPosition);
                tmp[i] = disc;
            }

            storagePartTwo.Add(t, tmp);
        }
    }

    Disc[] ParseInputPartOne()
    {
        Disc[] discs = new Disc[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            string s = input[i];
            string[] tmp = s.Split("has ");
            byte positions = byte.Parse(tmp[1][..tmp[1].IndexOf(" ")]);
            byte startPosition = byte.Parse(s.Replace(".", "").Split("position ")[1]);
            discs[i] = (positions, startPosition);
        }

        return discs;
    }

    Disc[] ParseInputPartTwo()
    {
        Disc[] discs = new Disc[input.Length + 1];
        for (int i = 0; i < input.Length; i++)
        {
            string s = input[i];
            string[] tmp = s.Split("has ");
            byte positions = byte.Parse(tmp[1][..tmp[1].IndexOf(" ")]);
            byte startPosition = byte.Parse(s.Replace(".", "").Split("position ")[1]);
            discs[i] = (positions, startPosition);
        }

        discs[^1] = new Disc(11, 0);
        return discs;
    }
}