namespace AdventOfCode._2021 {
    public class Day13 : IAdventDay {
        string[] input;

        public Day13() {
            this.input = Helper.GetInput(13);
        }

        public long PartOne() {
            List<FoldInstruction> foldinstructions = GetInstructions();
            HashSet<(int x, int y)> map = ParseInput();
            map = FoldMap(false, 655, map);
            return map.Count;
        }

        public long PartTwo() {
            List<FoldInstruction> foldinstructions = GetInstructions();
            HashSet<(int x, int y)> map = ParseInput();
            //map = FoldArrayY(7, map);
            for (int i = 0; i < foldinstructions.Count; i++) {
                map = FoldMap(foldinstructions[i].FoldByY, foldinstructions[i].FoldAt, map);
            }
            int maxY = map.Max(y => y.y);
            int maxX = map.Max(x => x.x);
            string output = string.Empty;
            for (int y = 0; y <= maxY; y++) {
                string part = string.Empty;
                for (int x = 0; x <= maxX; x++) {
                    part += map.Contains((x, y)) ? "#" : " ";
                }
                output += part + "\n";
            }
            Console.WriteLine(output);
            return long.MinValue;
        }
        HashSet<(int x, int y)> FoldMap(bool onY, int line, HashSet<(int x, int y)> map) {
            HashSet<(int x, int y)> foldedMap = new(map
                .Where(c => onY ? c.y > line : c.x > line)
                .Select(c => onY
                ? (c.x, 2 * line - c.y)
                : (2 * line - c.x, c.y)));
            map.RemoveWhere(c => onY ? c.y > line : c.x > line);
            map.UnionWith(foldedMap);
            return map;
        }

        HashSet<(int x, int y)> ParseInput() {
            HashSet<(int x, int y)> map = new();
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == "" || input[i][0] == 'f') {
                    continue;
                }
                (int x, int y) parts = (int.Parse(input[i].Split(',')[0]), int.Parse(input[i].Split(",")[1]));
                map.Add(parts);
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
                    f.FoldByY = false;
                } else f.FoldByY = true;
                f.FoldAt = int.Parse(instruction.Split('=')[1]);
                inst.Add(f);
            }
            return inst;
        }
    }

    class FoldInstruction {
        public bool FoldByY { get; set; }
        public int FoldAt { get; set; }
    }
}
