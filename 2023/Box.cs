namespace AdventOfCode._2023;

public class Box
{
    private List<Lens> lensSlots = new();

    public void InsertLens(Lens lensToInsert)
    {
        
    }

    public void RemoveLens(Lens lensToRemove)
    {
        
        List<Lens> lensInSlots = lensSlots.Where(l => l.Label == lensToRemove.Label).ToList();
        if (lensInSlots.Count == 1)
        {
            lensSlots.Remove(lensInSlots[0]);
        }
    }
}