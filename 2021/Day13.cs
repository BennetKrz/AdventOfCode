namespace AdventOfCode._2021 {
    public class Day13 : IAdventDay {
        string[] input;

        public Day13() {
            this.input = Helper.GetInput(13);
        }

        public long PartOne() {
            List<FoldInstruction> foldinstructions = GetInstructions();
            char[,] map = ParseInput();
            map = FoldArrayX(655, map);

            //DEBUGING: PRINT OUT MAP 
            /*
            string output = string.Empty;
            for (int y = 0; y < map.GetLength(1); y++) {
                string part = string.Empty;
                for (int x = 0; x < map.GetLength(0); x++) {
                    part += map[x, y];
                }
                output += part + "\n";
            }
            Console.WriteLine(output);*/
            return map.Cast<char>().Where(x => x == '#').Count();
        }

        char[,] FoldArrayX(int foldPos, char[,] map) {
            char[,] halfArray = new char[foldPos, map.GetLength(1)];
            int counterX = 0;
            for (int y = 0; y < map.GetLength(1); y++) {
                for (int x = 0; x < foldPos; x++) {
                    halfArray[x, y] = map[x, y];
                }
            }
            for (int y = 0; y < map.GetLength(1); y++) {
                for (int x = map.GetLength(0) - 1; x > foldPos; x--) {
                    if (halfArray[counterX, y] != '#') {
                        halfArray[counterX, y] = map[x, y];
                    }
                    counterX++;
                    if (counterX > foldPos - 1) counterX = 0;
                }
            }
            return halfArray;
        }

        char[,] FoldArrayY(int foldPos, char[,] map) {
            char[,] halfArray = new char[map.GetLength(0), foldPos];
            int counterY = 0;
            for (int y = 0; y < foldPos; y++) {
                for (int x = 0; x < map.GetLength(0); x++) {
                    halfArray[x, y] = map[x, y];
                }
            }
            for (int x = 0; x < map.GetLength(0); x++) {
                for (int y = map.GetLength(1) - 1; y > foldPos; y--) {
                    if (halfArray[x, counterY] != '#') {
                        halfArray[x, counterY] = map[x, y];
                    }
                    counterY++;
                    if (counterY > foldPos - 1) counterY = 0;
                }
            }

            //OUTPUT
            /*
            string output = string.Empty;
            for (int y = 0; y < halfArray.GetLength(1); y++) {
                string part = string.Empty;
                for (int x = 0; x < halfArray.GetLength(0); x++) {
                    part += halfArray[x, y];
                }
                output += part + "\n";
            }
            Console.WriteLine(output);*/
            return halfArray;
        }

        public long PartTwo() {
            List<FoldInstruction> foldinstructions = GetInstructions();
            char[,] map = ParseInput();
            //map = FoldArrayY(7, map);
            for (int i = 0; i < foldinstructions.Count; i++) {
                if (foldinstructions[i].FoldByX) {
                    map = FoldArrayX(foldinstructions[i].FoldAt, map);
                } else {
                    map = FoldArrayY(foldinstructions[i].FoldAt, map);
                }
            }

            //DEBUGING: PRINT OUT MAP
            string output = string.Empty;
            for (int y = 0; y < map.GetLength(1); y++) {
                string part = string.Empty;
                for (int x = 0; x < map.GetLength(0); x++) {
                    part += " " + map[x, y] + " ";
                }
                output += part + "\n";
            }
            Console.WriteLine(output);
            File.WriteAllText("output.txt", output);
            return map.Cast<char>().Where(x => x == '#').Count();
        }

        char[,] ParseInput() {
            List<string> gefiltert = input.Where(x => x != "" && x[0] != 'f').ToList();
            int[] allXes = gefiltert.Select(x => int.Parse(x.Split(',')[0])).ToArray();
            int[] allYes = gefiltert.Select(x => int.Parse(x.Split(',')[1])).ToArray();
            Dictionary<(int x, int y), char> points = new();
            for (int i = 0; i < allXes.Length; i++) {
                points.Add((allXes[i], allYes[i]), '#');
            }
            int xMax = allXes.Max() + 1;
            int yMax = allYes.Max() + 1;

            //Jetzt den eigentlichen Array erstellen:
            char[,] map = new char[xMax, yMax];
            for (int y = 0; y < yMax; y++) {
                for (int x = 0; x < xMax; x++) {
                    if (points.ContainsKey((x, y))) map[x, y] = '#';
                    else map[x, y] = '.';
                }
            }
            return map;
        }

        List<FoldInstruction> GetInstructions() {
            List<string> instructions = this.input.Where(x => x != "").Where(x => x[0] == 'f').ToList();
            List<FoldInstruction> inst = new();
            for (int i = 0; i < instructions.Count; i++) {
                FoldInstruction f = new();
                string instruction = instructions[i];
                if (instruction.Contains('x')) {
                    f.FoldByX = true;
                } else f.FoldByX = false;
                f.FoldAt = int.Parse(instruction.Split('=')[1]);
                inst.Add(f);
            }
            return inst;
        }
    }

    class FoldInstruction {
        public bool FoldByX { get; set; }
        public int FoldAt { get; set; }
    }
}
