using System.Xml.Schema;

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

    private Dictionary<string, Planet> CreatePlanets()
    {
        HashSet<string> allPlanets = GetAllPlanets();
        Dictionary<string, Planet> planets = new();

        foreach (string planet in allPlanets)
        {
            planets.Add(planet, new Planet(planet));
        }
        
        foreach (string line in this.input)
        {
            
        }

        return planets;
    }

    public HashSet<string> GetAllPlanets()
    {
        HashSet<string> planets = new();
        foreach (string line in this.input)
        {
            line.Split(")").Select(p => planets.Add(p));
        }

        return planets;
    }

    private class Planet
    {

        private List<Planet> next;
        public Planet previous { get; set; }
        public readonly string name;

        public Planet(string name)
        {
            this.name = name;
            this.next = new();
        }

        public void AddNext(Planet p)
        {
            next.Add(p);
        }

        public List<Planet> GetNextPossible()
        {
            return this.next;
        }

        public static bool operator ==(Planet p1, Planet p2)
        {
            return p1.name == p2.name;
        }
        
        public static bool operator !=(Planet p1, Planet p2)
        {
            return p1.name != p2.name;
        }
        
        protected bool Equals(Planet other)
        {
            return name == other.name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Planet)obj);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}
