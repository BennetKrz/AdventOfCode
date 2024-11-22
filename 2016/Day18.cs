namespace AdventOfCode._2016 {

    using ThreeTiles = (bool left, bool center, bool right);

    public class Day18 : IAdventDay {
        List<string> input;

        public Day18() {
            this.input = Helper.GetInput(18).ToList();
        }

        public long PartOne() {
            for(int i = 0; i < 39; i++) {
                string line = input[i];
                string nextLine = string.Empty;
                for(int c = 0; c < line.Length; c++) {
                    ThreeTiles tiles = GetNextTiles(line, c);
                    bool conditionTrue = false;
                    if(tiles.left && tiles.center && !tiles.right) {
                        conditionTrue = true;
                    }
                    if(!tiles.left && tiles.center && tiles.right) {
                        conditionTrue = true;
                    }
                    if(tiles.left && !tiles.center && !tiles.right) {
                        conditionTrue = true;
                    }
                    if(!tiles.left && !tiles.center && tiles.right) {
                        conditionTrue = true;
                    }
                    nextLine += conditionTrue ? '^' : '.';
                }
                input.Add(nextLine);
            }
            return input.Sum(s => s.Count(c => c == '.'));
        }

        ThreeTiles GetNextTiles(string line, int pos) {
            char c_left;
            char c_central = line[pos];
            char c_right;
            if(pos == 0) {
                c_left = '.';
            } else {
                c_left = line[pos - 1];
            }
            if(pos == line.Length - 1) {
                c_right = '.';
            } else {
                c_right = line[pos + 1];
            }
            return (c_left == '^', c_central == '^', c_right == '^');
        }

        public long PartTwo() {
            input.Clear();
            input = Helper.GetInput(18).ToList();
            for(int i = 0; i < 400_000 - 1; i++) {
                string line = input[i];
                string nextLine = string.Empty;
                for(int c = 0; c < line.Length; c++) {
                    ThreeTiles tiles = GetNextTiles(line, c);
                    bool conditionTrue = false;
                    if(tiles.left && tiles.center && !tiles.right) {
                        conditionTrue = true;
                    }
                    if(!tiles.left && tiles.center && tiles.right) {
                        conditionTrue = true;
                    }
                    if(tiles.left && !tiles.center && !tiles.right) {
                        conditionTrue = true;
                    }
                    if(!tiles.left && !tiles.center && tiles.right) {
                        conditionTrue = true;
                    }
                    nextLine += conditionTrue ? '^' : '.';
                }
                input.Add(nextLine);
            }
            return input.Sum(s => s.Count(c => c == '.'));
        }
    }
}
