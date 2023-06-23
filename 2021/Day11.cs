namespace AdventOfCode._2021 {
    public class Day11 : IAdventDay {
        string[] input;

        public Day11() {
            this.input = Helper.GetInput(11);
        }

        public long PartOne() {
            int flashed = 0;
            int[,] map = ParseInput();
            for (int step = 0; step < 100; step++) {
                //Alle um eins erhöhen:
                for (int y = 0; y < map.GetLength(0); y++) {
                    for (int x = 0; x < map.GetLength(1); x++) {
                        map[x, y]++;
                    }
                }
                //JEtzt für jeden gucken, ob er über 8 ist:
                List<(int x, int y)> allThatHaveToFlash = new();
                for (int y = 0; y < map.GetLength(0); y++) {
                    for (int x = 0; x < map.GetLength(1); x++) {
                        if (map[x, y] > 9) {
                            allThatHaveToFlash.Add((x, y));
                        }
                    }
                }
                //Jetzt alle in der Liste abarbeiten:
                for (int i = 0; i < allThatHaveToFlash.Count; i++) {
                    //Erst die umliegenden finden:
                    List<(int x, int y)> adjacent = GetAdjacent(allThatHaveToFlash[i], allThatHaveToFlash);
                    for (int a = 0; a < adjacent.Count; a++) {
                        map[adjacent[a].x, adjacent[a].y]++;
                        if (map[adjacent[a].x, adjacent[a].y] > 9) {
                            allThatHaveToFlash.Add(adjacent[a]);
                        }
                    }
                    map[allThatHaveToFlash[i].x, allThatHaveToFlash[i].y] = 0;
                    flashed++;
                }
            }
            return flashed;
        }

        public long PartTwo() {
            Dictionary<(int x, int y), bool> hasFlashed = new();
            int[,] map = ParseInput();
            for (int step = 0; step < 100_000; step++) {
                //Dictionary reseten:
                hasFlashed.Clear();
                //Alle um eins erhöhen:
                for (int y = 0; y < map.GetLength(0); y++) {
                    for (int x = 0; x < map.GetLength(1); x++) {
                        map[x, y]++;
                        hasFlashed.Add((x, y), false);
                    }
                }
                //JEtzt für jeden gucken, ob er über 8 ist:
                List<(int x, int y)> allThatHaveToFlash = new();
                for (int y = 0; y < map.GetLength(0); y++) {
                    for (int x = 0; x < map.GetLength(1); x++) {
                        if (map[x, y] > 9) {
                            allThatHaveToFlash.Add((x, y));
                        }
                    }
                }
                //Jetzt alle in der Liste abarbeiten:
                for (int i = 0; i < allThatHaveToFlash.Count; i++) {
                    //Erst die umliegenden finden:
                    List<(int x, int y)> adjacent = GetAdjacent(allThatHaveToFlash[i], allThatHaveToFlash);
                    for (int a = 0; a < adjacent.Count; a++) {
                        map[adjacent[a].x, adjacent[a].y]++;
                        if (map[adjacent[a].x, adjacent[a].y] > 9) {
                            allThatHaveToFlash.Add(adjacent[a]);
                        }
                    }
                    hasFlashed[(allThatHaveToFlash[i].x, allThatHaveToFlash[i].y)] = true;
                    map[allThatHaveToFlash[i].x, allThatHaveToFlash[i].y] = 0;
                }
                if (hasFlashed.All(x => x.Value == true)) return step + 1;
            }
            return 0;
        }

        List<(int x, int y)> GetAdjacent((int x, int y) currentPosition, List<(int x, int y)> FlashedAlready) {
            List<(int x, int y)> adjacent = new();
            List<(int x, int y)> standardMoves = new() {
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1),
                (1, -1),
                (1,1),
                (-1, 1),
                (-1, -1)
            };
            for (int i = 0; i < standardMoves.Count; i++) {
                int targetX = currentPosition.x + standardMoves[i].x;
                int targetY = currentPosition.y + standardMoves[i].y;
                if (targetX > input[0].Length - 1 || targetX < 0 || targetY > input.Length - 1 || targetY < 0) {
                    continue;
                }
                adjacent.Add((targetX, targetY));
            }
            adjacent.RemoveAll(x => FlashedAlready.Contains(x));
            return adjacent;
        }

        int[,] ParseInput() {
            int[,] map = new int[input[0].Length, input.Length];
            for (int y = 0; y < input.Length; y++) {
                string s = input[y].ToString();
                for (int x = 0; x < s.Length; x++) {
                    map[x, y] = s[x] - '0';
                }
            }
            return map;
        }
    }
}