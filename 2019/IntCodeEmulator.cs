namespace AdventOfCode._2019;

public class IntCodeEmulator(string[] instructions, List<int>? inputs, bool printOutputs)
{
    private string[] instructions = instructions;
    private List<int>? inputs = inputs;
    private bool printOutputs = printOutputs;
    
    private int inputCounter = 0;
    private string latestOutput;

    public string GetLatestOutput()
    {
        return latestOutput;
    }

    public string GetAddress(int addressPosition)
    {
        if (addressPosition >= 0 && addressPosition < instructions.Length)
        {
            return instructions[addressPosition];
        }

        return "";
    }
    
    public void ExecuteProgram()
    {
        int currentOpCodePosition = 0;

        string instruction = instructions[currentOpCodePosition];
            
        while (instruction != "99")
        {
            instruction = instructions[currentOpCodePosition];
            char code = instruction[^1];
            switch (code)
            {
                case '1':
                    ExecuteAdd(instructions[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '2':
                    ExecuteMultiply(instructions[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '3':
                    ExecuteInput(inputs[inputCounter], instructions[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    inputCounter++;
                    currentOpCodePosition += 2;
                    break;
                case '4':
                    ExecuteOutput(instructions[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    currentOpCodePosition += 2;
                    break;
                case '5':
                    int jump = JumpIfTrue(instructions[currentOpCodePosition..(currentOpCodePosition + 3)]);
                    currentOpCodePosition = jump == 3 ? (currentOpCodePosition + 3) : jump;
                    break;
                case '6':
                    int jumpBy = JumpIfFalse(instructions[currentOpCodePosition..(currentOpCodePosition + 3)]);
                    currentOpCodePosition = jumpBy == 3 ? (currentOpCodePosition + 3) : jumpBy;
                    break;
                case '7':
                    ExecuteLessThan(instructions[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '8':
                    ExecuteEquals(instructions[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
            }
        }
    }

    private void ExecuteAdd(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        instructions[operandThree] = (operandOne + operandTwo).ToString();
    }

    private void ExecuteMultiply(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        instructions[operandThree] = (operandOne * operandTwo).ToString();
    }

    private void ExecuteInput(int inputValue, params string[] values)
    {
        int position = int.Parse(values[1]);
        instructions[position] = inputValue.ToString();
    }

    private void ExecuteOutput(params string[] values)
    {
        (int mode1, int mode2, int mode3) modes = GetParameterMode(values[0]);
        int output = modes.mode1 == 1 ? int.Parse(values[1]) : int.Parse(instructions[int.Parse(values[1])]);
        latestOutput = output.ToString();
        if (printOutputs)
        {
            Console.WriteLine(output);  
        }
    }

    private int JumpIfTrue(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        if (operandOne != 0)
        {
            return operandTwo;
        }

        return 3;
    }

    private int JumpIfFalse(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        if (operandOne == 0)
        {
            return operandTwo;
        }

        return 3;
    }

    private void ExecuteLessThan(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        if (operandOne < operandTwo)
        {
            instructions[operandThree] = "1";
        }
        else
        {
            instructions[operandThree] = "0";
        }
    }

    private void ExecuteEquals(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(instructions[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(instructions[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        if (operandOne == operandTwo)
        {
            instructions[operandThree] = "1";
        }
        else
        {
            instructions[operandThree] = "0";
        }
    }
    
    private (int mode1, int mode2, int mode3) GetParameterMode(string opCode)
    {
        int modeCount = 2;
        int[] modes = new int[3];
        for (int i = opCode.Length - 3; i >= 0; i--)
        {
            if (opCode[i] == '1')
            {
                modes[modeCount] = 1;
            }
            else
            {
                modes[modeCount] = 0;
            }

            modeCount--;
        }
        return (modes[2], modes[1], modes[0]);
    }
}