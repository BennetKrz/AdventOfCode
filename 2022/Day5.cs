namespace AdventOfCode._2022 {
    public class Day5 : IAdventDay {
        string[] input;

        public Day5() {
            this.input = Helper.GetInput(5);
        }

        public long PartOne() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            //Wieviele Items gibt es maximal in den Stacks
            int linesWithStacks = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i].Contains("[")) {
                    linesWithStacks++;
                }
            }
            List<Stack<char>> stacks = new List<Stack<char>>();
            //Gesamte Anzahl aller Stack bekommen und entsprechend viele STacks erstellen:
            int numberOfStacksMax = input[linesWithStacks - 1].Count(c => c == ']');
            for (int i = 0; i < numberOfStacksMax; i++) {
                stacks.Add(new Stack<char>());
            }
            //Alles in die entsprechenden Stacks ballern
            for (int i = linesWithStacks; i >= 0; i--) {
                for (int j = 1; j < input[numberOfStacksMax - 1].Length; j += 4) {
                    char c = input[i][j];
                    if (Char.IsLetter(c)) {
                        decimal stack = Math.Floor((decimal)j / (decimal)4);
                        stacks[(int)stack].Push(c);
                    }
                }
            }
            //Jetzt die anweisungen erhalten
            int[] amount = new int[input.Length - linesWithStacks];
            int[] from = new int[input.Length - linesWithStacks];
            int[] to = new int[input.Length - linesWithStacks];
            for (int i = linesWithStacks + 2; i < input.Length; i++) {
                input[i] = input[i].Replace(" ", "").Remove(0, 4);
                amount[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(0, input[i].IndexOf('f')));
                input[i] = input[i].Remove(0, input[i].IndexOf('m') + 1);
                from[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(0, 1));
                to[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(input[i].Length - 1, 1));
            }
            //Jetzt kann das verschieben beginnen
            for (int i = 0; i < amount.Length; i++) {
                for (int a = 0; a < amount[i]; a++) {
                    char c = stacks[from[i] - 1].Pop();
                    stacks[to[i] - 1].Push(c);
                }
            }
            for (int i = 0; i < stacks.Count; i++) {
                Console.WriteLine(stacks[i].Peek());
            }
            return long.MinValue;
        }

        public long PartTwo() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            //Wieviele Items gibt es maximal in den Stacks
            int linesWithStacks = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i].Contains("[")) {
                    linesWithStacks++;
                }
            }
            List<Stack<char>> stacks = new List<Stack<char>>();
            //Gesamte Anzahl aller Stack bekommen und entsprechend viele STacks erstellen:
            int numberOfStacksMax = input[linesWithStacks - 1].Count(c => c == ']');
            for (int i = 0; i < numberOfStacksMax; i++) {
                stacks.Add(new Stack<char>());
            }
            //Alles in die entsprechenden Stacks ballern
            for (int i = linesWithStacks; i >= 0; i--) {
                for (int j = 1; j < input[numberOfStacksMax - 1].Length; j += 4) {
                    char c = input[i][j];
                    if (Char.IsLetter(c)) {
                        decimal stack = Math.Floor((decimal)j / (decimal)4);
                        stacks[(int)stack].Push(c);
                    }
                }
            }
            //Jetzt die anweisungen erhalten
            int[] amount = new int[input.Length - linesWithStacks];
            int[] from = new int[input.Length - linesWithStacks];
            int[] to = new int[input.Length - linesWithStacks];
            for (int i = linesWithStacks + 2; i < input.Length; i++) {
                input[i] = input[i].Replace(" ", "").Remove(0, 4);
                amount[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(0, input[i].IndexOf('f')));
                input[i] = input[i].Remove(0, input[i].IndexOf('m') + 1);
                from[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(0, 1));
                to[i - linesWithStacks] = Convert.ToInt32(input[i].Substring(input[i].Length - 1, 1));
            }
            //Jetzt kann das verschieben beginnen
            for (int i = 0; i < amount.Length; i++) {
                Stack<char> temp = new Stack<char>();
                for (int a = 0; a < amount[i]; a++) {
                    char c = stacks[from[i] - 1].Pop();
                    temp.Push(c);
                }
                while (temp.Count > 0) {
                    stacks[to[i] - 1].Push(temp.Pop());
                }
            }
            for (int i = 0; i < stacks.Count; i++) {
                Console.WriteLine(stacks[i].Peek());
            }
            return long.MinValue;
        }
    }
}
