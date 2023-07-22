namespace AdventOfCode._2022 {
    public static class Helper {

        private static string DefaultPath = @"/home/d0d0/RiderProjects/AdventOfCode/2022/Input/day_";
        public static string[] GetInput(int day) {
            return File.ReadAllLines($"{DefaultPath}{day}.txt");
        }

        public static string GetInputOneLine(int day) {
            return File.ReadAllText($"{DefaultPath}{day}.txt");
        }
    }
}
