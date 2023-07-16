namespace AdventOfCode._2019 {

    using Point = (int x, int y);

    public class Day3 : IAdventDay {
        string[] input;

        public Day3() {
            this.input = Helper.GetInput(3);
        }

        public long PartOne() {
            Dictionary<Point, int> wireOne = SimulateWire(input[0]);
            Dictionary<Point, int> wireTwo = SimulateWire(input[1]);
            List<Point> intersects = wireOne.Keys.Intersect(wireTwo.Keys).ToList();
            Point closest = intersects.OrderBy(p => Math.Abs(p.x) + Math.Abs(p.y)).First();
            int distance = Math.Abs(closest.x) + Math.Abs(closest.y);
            return distance;
        }

        public long PartTwo() {
            Dictionary<Point, int> wireOne = SimulateWire(input[0]);
            Dictionary<Point, int> wireTwo = SimulateWire(input[1]);
            List<Point> intersects = wireOne.Keys.Intersect(wireTwo.Keys).ToList();
            List<int> steps = intersects.Select(p => wireOne[p] + wireTwo[p]).OrderBy(i => i).ToList();
            return steps.Min();
        }

        Dictionary<Point, int> SimulateWire(string wireInstructions) {
            string[] instructions = wireInstructions.Split(',');
            Dictionary<Point, int> wire = new();
            int stepCount = 0;
            Point currentPosition = (0, 0);
            for(int i = 0; i < instructions.Length; i++) {
                int length = int.Parse(instructions[i].Substring(1));
                int xOffset = 0;
                int yOffset = 0;

                switch(instructions[i][0]) {
                    case 'R':
                        xOffset = 1;
                        break;
                    case 'L':
                        xOffset = -1;
                        break;
                    case 'U':
                        yOffset = 1;
                        break;
                    case 'D':
                        yOffset = -1;
                        break;
                }

                for(int counter = 0; counter < length; counter++) {
                    currentPosition = (currentPosition.x + xOffset, currentPosition.y + yOffset);
                    if(!wire.ContainsKey(currentPosition)) {
                        wire.Add(currentPosition, stepCount++);
                    }
                }
            }
            return wire;
        }
    }
}
