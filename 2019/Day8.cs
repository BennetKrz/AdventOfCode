using System.Text;

namespace AdventOfCode._2019;

public class Day8 : IAdventDay
{
    private string input = Helper.GetInputOneLine(8);
    private const int ImageWidth = 25;
    private const int ImageHeight = 6;
    
    public long PartOne()
    {
        int layerSize = ImageHeight * ImageWidth;
        int layerCount = this.input.Length / layerSize;

        string[] layers = ParseLayers(layerCount, layerSize);

        string minLayer = layers.MinBy(l => l.Count(c => c == '0'));
        
        return minLayer.Count(c => c == '1') * minLayer.Count(c => c == '2');
    }

    public long PartTwo()
    {
        int layerSize = ImageHeight * ImageWidth;
        int layerCount = this.input.Length / layerSize;

        string[] layers = ParseLayers(layerCount, layerSize);

        char[] resultLayer = new char[layerSize];
        
        for (int pos = 0; pos < layerSize; pos++)
        {
            for (int l = 0; l < layerCount; l++)
            {
                if (layers[l][pos] != '2')
                {
                    resultLayer[pos] = layers[l][pos];
                    break;
                }
            }
        }
        
        PrintLayer(resultLayer, ImageWidth);
        
        return long.MinValue;
    }

    private string[] ParseLayers(int layerCount, int layerSize)
    {
        return Enumerable.Range(0, layerCount)
            .Select(i => this.input.Substring(i * layerSize, layerSize))
            .ToArray();
    }

    private void PrintLayer(char[] layer, int imageWidth)
    {
        StringBuilder sb = new();

        for (int pos = 0; pos < layer.Length; pos++)
        {
            if (pos % imageWidth == 0)
            {
                sb.AppendLine();
            }

            sb.Append(layer[pos] == '1' ? '1' : ' ');
        }

        Console.WriteLine(sb.ToString());
    }
}