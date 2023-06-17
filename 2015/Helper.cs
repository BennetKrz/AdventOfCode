namespace AdventOfCode._2015 {
    public static class Helper {

        private static string DefaultPath = @"G:\C#\AdventOfCode\2015\Input\day_";
        public static string[] GetInput(int day) {
            return File.ReadAllLines($"{DefaultPath}{day}.txt");
        }

        public static string GetInputOneLine(int day) {
            return File.ReadAllText($"{DefaultPath}{day}.txt");
        }
    }
}
