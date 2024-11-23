namespace AdventOfCode._2016;

public class Day12 : AdventBase
{
    private readonly string[] input;

    public Day12() : base(2016, 12)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        Dictionary<char, int> registers = new()
        {
            { 'a', 0 },
            { 'b', 0 },
            { 'c', 0 },
            { 'd', 0 }
        };
        
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            switch (input[i][..3])
            {
                case "cpy":
                    string valueToCpy = line.Split(' ')[1];
                    int valueToCpyInt;
                    if (valueToCpy.All(c => char.IsDigit(c)))
                    {
                        valueToCpyInt = int.Parse(valueToCpy);
                    }
                    else
                    {
                        valueToCpyInt = registers[valueToCpy.ToCharArray()[0]];
                    }

                    registers[line[^1]] = valueToCpyInt;
                    break;
                case "inc":
                    registers[line[^1]]++;
                    break;
                case "dec":
                    registers[line[^1]]--;
                    break;
                case "jnz":
                    char valueToCheck = line.Split(' ')[1].ToCharArray()[0];
                    int valueToCheckInt = char.IsDigit(valueToCheck) ? int.Parse(valueToCheck.ToString()) : registers[valueToCheck];

                    if (valueToCheckInt != 0)
                    {
                        i += int.Parse(line.Split(' ')[2]) - 1;
                    }

                    break;
            }
        }

        return registers['a'];
    }


    public override long PartTwo()
    {
        Dictionary<char, int> registers = new()
        {
            { 'a', 0 },
            { 'b', 0 },
            { 'c', 1 },
            { 'd', 0 }
        };
        
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            switch (input[i].Substring(0, 3))
            {
                case "cpy":
                    string valueToCpy = line.Split(' ')[1];
                    int valueToCpyInt = valueToCpy.All(char.IsDigit) ? int.Parse(valueToCpy) : registers[valueToCpy.ToCharArray()[0]];

                    registers[line[^1]] = valueToCpyInt;
                    break;
                case "inc":
                    registers[line[^1]]++;
                    break;
                case "dec":
                    registers[line[^1]]--;
                    break;
                case "jnz":
                    char valueToCheck = line.Split(' ')[1].ToCharArray()[0];
                    int valueToCheckInt;
                    if (char.IsDigit(valueToCheck))
                    {
                        valueToCheckInt = int.Parse(valueToCheck.ToString());
                    }
                    else
                    {
                        valueToCheckInt = registers[valueToCheck];
                    }

                    if (valueToCheckInt != 0)
                    {
                        i += int.Parse(line.Split(' ')[2]) - 1;
                    }

                    break;
            }
        }

        return registers['a'];
    }
}