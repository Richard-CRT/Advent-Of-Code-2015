using AdventOfCodeUtilities;

void P1(string directions)
{
    int floor = 0;
    foreach (char c in directions)
    {
        if (c == '(')
            floor++;
        else if (c == ')')
            floor--;
    }
    Console.WriteLine(floor);
    Console.ReadLine();
}

void P2(string directions)
{
    int floor = 0;
    int i;
    for (i =0; i < directions.Length;i++)
    {
        char c = directions[i];
        if (c == '(')
            floor++;
        else if (c == ')')
            floor--;
        if (floor == -1)
            break;
    }
    Console.WriteLine(i+1);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList[0]);
P2(inputList[0]);