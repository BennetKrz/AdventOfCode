namespace AdventOfCode._2019 {

    public class Day3 : IAdventDay {
        string[] input;

        public Day3() {
            this.input = Helper.GetInput(3);
        }

        public long PartOne() {
            Dictionary<(int x, int y), int> wireOne = SimulateWire(input[0]);
            Dictionary<(int x, int y), int> wireTwo = SimulateWire(input[1]);
            List<(int x, int y)> intersects = wireOne.Keys.Intersect(wireTwo.Keys).ToList();
            (int x, int y) closest = intersects.OrderBy(p => Math.Abs(p.x) + Math.Abs(p.y)).First();
            int distance = Math.Abs(closest.x) + Math.Abs(closest.y);
            return distance;
        }

        public long PartTwo() {
            Dictionary<(int x, int y), int> wireOne = SimulateWire(input[0]);
            Dictionary<(int x, int y), int> wireTwo = SimulateWire(input[1]);
            List<(int x, int y)> intersects = wireOne.Keys.Intersect(wireTwo.Keys).ToList();
            List<int> steps = intersects.Select(p => wireOne[p] + wireTwo[p]).OrderBy(i => i).ToList();
            return steps.Min();
        }

        Dictionary<(int x, int y), int> SimulateWire(string wireInstructions) {
            string[] instructions = wireInstructions.Split(',');
            Dictionary<(int x, int y), int> wire = new();
            int stepCount = 0;
            (int x, int y) currentPosition = (0, 0);
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
                    currentPosition.x = currentPosition.x + xOffset; 
                    currentPosition.y =  currentPosition.y + yOffset;
                    if(!wire.ContainsKey(currentPosition)) {
                        wire.Add(currentPosition, stepCount++);
                    }
                }
            }
            return wire;
        }
    }
}
