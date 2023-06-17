using Priority_Queue;

namespace AdventOfCode._2022 {
    public class Day12 : IAdventDay {
        string[] input;

        char startChar = 'S';
        char endChar = 'E';
        char startCharToHightValue = 'a';
        char endCharToHightValue = 'z';

        char[][] Map(string[] input) => input.Select(x => x.ToCharArray()).ToArray();

        ((int y, int x) startPoint, (int y, int x) endPoint) findStartAndEnd(char[][] map) {
            bool startFound = false, endFound = false;
            (int x, int y) startPoint = (-1, -1), endPoint = (-1, -1);
            int rows = map.Length, cols = map[0].Length;
            char val;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    val = map[y][x];
                    if (val == startChar) {
                        if (startFound) Console.WriteLine("Found more than one starting point");
                        startPoint = (y, x);
                        startFound = true;
                        if (endFound) break;
                    } else if (val == endChar) {
                        if (endFound) Console.WriteLine("Found more than one end point");
                        endPoint = (y, x);
                        endFound = true;
                        if (startFound) break;
                    }
                }
            }
            return (startPoint, endPoint);
        }

        public Day12() {
            this.input = Helper.GetInput(12);
        }

        public long PartOne() {
            char[][] map = Map(input);
            int rows = map.Length, cols = map[0].Length;
            var startEnd = findStartAndEnd(map);
            map[startEnd.startPoint.y][startEnd.startPoint.x] = startCharToHightValue;
            map[startEnd.endPoint.y][startEnd.endPoint.x] = endCharToHightValue;
            (int x, int y)[] moves = Extensions.Directions.HorizontalAndVertical;
            SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q = new SimplePriorityQueue<((int y, int x) pos, int stepsTaken)>();
            q.Enqueue((startEnd.startPoint, 0), 0);
            var result = FindPathDijkstra(
                map: map,
                visitedFields: new Dictionary<(int y, int x), int>(),
                q: q,
                isEnd: (targetPos, targetVal) => targetPos == startEnd.endPoint,
                isValidMove: (targetPos, currentVal, targetVal) => targetVal - currentVal <= 1,
                maxX: cols - 1,
                maxY: rows - 1,
                moves: moves);
            return result;
        }

        public long PartTwo() {
            char[][] map = Map(input);
            int rows = map.Length, cols = map[0].Length;
            var startEnd = findStartAndEnd(map);
            map[startEnd.startPoint.y][startEnd.startPoint.x] = startCharToHightValue;
            map[startEnd.endPoint.y][startEnd.endPoint.x] = endCharToHightValue;
            (int x, int y)[] moves = Extensions.Directions.HorizontalAndVertical;
            SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q = new SimplePriorityQueue<((int y, int x) pos, int stepsTaken)>();
            q.Enqueue((startEnd.endPoint, 0), 0);
            var result = FindPathDijkstra(
                map: map,
                visitedFields: new Dictionary<(int y, int x), int>(),
                q: q,
                isEnd: (targetPos, targetVal) => targetVal == startCharToHightValue,
                isValidMove: (targetPos, currentVal, targetVal) => currentVal - targetVal <= 1,
                maxX: cols - 1,
                maxY: rows - 1,
                moves: moves);
            return result;
        }

        int FindPathDijkstra(char[][] map,
            Dictionary<(int y, int x), int> visitedFields,
            SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q,
            Func<(int y, int x), char, bool> isEnd,
            Func<(int y, int x), char, char, bool> isValidMove,
            int maxX,
            int maxY,
            (int y, int x)[] moves) {

            char targetVal;

            while (q.Count > 0) {
                (int y, int x) targetPos;
                var target = q.Dequeue();
                char currentVal = map[target.pos.y][target.pos.x];
                if (visitedFields.ContainsKey(target.pos)) continue;
                visitedFields.Add(target.pos, target.stepsTaken);
                foreach (var move in moves) {
                    int x = target.pos.x + move.x;
                    int y = target.pos.y + move.y;
                    targetPos = (y, x);
                    if (x >= 0 && y >= 0 && x <= maxX && y <= maxY && !visitedFields.ContainsKey(targetPos)) {
                        targetVal = map[y][x];
                        if (isValidMove((y, x), currentVal, targetVal)) {
                            int stepsTaken = target.stepsTaken + 1;
                            if (isEnd((y, x), targetVal)) return stepsTaken;
                            else q.Enqueue((targetPos, stepsTaken), stepsTaken);
                        }
                    }
                }
            }
            return -1;
        }
    }
}
