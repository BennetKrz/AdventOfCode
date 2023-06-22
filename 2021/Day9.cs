namespace AdventOfCode._2021 {
    public class Day9 : IAdventDay {
        string[] input;

        public Day9() {
            this.input = Helper.GetInput(9);
        }

        public long PartOne() {
            int[,] map = ParseInput();
            List<int> lowPoints = new();
            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input[0].Length; x++) {
                    int value = map[x, y];
                    //Alle felder in der Mitte
                    if (x > 0 && x < input[0].Length - 1 && y > 0 && y < input.Length - 1) {
                        if (value < map[x + 1, y] && value < map[x - 1, y] && value < map[x, y + 1] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    //Alle Ecken
                    if (x == 0 && y == 0) {
                        if (value < map[x + 1, y] && value < map[x, y + 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (x == input[0].Length - 1 && y == 0) {
                        if (value < map[x - 1, y] && value < map[x, y + 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (x == input[0].Length - 1 && y == input.Length - 1) {
                        if (value < map[x - 1, y] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (x == 0 && y == input.Length - 1) {
                        if (value < map[x + 1, y] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    //Jetzt die kanten:
                    if (x == 0) {
                        if (value < map[x + 1, y] && value < map[x, y + 1] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (y == 0) {
                        if (value < map[x + 1, y] && value < map[x - 1, y] && value < map[x, y + 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (x == input[0].Length - 1) {
                        if (value < map[x - 1, y] && value < map[x, y + 1] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                    if (y == input.Length - 1) {
                        if (value < map[x + 1, y] && value < map[x - 1, y] && value < map[x, y - 1]) {
                            lowPoints.Add(value);
                        }
                        continue;
                    }
                }
            }
            return lowPoints.Sum() + lowPoints.Count;
        }

        public record Coord(int Row, int Col);

        public long PartTwo() {
            List<List<Coord>> basins = new();

            return 0;
        }

        int[,] ParseInput() {
            int[,] map = new int[input[0].Length, input.Length];
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    map[j, i] = input[i][j] - '0';
                }
            }
            return map;
        }

        List<Coord> FindLowPoints() {
            List<Coord> points = new();
            int[,] map = ParseInput();
            for (int row = 0; row < input.Length; row++) {
                for (int col = 0; col < input[row].Length; col++) {
                    int height = map[col, row];
                    var neighbours =
                }
            }
        }

        List<Coord> Neighbours(int row, int col) {
            List<Coord> points = new() {
                new Coord(row-1, col),
                new Coord(row, col + 1),
                new Coord(row + 1, col),
                new Coord(row, col - 1),
            };

        }
    }
}
