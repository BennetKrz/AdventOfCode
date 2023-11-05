namespace AdventOfCode._2019;

public class Day5 : IAdventDay
{
    private string[] input;

    private int latestOutputPartOne;
    private int latestOutputPartTwo;
    public Day5()
    {
        input = Helper.GetInputOneLine(5).Split(',');
    }

    public long PartOne()
    {
        return ExecuteProgramPartOne();
    }

    public long PartTwo()
    {
        input = Helper.GetInputOneLine(5).Split(',');
        return ExecuteProgramPartTwo();
    }

    private long ExecuteProgramPartOne()
    {
        int currentOpCodePosition = 0;

        string instruction = input[currentOpCodePosition];
            
        while (instruction != "99")
        {
            instruction = input[currentOpCodePosition];
            char code = instruction[^1];
            switch (code)
            {
                case '1':
                    ExecuteAdd(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '2':
                    ExecuteMultiply(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '3':
                    ExecuteInput(1, input[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    currentOpCodePosition += 2;
                    break;
                case '4':
                    ExecuteOutput(input[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    currentOpCodePosition += 2;
                    break;
            }
        }
        return latestOutputPartOne;
    }

    private long ExecuteProgramPartTwo()
    {
        int currentOpCodePosition = 0;

        string instruction = input[currentOpCodePosition];
            
        while (instruction != "99")
        {
            instruction = input[currentOpCodePosition];
            char code = instruction[^1];
            switch (code)
            {
                case '1':
                    ExecuteAdd(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '2':
                    ExecuteMultiply(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '3':
                    ExecuteInput(5, input[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    currentOpCodePosition += 2;
                    break;
                case '4':
                    ExecuteOutput(input[currentOpCodePosition..(currentOpCodePosition + 2)]);
                    currentOpCodePosition += 2;
                    break;
                case '5':
                    int jump = JumpIfTrue(input[currentOpCodePosition..(currentOpCodePosition + 3)]);
                    currentOpCodePosition = jump == 3 ? (currentOpCodePosition + 3) : jump;
                    break;
                case '6':
                    int jumpBy = JumpIfFalse(input[currentOpCodePosition..(currentOpCodePosition + 3)]);
                    currentOpCodePosition = jumpBy == 3 ? (currentOpCodePosition + 3) : jumpBy;
                    break;
                case '7':
                    ExecuteLessThan(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
                case '8':
                    ExecuteEquals(input[currentOpCodePosition..(currentOpCodePosition + 4)]);
                    currentOpCodePosition += 4;
                    break;
            }
        }
        return latestOutputPartOne;
    }

    private void ExecuteAdd(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        input[operandThree] = (operandOne + operandTwo).ToString();
    }

    private void ExecuteMultiply(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        input[operandThree] = (operandOne * operandTwo).ToString();
    }

    private void ExecuteInput(int inputValue, params string[] values)
    {
        int position = int.Parse(values[1]);
        input[position] = inputValue.ToString();
    }

    private void ExecuteOutput(params string[] values)
    {
        (int mode1, int mode2, int mode3) modes = GetParameterMode(values[0]);
        int output = modes.mode1 == 1 ? int.Parse(values[1]) : int.Parse(input[int.Parse(values[1])]);
        latestOutputPartOne = output;
        //Console.WriteLine(output);
    }

    private int JumpIfTrue(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        if (operandOne != 0)
        {
            return operandTwo;
        }

        return 3;
    }

    private int JumpIfFalse(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        if (operandOne == 0)
        {
            return operandTwo;
        }

        return 3;
    }

    private void ExecuteLessThan(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        if (operandOne < operandTwo)
        {
            input[operandThree] = "1";
        }
        else
        {
            input[operandThree] = "0";
        }
    }

    private void ExecuteEquals(params string[] values)
    {
        (int mode1, int mode2, int mode3) parameterModes = GetParameterMode(values[0]);
        int operandOne = parameterModes.mode1 == 0 ? int.Parse(input[int.Parse(values[1])]) : int.Parse(values[1]);
        int operandTwo = parameterModes.mode2 == 0 ? int.Parse(input[int.Parse(values[2])]) : int.Parse(values[2]);
        int operandThree = int.Parse(values[3]);
        if (operandOne == operandTwo)
        {
            input[operandThree] = "1";
        }
        else
        {
            input[operandThree] = "0";
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