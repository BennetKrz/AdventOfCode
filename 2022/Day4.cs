namespace AdventOfCode._2022 {
    public class Day4 : IAdventDay {
        string[] input;

        public Day4() {
            input = Helper.GetInput(4);
        }

        public long PartOne() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            string[,] firstElf = new string[2, input.Length];
            string[,] secondElf = new string[2, input.Length];
            for (int i = 0; i < input.Length; i++) {
                firstElf[0, i] = input[i].Substring(0, input[i].IndexOf('-'));
                firstElf[1, i] = input[i].Substring(input[i].IndexOf('-') + 1, input[i].IndexOf(',') - input[i].IndexOf('-') - 1);
                input[i] = input[i].Remove(0, input[i].IndexOf(',') + 1);
                secondElf[0, i] = input[i].Substring(0, input[i].IndexOf('-'));
                input[i] = input[i].Remove(0, input[i].IndexOf('-') + 1);
                secondElf[1, i] = input[i];
            }

            int score = 0;

            for (int i = 0; i < input.Length; i++) {
                if (((Convert.ToInt32(value: firstElf[0, i]) >= Convert.ToInt32(secondElf[0, i])) && (Convert.ToInt32(firstElf[1, i]) <= Convert.ToInt32(secondElf[1, i]))) || ((Convert.ToInt32(secondElf[0, i]) >= Convert.ToInt32(firstElf[0, i])) && (Convert.ToInt32(secondElf[1, i]) <= Convert.ToInt32(firstElf[1, i])))) {
                    score++;
                }
            }
            return score;
        }

        public long PartTwo() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            string[,] sFirstElf = new string[2, input.Length];
            string[,] sSecondElf = new string[2, input.Length];
            int[,] firstElf = new int[2, input.Length];
            int[,] secondElf = new int[2, input.Length];
            for (int i = 0; i < input.Length; i++) {
                sFirstElf[0, i] = input[i].Substring(0, input[i].IndexOf('-'));
                sFirstElf[1, i] = input[i].Substring(input[i].IndexOf('-') + 1, input[i].IndexOf(',') - input[i].IndexOf('-') - 1);
                input[i] = input[i].Remove(0, input[i].IndexOf(',') + 1);
                sSecondElf[0, i] = input[i].Substring(0, input[i].IndexOf('-'));
                input[i] = input[i].Remove(0, input[i].IndexOf('-') + 1);
                sSecondElf[1, i] = input[i];
            }

            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < input.Length; j++) {
                    firstElf[i, j] = Convert.ToInt32(sFirstElf[i, j]);
                    secondElf[i, j] = Convert.ToInt32(sSecondElf[i, j]);
                }
            }

            int score = 0;

            for (int i = 0; i < input.Length; i++) {
                if (firstElf[1, i] >= secondElf[0, i] && firstElf[1, i] <= secondElf[1, i]) {
                    score++;
                } else if (firstElf[0, i] >= secondElf[0, i] && firstElf[0, i] <= secondElf[1, i]) {
                    score++;
                } else if (secondElf[0, i] >= firstElf[0, i] && secondElf[0, i] <= firstElf[1, i]) {
                    score++;
                } else if (secondElf[1, i] >= firstElf[0, i] && secondElf[1, i] <= firstElf[1, i]) {
                    score++;
                }
            }
            return score;
        }
    }
}
