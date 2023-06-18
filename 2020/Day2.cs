namespace AdventOfCode._2020 {
    public class Day2 : IAdventDay {
        string[] input;

        public Day2() {
            this.input = Helper.GetInput(2);
        }

        public long PartOne() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            int[] lowest = new int[input.Length];
            int[] highest = new int[input.Length];
            string[] c = new string[input.Length];
            string[] passwords = new string[input.Length];
            int validPasswords = 0;
            for (int i = 0; i < input.Length; i++) {
                //seperate by '-'
                string s = input[i];
                int min = Convert.ToInt32(s.Substring(0, s.IndexOf('-')));
                s = s.Remove(0, s.IndexOf('-') + 1);
                int max = Convert.ToInt32(s.Substring(0, s.IndexOf(' ')));
                s = s.Remove(0, s.IndexOf(' ') + 1);
                lowest[i] = min;
                highest[i] = max;
                input[i] = s;
            }
            for (int i = 0; i < input.Length; i++) {
                //get the gesuchten char
                string s = input[i];
                c[i] = s.Substring(0, s.IndexOf(':'));
                s = s.Remove(0, s.IndexOf(':') + 2);
                input[i] = s;
            }
            for (int i = 0; i < input.Length; i++) {
                //Get all Passwords
                string s = input[i];
                passwords[i] = s;
            }
            //Jetzt überprüfen
            for (int i = 0; i < input.Length; i++) {
                string password = passwords[i];
                string gesuchterChar = c[i];
                char ch = Convert.ToChar(gesuchterChar);
                int freq = password.Count(f => (f == ch));
                if (freq <= highest[i] && freq >= lowest[i]) {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        public long PartTwo() {
            string[] input = new string[this.input.Length];
            this.input.CopyTo(input, 0);
            int[] minPos = new int[input.Length];
            int[] maxPos = new int[input.Length];
            string[] c = new string[input.Length];
            string[] passwords = new string[input.Length];
            int validPasswords = 0;
            for (int i = 0; i < input.Length; i++) {
                //seperate by '-'
                string s = input[i];
                int min = Convert.ToInt32(s.Substring(0, s.IndexOf('-')));
                s = s.Remove(0, s.IndexOf('-') + 1);
                int max = Convert.ToInt32(s.Substring(0, s.IndexOf(' ')));
                s = s.Remove(0, s.IndexOf(' ') + 1);
                minPos[i] = min;
                maxPos[i] = max;
                input[i] = s;
            }
            for (int i = 0; i < input.Length; i++) {
                //get the gesuchten char
                string s = input[i];
                c[i] = s.Substring(0, s.IndexOf(':'));
                s = s.Remove(0, s.IndexOf(':') + 2);
                input[i] = s;
            }
            for (int i = 0; i < input.Length; i++) {
                //Get all Passwords
                string s = input[i];
                passwords[i] = s;
            }
            //Jetzt überprüfen
            for (int i = 0; i < input.Length; i++) {
                string password = passwords[i];
                string gesuchterChar = c[i];
                char ch = Convert.ToChar(gesuchterChar);
                if (password[minPos[i] - 1] == ch || password[maxPos[i] - 1] == ch) {
                    if ((password[minPos[i] - 1] == ch && password[maxPos[i] - 1] == ch) == false) {
                        validPasswords++;
                    }
                }
            }
            return validPasswords;
        }
    }
}
