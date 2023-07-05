namespace AdventOfCode._2021 {
    public class Day17 : IAdventDay {
        string input;

        public Day17() {
            this.input = Helper.GetInputOneLine(17);
        }

        public long PartOne() {
            (int x1, int x2, int y1, int y2) targetZone = ReturnTargetArea();
            Dictionary<(int x, int y), int> velocityToHighestY = new();
            for(int x = 0; x < 1000; x++) {
                for(int y = 0; y < 1000; y++) {
                    velocityToHighestY.Add((x, y), SimulateTrajectory(x, y, targetZone).maxY);
                }
            }
            return velocityToHighestY.Values.Max();
        }

        public long PartTwo() {
            (int x1, int x2, int y1, int y2) targetZone = ReturnTargetArea();
            HashSet<(int x, int y)> ReachTarget = new();
            for(int x = 0; x < 1000; x++) {
                for(int y = -1000; y < 1000; y++) {
                    (int x, int y, int maxY) endPosition = SimulateTrajectory(x, y, targetZone);
                    if(endPosition != (-1, 1, -1)) {
                        ReachTarget.Add((x, y));
                    }
                }
            }
            return ReachTarget.Count;
        }

        (int x, int y, int maxY) SimulateTrajectory(int velocityX, int velocityY, (int x1, int x2, int y1, int y2) endPosition) {
            List<int> allYs = new();
            int posX = 0;
            int posY = 0;
            while(posX <= endPosition.x2 && posY >= endPosition.y2) {
                if(posX >= endPosition.x1 && posX <= endPosition.x2 && posY <= endPosition.y1 && posY >= endPosition.y2) {
                    return (posX, posY, allYs.Max());
                }
                allYs.Add(posY);
                posX += velocityX;
                posY += velocityY;
                if(velocityX > 0) {
                    velocityX += -1;
                } else if(velocityX < 0) {
                    velocityX += 1;
                }
                velocityY += -1;
                if(posX > endPosition.x2 || posY < endPosition.y2) {
                    return (-1, 1, -1);
                }
            }
            return (-1, 1, -1);
        }

        (int x1, int x2, int y1, int y2) ReturnTargetArea() {
            return (240, 292, -57, -90);
        }
    }
}