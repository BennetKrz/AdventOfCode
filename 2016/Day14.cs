using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016;

public class Day14 : AdventBase
{
    private readonly string input;

    MD5 md5 = MD5.Create();

    long md5Calls = 0;

    Dictionary<string, string> hashTablePartOne = new();
    Dictionary<string, string> hashTablePartTwo = new();

    public Day14() : base(2016, 14)
    {
        input = InputOneLine;
    }

    public override long PartOne()
    {
        int countValidKeys = 0;
        int counter = 0;
        while (countValidKeys < 64)
        {
            string data = input + counter.ToString();
            string hash = GetHash(data);
            (bool hasThreeInRow, char charRepeatedThreeTimes) result = HasThreeInRow(hash);
            if (result.hasThreeInRow)
            {
                if (CheckNext1KHashes(counter, result.charRepeatedThreeTimes))
                {
#if DEBUG
                    Console.WriteLine(counter);
#endif
                    countValidKeys++;
                }
            }

            counter++;
        }

        return counter - 1;
    }

    public override long PartTwo()
    {
        int countValidKeys = 0;
        int counter = 0;
        while (countValidKeys < 64)
        {
            string data = input + counter.ToString();
            string hash = GetStretchedHash(data, 2016);
            (bool hasThreeInRow, char charRepeatedThreeTimes) result = HasThreeInRow(hash);
            if (result.hasThreeInRow)
            {
                if (CheckNext1KHashesStreched(counter, result.charRepeatedThreeTimes))
                {
                    countValidKeys++;
#if DEBUG
                    Console.WriteLine($"Valid keys: {countValidKeys}");
#endif
                }
            }

            counter++;
#if DEBUG
            if (counter % 100 == 0)
            {
                Console.WriteLine(counter);
            }
#endif
        }
#if DEBUG
        Console.WriteLine($"Es wurden {md5Calls} benötigt.");
#endif
        return counter - 1;
    }

    string GetHash(string s)
    {
        if (hashTablePartOne.ContainsKey(s))
            return hashTablePartOne[s];
        byte[] stringAsByteArray = Encoding.ASCII.GetBytes(s);
        byte[] hash = md5.ComputeHash(stringAsByteArray);
        string result = Convert.ToHexString(hash).ToLower();
        hashTablePartOne.Add(s, result);
        return result;
    }

    private bool CheckNext1KHashes(int counter, char charToLookFor)
    {
        for (int i = 1; i < 1001; i++)
        {
            string index = input + (counter + i).ToString();
            if (hashTablePartOne.ContainsKey(index))
            {
                if (HasFiveInRow(hashTablePartOne[index], charToLookFor))
                {
                    return true;
                }
            }
            else
            {
                string hash = GetHash(index);
                if (HasFiveInRow(hash, charToLookFor))
                {
                    return true;
                }
            }
        }

        return false;
    }

    string GetStretchedHash(string s, int times)
    {
        if (hashTablePartTwo.ContainsKey(s))
            return hashTablePartTwo[s];
        byte[] stringAsByteArray = Encoding.ASCII.GetBytes(s);
        byte[] hash = md5.ComputeHash(stringAsByteArray);
        md5Calls++;
        string result = Convert.ToHexString(hash).ToLower();
        for (int i = 0; i < times; i++)
        {
            stringAsByteArray = Encoding.ASCII.GetBytes(result);
            hash = md5.ComputeHash(stringAsByteArray);
            md5Calls++;
            result = Convert.ToHexString(hash).ToLower();
        }

        hashTablePartTwo.Add(s, result);
        return result;
    }

    private bool CheckNext1KHashesStreched(int counter, char charToLookFor)
    {
        for (int i = 1; i < 1001; i++)
        {
            string index = input + (counter + i).ToString();
            if (hashTablePartTwo.ContainsKey(index))
            {
                if (HasFiveInRow(hashTablePartTwo[index], charToLookFor))
                {
                    return true;
                }
            }
            else
            {
                string hash = GetStretchedHash(index, 2016);
                if (HasFiveInRow(hash, charToLookFor))
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool HasFiveInRow(string hasToCheck, char charToCheck)
    {
        string pattern = $"{charToCheck}{{5}}";
        return Regex.IsMatch(hasToCheck, pattern);
    }

    (bool hasThreeInRow, char charRepeatedThreeTimes) HasThreeInRow(string hasToCheck)
    {
        Match match = Regex.Match(hasToCheck, @"(.)\1\1");
        (bool hasThreeInRow, char charRepeatedThreeTimes) result = (match.Success,
            match.Success ? match.Value[0] : '\0');
        return result;
    }
}