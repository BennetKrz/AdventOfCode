namespace AdventOfCode._2016 {
    public static class Helper {

        private static string DefaultPath = @"G:\C#\AdventOfCode\2016\Input\day_";
        public static string[] GetInput(int day) {
            return File.ReadAllLines($"{DefaultPath}{day}.txt");
        }

        public static string GetInputOneLine(int day) {
            return File.ReadAllText($"{DefaultPath}{day}.txt");
        }
    }
}

