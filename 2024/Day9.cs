using System.Text;

namespace AdventOfCode._2024;

public class Day9 : AdventBase
{
    private string input;
    
    public Day9() : base(2024, 9)
    {
        this.input = base.InputOneLine;
    }

    public override long PartOne()
    {
        List<int> diskMap = ParseInput();

        for (int i = diskMap.Count - 1; i >= 0; i--)
        {
            int freeSpaceIndex = FindFirstFreeSpaceIndex(diskMap, i);
            if (freeSpaceIndex == -1)
            {
                continue;
            }
            
            diskMap[freeSpaceIndex] = diskMap[i];
            diskMap[i] = -1;
        }
        
        return CalculateChecksum(diskMap);
    }

    public override long PartTwo()
    {
        List<int> diskMap = ParseInput();

        for (int i = diskMap.Count - 1; i >= 0; i--)
        {
            int freeSpaceIndex = FindEnoughFreeSpaceIndex(diskMap, diskMap[i], i);
            if (freeSpaceIndex == -1)
            {
                continue;
            }
            
            diskMap[freeSpaceIndex] = diskMap[i];
            diskMap[i] = -1;
        }
        
        return CalculateChecksum(diskMap);
    }

    private int FindEnoughFreeSpaceIndex(List<int> diskMap, int freeSpaceRequired, int index)
    {
        for (int i = 1; i < index; i += 2)
        {
            if (diskMap[i] >= freeSpaceRequired)
            {
                return i;
            }
        }

        return -1;
    }

    private int FindFirstFreeSpaceIndex(List<int> diskMap, int index)
    {
        return diskMap.IndexOf(-1, 0, index);
    }

    private long CalculateChecksum(List<int> diskMap)
    {
        return diskMap
            .Where(x => x != -1)
            .Select((x, i) => (long)x * i)
            .Sum();
    }

    private List<int> ParseInput()
    {
        List<int> diskMap = [];
        bool freeBlocks = false;
        
        for (int i = 0; i < this.input.Length; i++)
        {
            int length = int.Parse(input[i].ToString());
            if (freeBlocks)
            {
                for(int j = 0; j < length; j++)
                {
                    diskMap.Add(-1);
                }    
            }
            else
            {
                for(int j = 0; j < length; j++)
                {
                    diskMap.Add(i / 2);
                }
            }
            
            freeBlocks = !freeBlocks;
        }
        
        return diskMap;
    }

    private void Print(List<int> diskMap)
    {
        StringBuilder sb = new();
        foreach (int id in diskMap)
        {
            sb.Append(id == -1 ? "." : id);
        }
        
        Console.WriteLine(sb.ToString());
    }
}