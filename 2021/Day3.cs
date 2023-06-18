namespace AdventOfCode._2021 {
    public class Day3 : IAdventDay {
        string[] input;

        public Day3() {
            this.input = Helper.GetInput(3);
        }

        public long PartOne() {
            string gammaRate_b = String.Empty;
            string epsilonRate_b = String.Empty;
            for (int i = 0; i < input[0].Length; i++) {
                int numberOfOnes = 0;
                for (int j = 0; j < input.Length; j++) {
                    if (input[j][i] == '1') numberOfOnes++;
                }
                if (numberOfOnes > input.Length / 2) {
                    gammaRate_b += "1";
                    epsilonRate_b += "0";
                } else {
                    gammaRate_b += "0";
                    epsilonRate_b += "1";
                }
            }
            int epsilonRate_d = Convert.ToInt32(epsilonRate_b, 2);
            int gammaRate_d = Convert.ToInt32(gammaRate_b, 2);
            return epsilonRate_d * gammaRate_d;
        }

        public long PartTwo() {
            char criticalBit;
            List<string> input = new List<string>(this.input);

            for (int i = 0; i < input[0].Length; i++) {
                List<string> worklist = new List<string>();
                foreach (string s in input) {
                    worklist.Add(s);
                }
                int occurence_1 = 0;
                int occurence_0 = 0;
                for (int j = 0; j < worklist.Count; j++) {
                    char c = worklist[j][i];
                    if (c == '1') {
                        occurence_1++;
                    } else {
                        occurence_0++;
                    }
                }
                if (occurence_1 > worklist.Count / 2) {
                    criticalBit = '1';
                } else if (occurence_0 > worklist.Count / 2) {
                    criticalBit = '0';
                } else {
                    criticalBit = '1';
                }
                foreach (string s in worklist) {
                    if (s[i] != criticalBit) {
                        input.Remove(s);
                        if (input.Count == 1) {
                            break;
                        }
                    }
                }
            }
            int ox = Convert.ToInt32(input[0], 2);

            input = new List<string>(this.input);

            for (int i = 0; i < input[0].Length; i++) {
                List<string> worklist = new List<string>();
                foreach (string s in input) {
                    worklist.Add(s);
                }
                int occurence_1 = 0;
                int occurence_0 = 0;
                for (int j = 0; j < worklist.Count; j++) {
                    char c = worklist[j][i];
                    if (c == '1') {
                        occurence_1++;
                    } else {
                        occurence_0++;
                    }
                }
                if (occurence_1 > worklist.Count / 2) {
                    criticalBit = '1';
                } else if (occurence_0 > worklist.Count / 2) {
                    criticalBit = '0';
                } else {
                    criticalBit = '1';
                }
                foreach (string s in worklist) {
                    if (s[i] == criticalBit) {
                        input.Remove(s);
                        if (input.Count == 1) {
                            break;
                        }
                    }
                }
                if (input.Count == 1) {
                    break;
                }
            }
            int co = Convert.ToInt32(input[0], 2);
            return ox * co;
        }
    }
}
