namespace AdventOfCode._2019;

public class Day6 : AdventBase
{
    private readonly string[] input;

    public Day6() : base(2019, 6)
    {
        input = Input;
    }

    public override long PartOne()
    {
        Dictionary<string, Planet> planets = CreatePlanetSystem();

        long orbits = 0;

        foreach (KeyValuePair<string, Planet> kvp in planets)
        {
            string planet = kvp.Key;
            while (planets[planet].Name != "COM")
            {
                orbits++;
                planet = planets[planet].Previous.Name;
            }
        }

        return orbits;
    }

    public override long PartTwo()
    {
        Dictionary<string, Planet> planets = CreatePlanetSystem();
        return ShortestTransfer(planets, planets["YOU"], planets["SAN"]) - 1;
    }

    private long ShortestTransfer(Dictionary<string, Planet> map, Planet startPoint, Planet target)
    {
        PriorityQueue<Planet, int> pq = new();
        List<Planet> tmp = startPoint.GetNextPossible();
        foreach (Planet planet in tmp)
        {
            pq.Enqueue(planet, 0);
        }

        HashSet<Planet> seen = new();

        while (pq.TryDequeue(out Planet curPlanet, out int priority))
        {
            if (curPlanet.Equals(target))
            {
                return priority;
            }

            List<Planet> next = curPlanet.GetNextPossible();
            foreach (Planet planet in next)
            {
                if (!seen.Contains(planet))
                {
                    seen.Add(planet);
                    pq.Enqueue(planet, priority + 1);
                }
            }
        }

        return -1;
    }

    private Dictionary<string, Planet> CreatePlanetSystem()
    {
        HashSet<string> allPlanets = GetAllPlanets();
        Dictionary<string, Planet> planets = new();

        foreach (string planet in allPlanets)
        {
            planets.Add(planet, new Planet(planet));
        }

        foreach (string line in this.input)
        {
            string[] p = line.Split(")");
            Planet parent = planets[p[0]];
            Planet child = planets[p[1]];
            parent.AddNext(child);
            child.Previous = parent;
        }

        return planets;
    }

    public HashSet<string> GetAllPlanets()
    {
        HashSet<string> planets = new();
        foreach (string line in this.input)
        {
            string[] p = line.Split(")");
            planets.Add(p[0]);
            planets.Add(p[1]);
        }

        return planets;
    }

    private class Planet
    {
        private readonly List<Planet> neighbors;

        private Planet previous;
        
        public Planet Previous
        {
            get => previous;
            set
            {
                neighbors.Add(value);
                previous = value;
            }
        }

        public readonly string Name;

        public Planet(string name)
        {
            this.Name = name;
            this.neighbors = new();
        }

        public void AddNext(Planet p)
        {
            neighbors.Add(p);
        }

        public List<Planet> GetNextPossible()
        {
            return this.neighbors;
        }

        public static bool operator ==(Planet p1, Planet p2)
        {
            return p1.Name == p2.Name;
        }

        public static bool operator !=(Planet p1, Planet p2)
        {
            return p1.Name != p2.Name;
        }

        protected bool Equals(Planet other)
        {
            return Name == other.Name;
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
            return Name.GetHashCode();
        }
    }
}