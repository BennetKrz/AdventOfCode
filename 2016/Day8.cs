namespace AdventOfCode._2016 {
    public class Day8 : IAdventDay {
        string[] input;

        public Day8() {
            this.input = Helper.GetInput(8);
        }

        public long PartOne() {
            char[,] display = new char[50, 6];
            for(int y = 0; y < display.GetLength(1); y++) {
                for(int x = 0; x < display.GetLength(0); x++) {
                    display[x, y] = '.';
                }
            }
            //Instructions abarbeiten:
            for(int i = 0; i < input.Length; i++) {
                if(input[i].Contains("rect")) {
                    int a;
                    int b;
                    string[] tmp = input[i].Split('x');
                    tmp[0] = tmp[0].Replace("rect ", "");
                    a = int.Parse(tmp[0]);
                    b = int.Parse(tmp[1]);
                    DrawRect(display, a, b);
                } else {
                    int a;
                    int b;
                    string[] tmp = input[i].Split(" by ");
                    b = int.Parse(tmp[1]);
                    string[] tmp2 = tmp[0].Split('=');
                    a = int.Parse(tmp2[1]);
                    if(input[i].Contains("row")) {
                        RotateRow(display, a, b);
                    } else {
                        RotateColumn(display, a, b);
                    }
                }
            }
            return display.Cast<char>().Where(c => c == '#').Count();
        }

        char[,] RotateRow(char[,] display, int a, int b) {
            string tmp = string.Empty;
            //Backup
            for(int x = 0; x < display.GetLength(0); x++) {
                tmp += display[x, a];
            }
            //Jetzt shiften:
            for(int c = 0; c < tmp.Length; c++) {
                int posAfterShift = c + b;
                while(posAfterShift > display.GetLength(0) - 1) {
                    posAfterShift -= display.GetLength(0);
                }
                display[posAfterShift, a] = tmp[c];
            }
            return display;
        }

        char[,] RotateColumn(char[,] display, int a, int b) {
            string tmp = string.Empty;
            //Backup
            for(int y = 0; y < display.GetLength(1); y++) {
                tmp += display[a, y];
            }
            //Jetzt shiften
            for(int c = 0; c < tmp.Length; c++) {
                int posAfterShift = c + b;
                while(posAfterShift > display.GetLength(1) - 1) {
                    posAfterShift -= display.GetLength(1);
                }
                display[a, posAfterShift] = tmp[c];
            }
            return display;
        }

        char[,] DrawRect(char[,] display, int a, int b) {
            for(int x = 0; x < a; x++) {
                for(int y = 0; y < b; y++) {
                    display[x, y] = '#';
                }
            }
            return display;
        }

        public long PartTwo() {
            char[,] display = new char[50, 6];
            for(int y = 0; y < display.GetLength(1); y++) {
                for(int x = 0; x < display.GetLength(0); x++) {
                    display[x, y] = '.';
                }
            }
            //Instructions abarbeiten:
            for(int i = 0; i < input.Length; i++) {
                if(input[i].Contains("rect")) {
                    int a;
                    int b;
                    string[] tmp = input[i].Split('x');
                    tmp[0] = tmp[0].Replace("rect ", "");
                    a = int.Parse(tmp[0]);
                    b = int.Parse(tmp[1]);
                    DrawRect(display, a, b);
                } else {
                    int a;
                    int b;
                    string[] tmp = input[i].Split(" by ");
                    b = int.Parse(tmp[1]);
                    string[] tmp2 = tmp[0].Split('=');
                    a = int.Parse(tmp2[1]);
                    if(input[i].Contains("row")) {
                        RotateRow(display, a, b);
                    } else {
                        RotateColumn(display, a, b);
                    }
                }
            }
            PrintArray(display);
            return long.MinValue;
        }

        void PrintArray(char[,] arrayToPrint) {
            string output = string.Empty;
            for(int y = 0; y < arrayToPrint.GetLength(1); y++) {
                string part = string.Empty;
                for(int x = 0; x < arrayToPrint.GetLength(0); x++) {
                    part += arrayToPrint[x, y];
                    part += ' ';
                }
                output += part + "\n";
            }
            Console.WriteLine(output);
        }
    }
}
