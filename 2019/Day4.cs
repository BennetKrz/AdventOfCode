namespace AdventOfCode._2019 {
    public class Day4 : IAdventDay {

        int lowerLimit = 402328;
        int upperLimit = 864247;

        public long PartOne() {
            int validPasswordCount = 0;

            for(int i = lowerLimit; i <= upperLimit; i++) {
                string number = i.ToString();
                //Gucken, ob 2 zahlen nacheinander identisch sind
                bool noDoubles = true;
                for(int x = 0; x < number.Length - 1; x++) {
                    if(number[x] == number[x + 1]) {
                        noDoubles = false;
                        break;
                    }
                }
                if(noDoubles) {
                    continue;
                }

                bool isDecreasing = false;
                //Gucken, ob die nummern kleiner werden
                for(int x = 1; x < number.Length; x++) {
                    if(number[x] < number[x - 1]) {
                        isDecreasing = true;
                        break;
                    }
                }
                if(isDecreasing) {
                    continue;
                }
                validPasswordCount++;
            }
            return validPasswordCount;
        }

        public long PartTwo() {
            int validPasswordCount = 0;

            for(int i = lowerLimit; i <= upperLimit; i++) {
                string number = i.ToString();
                //Gucken, ob 2 zahlen nacheinander identisch sind
                bool noDoubles = true;

                Dictionary<char, int> occurences = new();

                for(int x = 0; x < number.Length; x++) {
                    if(!occurences.ContainsKey(number[x])) {
                        occurences.Add(number[x], 1);
                    } else {
                        occurences[number[x]]++;
                    }
                }
                if(occurences.Values.Min() > 2) {
                    continue;
                }
                for(int x = 0; x < number.Length; x++) {
                    if(occurences[number[x]] == 2) {
                        int index = number.IndexOf(number[x]);
                        if(number[index] == number[index + 1]) {
                            noDoubles = false;
                            break;
                        }
                    }
                }

                if(noDoubles) {
                    continue;
                }

                bool isDecreasing = false;
                //Gucken, ob die nummern kleiner werden
                for(int x = 1; x < number.Length; x++) {
                    if(number[x] < number[x - 1]) {
                        isDecreasing = true;
                        break;
                    }
                }
                if(isDecreasing) {
                    continue;
                }
                validPasswordCount++;
            }
            return validPasswordCount;
        }
    }
}
