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
        List<int> diskMap = ParseInput(input.Select(x => int.Parse(x.ToString())).ToList());

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

        return diskMap
            .Where(x => x != -1)
            .Select((x, i) => (long)x * i)
            .Sum();
    }

    public override long PartTwo()
    {
        List<(int index, int length)> diskMap = ParseInput2();

        for (int i = diskMap.Count - 1; i >= 0; i -= 2)
        {
            int freeSpaceIndex = FindEnoughFreeSpaceIndex(diskMap, diskMap[i].length, i);
            if (freeSpaceIndex == -1)
            {
                continue;
            }

            // Das zu bewegende sichern und dann entfernen
            (int index, int Length) beforeFreeSpace = diskMap[i - 1];
            (int index, int Length) toMove = diskMap[i];
            (int index, int Length) afterFreeSpace = (0, 0);
            if (i + 1 < diskMap.Count)
            {
                afterFreeSpace = i + 1 < diskMap.Count ? diskMap[i + 1] : (0, 0);
                diskMap.RemoveAt(i + 1);
            }
            diskMap.RemoveAt(i);
            diskMap[i - 1] = (-1, beforeFreeSpace.Length + toMove.Length + afterFreeSpace.Length);
            
            //Neues schreiben
            // Stelle wo eingefügt wird, auf 0 setzen
            (int index, int Length) freeSpaceValue = diskMap[freeSpaceIndex];
            diskMap[freeSpaceIndex] = (-1, 0);
            diskMap.Insert(freeSpaceIndex + 1, toMove);
            diskMap.Insert(freeSpaceIndex + 2, (-1, freeSpaceValue.Length - toMove.Length));

            i += 2;
        }

        return CalculateChecksum(diskMap);
    }

    private long CalculateChecksum(List<(int index, int length)> diskMap)
    {
        return diskMap
            .SelectMany(x => Enumerable.Repeat(x.index, x.length))
            .Select((x, i) => x == -1 ? 0 : (long)x * i)
            .Sum();
    }

    private int FindFirstFreeSpaceIndex(List<int> diskMap, int index)
    {
        return diskMap.IndexOf(-1, 0, index);
    }

    private int FindEnoughFreeSpaceIndex(List<(int index, int length)> diskMap, int freeSpaceRequired, int index)
    {
        for (int i = 1; i < index; i += 2)
        {
            if (diskMap[i].length >= freeSpaceRequired)
            {
                return i;
            }
        }

        return -1;
    }

    private List<int> ParseInput(List<int> disk)
    {
        List<int> diskMap = [];
        bool freeBlocks = false;

        for (int i = 0; i < disk.Count; i++)
        {
            int length = disk[i];
            if (freeBlocks)
            {
                for (int j = 0; j < length; j++)
                {
                    diskMap.Add(-1);
                }
            }
            else
            {
                for (int j = 0; j < length; j++)
                {
                    diskMap.Add(i / 2);
                }
            }

            freeBlocks = !freeBlocks;
        }

        return diskMap;
    }
    
    private List<(int index, int length)> ParseInput2()
    {
        List<(int index, int length)> diskMap = [];
        bool freeSpace = false;
        
        for (int i = 0; i < input.Length; i++)
        {
            if (freeSpace)
            {
                diskMap.Add((-1, int.Parse(input[i].ToString())));
            }
            else
            {
                diskMap.Add((i / 2, int.Parse(input[i].ToString())));
            }
            
            freeSpace = !freeSpace;
        }
        
        return diskMap;
    }
}