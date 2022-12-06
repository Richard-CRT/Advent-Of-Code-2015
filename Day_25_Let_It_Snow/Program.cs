using AdventOfCodeUtilities;
using System.Data;

List<string> inputList = AoCUtilities.GetInputLines();
string[] split = inputList[0].Split(' ');
int targetRow = int.Parse(split[16].Substring(0, split[16].Length - 1));
int targetColumn = int.Parse(split[18].Substring(0, split[18].Length - 1));


void P1()
{
    Int64 previousCode = 20151125;
    int row = 1;
    int column = 1;
    int maxRow = 1;
    while (row != targetRow || column != targetColumn)
    {
        previousCode = (previousCode * 252533) % 33554393;
        if (row > 1)
        {
            row--;
            column++;
        }
        else
        {
            column = 1;
            maxRow++;
            row = maxRow;
        }
    }
    Console.WriteLine(previousCode);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();