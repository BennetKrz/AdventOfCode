namespace AdventOfCode._2015 {
    public class Day2 : IAdventDay {
        string[] input;

        public Day2() {
            this.input = Helper.GetInput(2);
        }

        public long PartOne() {
            List<int> allArea = new();
            for (int i = 0; i < input.Length; i++) {
                int[] t = Array.ConvertAll(input[i].Split('x'), s => int.Parse(s));
                //t[0] = l; t[1] = w; t[2] = h;
                int[] allThreeSites = { t[0] * t[1], t[1] * t[2], t[2] * t[0] };
                allArea.Add(allThreeSites[0] * 2 + allThreeSites[1] * 2 + allThreeSites[2] * 2 + allThreeSites.Min());
            }
            return allArea.Sum();
        }

        public long PartTwo() {
            List<int> ribbonNeeded = new();
            for (int i = 0; i < input.Length; i++) {
                int[] t = Array.ConvertAll(input[i].Split('x'), s => int.Parse(s)).Order().ToArray();
                //t[0] = l; t[1] = w; t[2] = h;
                ribbonNeeded.Add(t[0] + t[0] + t[1] + t[1] + (t[0] * t[1] * t[2]));
            }
            return ribbonNeeded.Sum();
        }
    }
}
