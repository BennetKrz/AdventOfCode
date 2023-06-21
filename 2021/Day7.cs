namespace AdventOfCode._2021 {
    public class Day7 : IAdventDay {
        string[] input_string;
        int[] input;
        public Day7() {
            this.input_string = Helper.GetInputOneLine(7).Split(',');
            input = new int[input_string.Length];
            for (int i = 0; i < this.input_string.Length; i++) {
                int.TryParse(input_string[i], out input[i]);
            }
        }

        public long PartOne() {
            Dictionary<int, int> FuelForPosition = new();
            for (int i = 0; i < input.Max(); i++) {
                List<int> FuelNeededAllCrabs = new();
                for (int c = 0; c < input.Length; c++) {
                    FuelNeededAllCrabs.Add(CalculateFuel(input[c], i));
                }
                FuelForPosition.Add(i, FuelNeededAllCrabs.Sum());
            }
            return FuelForPosition.Values.Min();
        }

        public long PartTwo() {
            Dictionary<int, int> FuelForPosition = new();
            for (int i = 0; i < input.Max(); i++) {
                List<int> FuelNeededAllCrabs = new();
                for (int c = 0; c < input.Length; c++) {
                    FuelNeededAllCrabs.Add(CalculateFuelPartTwo(input[c], i));
                }
                FuelForPosition.Add(i, FuelNeededAllCrabs.Sum());
            }
            return FuelForPosition.Values.Min();
        }

        int CalculateFuel(int startPosition, int targetPosition) {
            return Math.Abs(startPosition - targetPosition);
        }

        int CalculateFuelPartTwo(int startPosition, int targetPosition) {
            int delta = CalculateFuel(startPosition, targetPosition);
            int fuel = 0;
            for (int i = startPosition; i < startPosition + delta; i++) {
                fuel += i - startPosition + 1;
            }
            return fuel;
        }
    }
}
