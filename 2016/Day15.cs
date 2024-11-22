namespace AdventOfCode._2016 {

    using Disc = (byte Positions, byte CurrentPosition);

    public class Day15 : IAdventDay {
        string[] input;

        public Day15() {
            this.input = Helper.GetInput(15);
        }

        Dictionary<int, Disc[]> StoragePartOne = new();
        Dictionary<int, Disc[]> StoragePartTwo = new();

        public long PartOne() {
            Disc[] discs = ParseInputPartOne();
            StoragePartOne.Add(0, discs);
            CalculatePartOne(discs);
            int time = 0;
            while(true) {
                bool crash = false;
                for(int t = time; t < time + discs.Length; t++) {
                    //check current
                    if(StoragePartOne[t][t - time].CurrentPosition != 0) {
                        crash = true;
                        break;
                    }
                }
                if(crash) {
                    time++;
                } else {
                    break;
                }
            }
            return time - 1;
        }

        public long PartTwo() {
            Disc[] discs = ParseInputPartTwo();
            StoragePartTwo.Add(0, discs);
            CalculatePartTwo(discs);
            int time = 0;
            while(true) {
                bool crash = false;
                for(int t = time; t < time + discs.Length; t++) {
                    //check current
                    if(StoragePartTwo[t][t - time].CurrentPosition != 0) {
                        crash = true;
                        break;
                    }
                }
                if(crash) {
                    time++;
                } else {
                    break;
                }
            }
            return time - 1;
        }
        void CalculatePartOne(Disc[] discs, int limit = 10_000_000) {
            for(int t = 1; t < limit; t++) {
                discs = StoragePartOne[t - 1];
                Disc[] tmp = new Disc[discs.Length];
                Disc disc = (0, 0);
                for(int i = 0; i < discs.Length; i++) {
                    byte Positions = discs[i].Positions;
                    byte CurrentPosition = discs[i].CurrentPosition;
                    CurrentPosition++;
                    if(CurrentPosition >= Positions) {
                        CurrentPosition = 0;
                    }
                    disc = (Positions, CurrentPosition);
                    tmp[i] = disc;
                }
                StoragePartOne.Add(t, tmp);
            }
        }

        void CalculatePartTwo(Disc[] discs, int limit = 10_000_000) {
            for(int t = 1; t < limit; t++) {
                discs = StoragePartTwo[t - 1];
                Disc[] tmp = new Disc[discs.Length];
                Disc disc = (0, 0);
                for(int i = 0; i < discs.Length; i++) {
                    byte Positions = discs[i].Positions;
                    byte CurrentPosition = discs[i].CurrentPosition;
                    CurrentPosition++;
                    if(CurrentPosition >= Positions) {
                        CurrentPosition = 0;
                    }
                    disc = (Positions, CurrentPosition);
                    tmp[i] = disc;
                }
                StoragePartTwo.Add(t, tmp);
            }
        }
        Disc[] ParseInputPartOne() {
            Disc[] discs = new Disc[input.Length];
            for(int i = 0; i < input.Length; i++) {
                string s = input[i];
                string[] tmp = s.Split("has ");
                byte positions = byte.Parse(tmp[1].Substring(0, tmp[1].IndexOf(" ")));
                byte startPosition = byte.Parse(s.Replace(".", "").Split("position ")[1]);
                discs[i] = (positions, startPosition);
            }
            return discs;
        }

        Disc[] ParseInputPartTwo() {
            Disc[] discs = new Disc[input.Length + 1];
            for(int i = 0; i < input.Length; i++) {
                string s = input[i];
                string[] tmp = s.Split("has ");
                byte positions = byte.Parse(tmp[1].Substring(0, tmp[1].IndexOf(" ")));
                byte startPosition = byte.Parse(s.Replace(".", "").Split("position ")[1]);
                discs[i] = (positions, startPosition);
            }
            discs[discs.Length - 1] = new Disc(11, 0);
            return discs;
        }

    }
}
