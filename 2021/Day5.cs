namespace AdventOfCode._2021 {
    public class Day5 : IAdventDay {
        string[] input;

        public Day5() {
            this.input = Helper.GetInput(5);
        }

        public long PartOne() {
            List<(int x1, int y1, int x2, int y2)> lines = ParseInput();
            //Create Map
            int[,] map = new int[1000, 1000];


            for (int i = 0; i < lines.Count; i++) {
                (int x1, int y1, int x2, int y2) line = lines[i];
                if (line.x1 == line.x2) {
                    if (line.y1 > line.y2) {
                        int yT = line.y2;
                        line.y2 = line.y1;
                        line.y1 = yT;
                    }
                    for (int y = line.y1; y <= line.y2; y++) {
                        map[line.x1, y]++;
                    }
                }
                if (line.y1 == line.y2) {
                    if (line.x1 > line.x2) {
                        int xT = line.x2;
                        line.x2 = line.x1;
                        line.x1 = xT;
                    }
                    for (int x = line.x1; x <= line.x2; x++) {
                        map[x, line.y1]++;
                    }
                }
            }
            return map.Cast<int>().Where(x => x > 1).Count();
        }

        public long PartTwo() {
            List<(int x1, int y1, int x2, int y2)> lines = ParseInput();
            //Create Map
            int[,] map = new int[1000, 1000];


            for (int i = 0; i < lines.Count; i++) {
                (int x1, int y1, int x2, int y2) line = lines[i];

                //Steigung berechen
                if (line.x1 != line.x2 && line.y1 != line.y2) {
                    Console.WriteLine(Steigung(line));
                } else {
                    Console.WriteLine("Nicht eins");
                }

                /*
                if (line.x1 == line.x2) {
                    if (line.y1 > line.y2) {
                        int yT = line.y2;
                        line.y2 = line.y1;
                        line.y1 = yT;
                    }
                    for (int y = line.y1; y <= line.y2; y++) {
                        map[line.x1, y]++;
                    }
                }
                if (line.y1 == line.y2) {
                    if (line.x1 > line.x2) {
                        int xT = line.x2;
                        line.x2 = line.x1;
                        line.x1 = xT;
                    }
                    for (int x = line.x1; x <= line.x2; x++) {
                        map[x, line.y1]++;
                    }
                }*/
            }
            return map.Cast<int>().Where(x => x > 1).Count();
        }

        float Steigung((int x1, int y1, int x2, int y2) line) {
            return (line.y2 - line.y1) / (line.x2 - line.x1);
        }

        List<(int x1, int y1, int x2, int y2)> ParseInput() {
            List<(int x1, int y1, int x2, int y2)> output = new();
            for (int i = 0; i < input.Length; i++) {
                string[] tmp = input[i].Split("->");
                string[] tmp2 = tmp[0].Split(",");
                string[] tmp3 = tmp[1].Split(",");
                output.Add((int.Parse(tmp2[0]), int.Parse(tmp2[1]), int.Parse(tmp3[0]), int.Parse(tmp3[1])));
            }
            return output;
        }
    }
}
