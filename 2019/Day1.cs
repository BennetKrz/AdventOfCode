namespace AdventOfCode._2019 {
    public class Day1 : IAdventDay {
        string[] input;

        public Day1() {
            this.input = Helper.GetInput(1);
        }

        public long PartOne() {
            int[] fuel = new int[input.Length];
            for (int i = 0; i < input.Length; i++) {
                int.TryParse(input[i], out fuel[i]);
                fuel[i] = fuel[i] / 3 - 2;
            }
            return fuel.Sum();
        }

        public long PartTwo() {
            int result = 0;
            int[] fuel = new int[input.Length];
            for (int i = 0; i < fuel.Length; i++) {
                fuel[i] = Convert.ToInt32(input[i]);
                fuel[i] = (fuel[i] / 3 - 2);
                result += fuel[i];
            }
            for (int i = 0; i < fuel.Length; i++) {
                while (fuel[i] > 0) {
                    if (fuel[i] / 3 - 2 >= 0) result += fuel[i] / 3 - 2;
                    fuel[i] = fuel[i] / 3 - 2;
                }
            }
            return result;
        }
    }
}
