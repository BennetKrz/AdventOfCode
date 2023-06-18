namespace AdventOfCode._2021 {
    public class Day2 : IAdventDay {
        string[] input;

        public Day2() {
            this.input = Helper.GetInput(2);
        }

        public long PartOne() {
            List<Instruction> instructions = new();
            int forward = 0;
            int depth = 0;
            for (int i = 0; i < input.Length; i++) {
                Instruction instruction;
                if (input[i][0] == 'f') {
                    instruction = new(0, 0);
                } else {
                    instruction = new(0, 1);
                }
                string[] tmp = input[i].Split(' ');
                if (instruction.dir == 0) {
                    instruction.value = Convert.ToInt32(tmp[1]);
                } else {
                    if (tmp[0][0] == 'd') {
                        instruction.value = Convert.ToInt32(tmp[1]);
                    } else {
                        instruction.value = Convert.ToInt32(tmp[1]) * -1;
                    }
                }
                instructions.Add(instruction);
            }
            for (int i = 0; i < instructions.Count; i++) {
                if (instructions[i].dir == 0) {
                    forward += instructions[i].value;
                } else {
                    depth += instructions[i].value;
                }
            }
            return forward * depth;
        }

        public long PartTwo() {
            int aim = 0;
            int horizontal = 0;
            int depth = 0;
            List<Instruction> instructions = new();
            for (int i = 0; i < input.Length; i++) {
                Instruction instruction;
                if (input[i][0] == 'f') {
                    instruction = new(0, 0);
                } else {
                    instruction = new(0, 1);
                }
                string[] tmp = input[i].Split(' ');
                if (instruction.dir == 0) {
                    instruction.value = Convert.ToInt32(tmp[1]);
                } else {
                    if (tmp[0][0] == 'd') {
                        instruction.value = Convert.ToInt32(tmp[1]);
                    } else {
                        instruction.value = Convert.ToInt32(tmp[1]) * -1;
                    }
                }
                instructions.Add(instruction);
            }
            for (int i = 0; i < instructions.Count; i++) {
                if (instructions[i].dir == 0) {
                    horizontal += instructions[i].value;
                    depth += aim * instructions[i].value;
                } else {
                    aim += instructions[i].value;
                }
            }
            return horizontal * depth;
        }

        struct Instruction {

            public int value;
            public int dir;

            public Instruction(int _value, int _dir) {
                value = _value;
                dir = _dir;
            }
        }
    }
}
