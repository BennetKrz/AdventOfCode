namespace AdventOfCode._2021 {
    public class Day21 : IAdventDay {
        string[] input;

        public Day21() {
            this.input = Helper.GetInput(21);
        }

        public long PartOne() {
            int rolls = 0;
            int lastRoll = 0;
            int player1Score = 0;
            int player2Score = 0;
            int player1Pos = 4;
            int player2Pos = 8;

            for(; ; ) {
                //Player 1
                player1Pos = PositionAfterRoll(player1Pos, GetSumOfNextThreeNumbers(lastRoll));
                lastRoll += 3;
                rolls += 3;
                if(lastRoll > 100) {
                    lastRoll = lastRoll - 100;
                }
                player1Score += player1Pos;
                if(player1Score > 1000) {
                    Console.WriteLine(rolls);
                    return rolls * player2Score;
                }

                //Player 2
                player2Pos = PositionAfterRoll(player2Pos, GetSumOfNextThreeNumbers(lastRoll));
                lastRoll += 3;
                rolls += 3;
                if(lastRoll > 100) {
                    lastRoll = lastRoll - 100;
                }
                player2Score += player2Pos;
                if(player2Score > 1000) {
                    Console.WriteLine(rolls);
                    return rolls * player1Score;
                }
                if(rolls < 4000) {

                } else {
                    break;
                }
            }
            return 0;
        }

        public long PartTwo() {
            return 0;
        }

        int GetSumOfNextThreeNumbers(int lastRoll) {
            int sum = 0;
            for(int i = 1; i < 4; i++) {
                lastRoll++;
                if(lastRoll < 101) {
                    sum += lastRoll;
                } else {
                    lastRoll = 1;
                    sum += lastRoll;
                }
            }
            return sum;
        }

        int PositionAfterRoll(int startPosition, int stepsToGo) {
            int endPosition = startPosition + stepsToGo;
            for(int i = 0; i < stepsToGo; i++) {
                startPosition++;
                if(startPosition > 10) {
                    startPosition -= 10;
                }
            }
            return startPosition;
        }
    }
}
