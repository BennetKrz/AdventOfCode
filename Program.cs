namespace AdventOfCode {
    public class Program {
        static void Main(string[] args) {
            IAdventDay day = new _2021.Day3();
            Console.WriteLine("- - - - - - - - - PART ONE - - - - - - - - - ");
            long resultPartOne = day.PartOne();
            if (resultPartOne != long.MinValue) Console.WriteLine(resultPartOne);
            Console.WriteLine("- - - - - - - - - PART TWO - - - - - - - - - ");
            long resultPartTwo = day.PartTwo();
            if (resultPartTwo != long.MinValue) Console.WriteLine(resultPartTwo);
        }
    }

    public interface IAdventDay {
        public long PartOne();
        public long PartTwo();
    }
}