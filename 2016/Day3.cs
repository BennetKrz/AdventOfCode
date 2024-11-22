namespace AdventOfCode._2016 {
    public class Day3 : IAdventDay {

        string[] input;

        public Day3() {
            this.input = Helper.GetInput(3);
        }

        public long PartOne() {
            int validCounter = 0;
            for(int i = 0; i < input.Length; i++) {
                int[] lengths = new int[3];
                lengths[0] = int.Parse(input[i].Substring(2, 3));
                lengths[1] = int.Parse(input[i].Substring(7, 3));
                lengths[2] = int.Parse(input[i].Substring(12, 3));
                if(lengths[0] + lengths[1] > lengths[2] && lengths[1] + lengths[2] > lengths[0] && lengths[0] + lengths[2] > lengths[1]) {
                    validCounter++;
                }
            }
            return validCounter;
        }
        public long PartTwo() {
            int validCounter = 0;
            for(int col = 0; col < 3; col++) {
                for(int i = 0; i < input.Length - 2; i += 3) {
                    int[] lengths = new int[3];
                    int startIndex;
                    if(col == 0) {
                        startIndex = 2;
                    } else if(col == 1) {
                        startIndex = 7;
                    } else {
                        startIndex = 12;
                    }
                    lengths[0] = int.Parse(input[i].Substring(startIndex, 3));
                    lengths[1] = int.Parse(input[i + 1].Substring(startIndex, 3));
                    lengths[2] = int.Parse(input[i + 2].Substring(startIndex, 3));
                    if(lengths[0] + lengths[1] > lengths[2] && lengths[1] + lengths[2] > lengths[0] && lengths[0] + lengths[2] > lengths[1]) {
                        validCounter++;
                    }
                }
            }
            return validCounter;
        }
    }
}
