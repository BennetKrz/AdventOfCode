namespace AdventOfCode._2016 {
    public class Day2 : IAdventDay {
        string[] input;

        public Day2() {
            this.input = Helper.GetInput(2);
        }

        string[] keypadPartOne = new string[] {
            "123", "456", "789",
        };
        
        string[] keypadPartTwo = new string[] {
            "  1  ", " 234 ", "56789", " ABC ", "  D  "
        };
        
        public long PartOne() {
            (int x, int y) currentPosition = (1, 1);
            string code = string.Empty;
            for (int i = 0; i < input.Length; i++) {
                string instruction = input[i];
                for (int c = 0; c < instruction.Length; c++) {
                    switch (instruction[c]) {
                        case 'U':
                            currentPosition.y -= 1;
                            if (currentPosition.y < 0) {
                                currentPosition.y = 0;
                            } 
                            break;
                        case 'R':
                            currentPosition.x += 1;
                            if (currentPosition.x > 2) {
                                currentPosition.x = 2;
                            }
                            break;
                        case 'D':
                            currentPosition.y += 1;
                            if (currentPosition.y > 2) {
                                currentPosition.y = 2;
                            }
                            break;
                        case 'L':
                            currentPosition.x -= 1;
                            if (currentPosition.x < 0) {
                                currentPosition.x = 0;
                            }
                            break;
                    }
                }
                code += keypadPartOne[currentPosition.y][currentPosition.x];
            }
            return long.Parse(code);
        }

        public long PartTwo() {
            (int x, int y) currentPosition = (0, 2);
            string code = string.Empty;
            for (int i = 0; i < input.Length; i++) {
                string instruction = input[i];
                for (int c = 0; c < instruction.Length; c++) {
                    switch (instruction[c]) {
                        case 'U':
                            if (currentPosition.y - 1 >= 0 && keypadPartTwo[currentPosition.y - 1][currentPosition.x] != ' ') {
                                currentPosition.y -= 1;
                            }
                            break;
                        case 'R':
                            if (currentPosition.x + 1 < 5 && keypadPartTwo[currentPosition.y][currentPosition.x + 1] != ' ') {
                                currentPosition.x += 1;
                            }
                            break;
                        case 'D':
                            if (currentPosition.y + 1 < 5 && keypadPartTwo[currentPosition.y + 1][currentPosition.x] != ' ') {
                                currentPosition.y += 1;
                            }
                            break;
                        case 'L':
                            if (currentPosition.x - 1 >= 0 && keypadPartTwo[currentPosition.y][currentPosition.x - 1] != ' ') {
                                currentPosition.x -= 1;
                            }
                            break;
                    }
                }
                code += keypadPartTwo[currentPosition.y][currentPosition.x];
            }
            Console.WriteLine(code);
            return long.MinValue;
        }
    }
}
