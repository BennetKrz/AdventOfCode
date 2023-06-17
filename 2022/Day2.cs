namespace AdventOfCode._2022 {
    public class Day2 : IAdventDay {

        string[] input;

        public Day2() {
            this.input = Helper.GetInput(2);
        }

        public long PartOne() {
            int score = 0;

            for (int i = 0; i < input.Length; i++) {
                if (input[i][0] == 'A') {
                    if (input[i][2] == 'X') score += 4;
                    if (input[i][2] == 'Y') score += 8;
                    if (input[i][2] == 'Z') score += 3;
                } else if (input[i][0] == 'B') {
                    if (input[i][2] == 'X') score += 1;
                    if (input[i][2] == 'Y') score += 5;
                    if (input[i][2] == 'Z') score += 9;
                } else if (input[i][0] == 'C') {
                    if (input[i][2] == 'X') score += 7;
                    if (input[i][2] == 'Y') score += 2;
                    if (input[i][2] == 'Z') score += 6;
                }
            }
            return score;
        }

        public long PartTwo() {
            int score = 0;

            for (int i = 0; i < input.Length; i++) {
                if (input[i][0] == 'A') {
                    if (input[i][2] == 'X') score += 3;
                    if (input[i][2] == 'Y') score += 4;
                    if (input[i][2] == 'Z') score += 8;
                } else if (input[i][0] == 'B') {
                    if (input[i][2] == 'X') score += 1;
                    if (input[i][2] == 'Y') score += 5;
                    if (input[i][2] == 'Z') score += 9;
                } else if (input[i][0] == 'C') {
                    if (input[i][2] == 'X') score += 2;
                    if (input[i][2] == 'Y') score += 6;
                    if (input[i][2] == 'Z') score += 7;
                }
            }
            return score;
        }
    }
}
