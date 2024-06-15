namespace AdventOfCode._2019;

public class Day6 : IAdventDay
{
    private readonly string[] input;

    public Day6()
    {
        input = Helper.GetInput(6);
    }

    public long PartOne()
    {
        Dictionary<string, string> objects = CreatePlanets();

        long orbits = 0;
        
        foreach (KeyValuePair<string,string> kvp in objects)
        {
            string planet = kvp.Key;
            orbits++;
            while (objects[planet] != "COM")
            {
                orbits++;
                planet = objects[planet];
            }
        }
        
        return orbits;
    }

    public long PartTwo()
    {
        return 0;
    }

    private Dictionary<string, string> CreatePlanets()
    {
        return input.Select(t => t.Split(")"))
                         .ToDictionary(objects => objects[1], objects => objects[0]);
    }
}