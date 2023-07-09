namespace AdventOfCode._2021 {

    using SecondValue = (string valueOrVariable, bool isNumber);

    public class Day24 : IAdventDay {
        string[] input;

        public Day24() {
            this.input = Helper.GetInput(24);

        }

        public long PartOne() {
            List<long> possibleNumbers = new();
            for(long i = 11111111111111; i < 99999999999999; i++) {
                if(i % 1_000_000 == 0) {
                    Console.WriteLine(i);
                }
                if(i.ToString().Contains("0")) {
                    continue;
                }
                if(validateNumber(i.ToString()) == 0) {
                    possibleNumbers.Add(i);
                }
            }
            return possibleNumbers.Max();
        }

        long validateNumber(string number) {
            string serialNumber = "13579246899999";
            int counter = 0;
            Dictionary<char, int> variables = new() {
                { 'w', 0},
                { 'x', 0},
                { 'y', 0},
                { 'z', 0}
            };

            for(int i = 0; i < input.Length; i++) {
                string instruction = input[i];
                string instructionType = instruction.Substring(0, 3);
                if(instructionType == "inp") {
                    variables['w'] = int.Parse(serialNumber[counter].ToString());
                    counter++;
                    continue;
                }
                SecondValue val = GetSecondValue(instruction);
                int b = val.isNumber ? int.Parse(val.valueOrVariable) : variables[val.valueOrVariable[0]];
                char target = instruction[4];
                switch(instructionType) {
                    case "add": {
                        variables[target] = add(variables[target], b);
                        break;
                    }
                    case "mul": {
                        variables[target] = mul(variables[target], b);
                        break;
                    }
                    case "div": {
                        variables[target] = div(variables[target], b);
                        break;
                    }
                    case "mod": {
                        variables[target] = mod(variables[target], b);
                        break;
                    }
                    case "eql": {
                        variables[target] = eql(variables[target], b);
                        break;
                    }
                }
            }
            return variables['z'];
        }

        public long PartTwo() {
            return 0;
        }

        int add(int a, int b) {
            return a + b;
        }

        int mul(int a, int b) {
            return a * b;
        }

        int div(int a, int b) {
            return a / b;
        }

        int mod(int a, int b) {
            return a % b;
        }

        int eql(int a, int b) {
            return a == b ? 1 : 0;
        }

        SecondValue GetSecondValue(string s) {
            if(s[0] == 'i') {
                return ("", true);
            }

            if("wxyz".Contains(s[^1])) {
                return (s[^1].ToString(), false);
            }
            return (s.Split(' ')[2], true);
        }
    }
}
