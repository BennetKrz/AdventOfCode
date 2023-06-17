namespace AdventOfCode._2022 {
    public class Day1 : IAdventDay {
        string[] input;
        public Day1() {
            this.input = Helper.GetInput(1);
        }
        public long PartOne() {

            List<int> CaloriesPerElf = new List<int>();
            int count = 0;
            for (int i = 0; i < input.Length; i++) {
                if (int.TryParse(input[i], out _)) {
                    count += Convert.ToInt32(input[i]);
                } else {
                    CaloriesPerElf.Add(count);
                    count = 0;
                }
            }

            return CaloriesPerElf.Max();
        }

        public long PartTwo() {

            List<int> CaloriesPerElf = new List<int>();
            int count = 0;
            for (int i = 0; i < input.Length; i++) {
                if (int.TryParse(input[i], out _)) {
                    count += Convert.ToInt32(input[i]);
                } else {
                    CaloriesPerElf.Add(count);
                    count = 0;
                }
            }

            return CaloriesPerElf.OrderByDescending(i => i).Take(3).Sum();
        }
    }
}
