namespace AdventOfCode._2023;

public class Box
{
    private readonly List<Lens> lensSlots = [];

    public void OverwriteLens(Lens lensToInsert)
    {
        for (int i = 0; i < lensSlots.Count; i++)
        {
            if (lensSlots[i].Label != lensToInsert.Label) continue;
            lensSlots[i] = lensToInsert;
            return;
        }
        lensSlots.Add(lensToInsert);
    }

    public void RemoveLens(Lens lensToRemove)
    {
        List<Lens> lensInSlots = lensSlots.Where(l => l.Label == lensToRemove.Label).ToList();
        if (lensInSlots.Count == 1)
        {
            lensSlots.Remove(lensInSlots[0]);
        }
    }

    public long GetFocusingPower(int boxNumber)
    {
        boxNumber++;
        long sum = 0;
        for (int i = 0; i < lensSlots.Count; i++)
        {
            sum += boxNumber * (i + 1) * lensSlots[i].FocalLength;
        }

        return sum;
    }
}