namespace AdventOfCode._2018 {
    public static class Helper {

        private static string DefaultPath = @"G/home/d0d0/RiderProjects/AdventOfCode/2018/Input/day_";
        public static string[] GetInput(int day) {
            return File.ReadAllLines($"{DefaultPath}{day}.txt");
        }

        public static string GetInputOneLine(int day) {
            return File.ReadAllText($"{DefaultPath}{day}.txt");
        }
    }
}
