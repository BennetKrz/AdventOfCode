namespace AdventOfCode._2016 {
    public class Day4 : IAdventDay {
        string[] input;

        List<string> filterdList = new();

        public Day4() {
            this.input = Helper.GetInput(4);
        }

        public long PartOne() {
            int validScore = 0;
            for(int i = 0; i < input.Length; i++) {
                string name = input[i].Substring(0, input[i].LastIndexOf('-')).Replace("-", "");
                Dictionary<char, int> occurences = SortDictionary(GetOccurencesInString(name));
                string checksum = input[i].Substring(input[i].IndexOf("[") + 1, 5);
                bool valid = true;
                for(int j = 0; j < checksum.Length; j++) {
                    if(occurences.Keys.ToArray()[j] != checksum[j]) {
                        valid = false;
                        break;
                    }
                }
                if(valid) {
                    filterdList.Add(input[i]);
                    validScore += int.Parse(input[i].Substring(input[i].LastIndexOf("-") + 1, 3));
                }
            }
            return validScore;
        }

        Dictionary<char, int> SortDictionary(Dictionary<char, int> occurences) {
            List<KeyValuePair<char, int>> items = occurences.ToList();
            items.Sort((x, y) => {
                int valueComparison = y.Value.CompareTo(x.Value);
                if(valueComparison != 0) {
                    return valueComparison;
                } else {
                    return x.Key.CompareTo(y.Key);
                }
            });

            Dictionary<char, int> result = new();
            foreach(KeyValuePair<char, int> pair in items) {
                result.Add(pair.Key, pair.Value);
            }
            return result;
        }

        Dictionary<char, int> GetOccurencesInString(string name) {
            Dictionary<char, int> occurences = new();
            for(int i = 0; i < name.Length; i++) {
                char c = name[i];
                if(occurences.ContainsKey(c)) {
                    occurences[c]++;
                } else {
                    occurences.Add(c, 1);
                }
            }
            return occurences;
        }

        public long PartTwo() {
            List<string> decryptedNames = new();
            List<int> roomNumbers = new();
            for(int i = 0; i < filterdList.Count; i++) {
                string name = filterdList[i].Substring(0, filterdList[i].LastIndexOf('-')).Replace("-", " ");
                int roomNumber = int.Parse(filterdList[i].Substring(filterdList[i].LastIndexOf("-") + 1, 3));
                string decryptedName = ShiftCharacters(name, roomNumber);
                decryptedNames.Add(decryptedName);
                roomNumbers.Add(roomNumber);
                Console.WriteLine(decryptedName + "  ||  " + roomNumber);
            }
            return long.MinValue;
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        string ShiftCharacters(string name, int amount) {
            int shift = amount % 26;
            string result = string.Empty;
            for(int i = 0; i < name.Length; i++) {
                if(name[i] == ' ') {
                    result += ' ';
                    continue;
                }
                int posAfterShift = alphabet.IndexOf(name[i]) + shift;
                if(posAfterShift > 25) {
                    posAfterShift -= 26;
                }
                result += alphabet[posAfterShift];
            }
            return result;
        }
    }
}
