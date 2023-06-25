namespace AdventOfCode._2021 {
    public class Day25 : IAdventDay {
        string[] input;

        public Day25() {
            this.input = Helper.GetInput(25);
        }

        public long PartOne() {
            char[,] map = ParseInput();
            PrintArray(map);
            return 0;
        }

        public long PartTwo() {
            return 0;
        }

        char[,] ParseInput() {
            char[,] map = new char[input[0].Length, input.Length];
            for (int y = 0; y < map.GetLength(1); y++) {
                for (int x = 0; x < input.GetLength(0); x++) {
                    map[x, y] = input[y][x];
                }
            }
            return map;
        }

        void PrintArray(char[,] map) {
            string output = string.Empty;
            for (int y = 0; y < map.GetLength(1); ++y) {
                string part = string.Empty;
                for (int x = 0; x < map.GetLength(0); x++) {
                    part += map[x, y];
                }
                output += part + "\n";
            }
            Console.WriteLine(output);
        }
    }
}
