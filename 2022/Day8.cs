namespace AdventOfCode._2022 {
    public class Day8 : IAdventDay {
        string[] input;

        public Day8() {
            this.input = Helper.GetInput(8);
        }

        public long PartOne() {
            List<List<int>> forest = input.Select(line => {
                var numbers = line.Select(c => (int)char.GetNumericValue(c)).ToList();
                return numbers;
            }).ToList();
            int visibleTrees = 0;
            for (int row = 0; row < forest.Count; row++) {
                for (int collum = 0; collum < forest[row].Count; collum++) {
                    int treeHeight = forest[row][collum];
                    bool visibleLeft = !Enumerable.Range(0, collum).Any(leftIndex => forest[row][leftIndex] >= treeHeight);
                    bool visibleRight = !Enumerable.Range(collum + 1, forest[row].Count - collum - 1).Any(rightIndex => forest[row][rightIndex] >= treeHeight);
                    bool visibleTop = !Enumerable.Range(0, row).Any(topIndex => forest[topIndex][collum] >= treeHeight);
                    bool visibleBottom = !Enumerable.Range(row + 1, forest.Count - row - 1).Any(bottomIndex => forest[bottomIndex][collum] >= treeHeight);

                    if (visibleBottom || visibleLeft || visibleRight || visibleTop) {
                        visibleTrees++;
                    }
                }
            }
            return visibleTrees;
        }

        public long PartTwo() {
            List<List<int>> forest = input.Select(line => {
                var numbers = line.Select(c => (int)char.GetNumericValue(c)).ToList();
                return numbers;
            }).ToList();

            int highestScore = 0;

            for (int row = 0; row < forest.Count; row++) {
                for (int collum = 0; collum < forest[row].Count; collum++) {
                    if (row == 0 || collum == 0 || row == forest.Count || collum == forest[row].Count) {
                        continue;
                    }
                    int treeHeight = forest[row][collum];

                    List<int> leftTrees = forest[row].Skip(0).Take(collum).ToList();
                    int firstBlockLeftIndex = leftTrees.FindLastIndex(height => height >= treeHeight);
                    int viewableLeftCount = firstBlockLeftIndex == -1 ? leftTrees.Count : collum - firstBlockLeftIndex;

                    List<int> rightTrees = forest[row].Skip(collum + 1).Take(forest[row].Count - collum - 1).Reverse().ToList();
                    int firstBlockRightIndex = rightTrees.FindLastIndex(height => height >= treeHeight);
                    int viewableRightCount = firstBlockRightIndex == -1 ? rightTrees.Count : forest[row].Count - collum - firstBlockRightIndex - 1;

                    List<int> upTrees = forest.Skip(0).Take(row).Select(ints => ints[collum]).ToList();
                    int firstBlockUpIndex = upTrees.FindLastIndex(height => height >= treeHeight);
                    int viewableUpCount = firstBlockUpIndex == -1 ? upTrees.Count : row - firstBlockUpIndex;

                    List<int> downTrees = forest.Skip(row + 1).Take(forest.Count - row - 1).Select(ints => ints[collum]).Reverse().ToList();
                    int firstBlockDownIndex = downTrees.FindLastIndex(height => height >= treeHeight);
                    int viewableDownCount = firstBlockDownIndex == -1 ? downTrees.Count : forest.Count - row - firstBlockDownIndex - 1;

                    int scorePerTree = viewableDownCount * viewableLeftCount * viewableRightCount * viewableUpCount;

                    if (scorePerTree > highestScore) {
                        highestScore = scorePerTree;
                    }
                }
            }
            return highestScore;
        }
    }
}
