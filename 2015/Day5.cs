namespace AdventOfCode._2015 {
    public class Day5 : IAdventDay {
        string[] input;

        public Day5() {
            this.input = Helper.GetInput(5);
        }

        public long PartOne() {
            string[] badStringParts = { "ab", "cd", "pq", "xy" };
            int niceStrings = 0;
            for (int i = 0; i < input.Length; i++) {
                string s = input[i];
                if (s.Count(f => f == 'a' || f == 'e' || f == 'i' || f == 'o' || f == 'u') < 3) {
                    continue;
                }
                if (!containsCharTwoTimesInRow(s)) {
                    continue;
                }
                if (stringContainsBadString(s, badStringParts)) {
                    continue;
                }
                niceStrings++;
            }
            return niceStrings;
        }

        public long PartTwo() {
            return 0;
        }

        private bool stringContainsBadString(string input, string[] stringToCheckFor) {
            foreach (string s in stringToCheckFor) {
                if (input.Contains(s)) {
                    return true;
                }
            }
            return false;
        }

        private bool containsCharTwoTimesInRow(string input) {
            for (int i = 1; i < input.Length; i++) {
                if (input[i] == input[i - 1]) {
                    return true;
                }
            }
            return false;
        }
    }
}
