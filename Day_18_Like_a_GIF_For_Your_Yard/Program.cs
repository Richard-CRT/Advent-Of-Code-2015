using AdventOfCodeUtilities;

int dim = 100;
bool[,] map = new bool[dim, dim];

void P1()
{
    for (int i = 0; i < 100; i++)
    {
        TickMap();
        PrintMap();
    }
    int result = 0;
    for (int y = 0; y < dim; y++)
    {
        for (int x = 0; x < dim; x++)
        {
            if (GetSafe(x, y))
                result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    for (int i = 0; i < 100; i++)
    {
        TickMap(true);
        PrintMap(true);
    }
    int result = 0;
    for (int y = 0; y < dim; y++)
    {
        for (int x = 0; x < dim; x++)
        {
            if (GetSafe(x, y, true))
                result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void PrintMap(bool p2 = false)
{
    for (int y = 0; y < dim; y++)
    {
        for (int x = 0; x < dim; x++)
        {
            if (GetSafe(x, y, p2))
                AoCUtilities.DebugWrite("#");
            else
                AoCUtilities.DebugWrite(".");
        }
        AoCUtilities.DebugWriteLine();
    }
    AoCUtilities.DebugWriteLine();
}

bool GetSafe(int x, int y, bool p2 = false)
{
    if ((x < 0 || x >= dim) || (y < 0 || y >= dim))
        return false;
    if (p2 && ((x == 0 && y == 0) || (x == dim - 1 && y == dim - 1) || (x == dim - 1 && y == 0) || (x == 0 && y == dim - 1)))
        return true;
    return map[y, x];
}

void TickMap(bool p2 = false)
{
    bool[,] nextMap = (bool[,])map.Clone();
    for (int y = 0; y < dim; y++)
    {
        for (int x = 0; x < dim; x++)
        {
            int neighboursOn = 0;
            if (GetSafe(x - 1, y - 1, p2)) neighboursOn++;
            if (GetSafe(x, y - 1, p2)) neighboursOn++;
            if (GetSafe(x + 1, y - 1, p2)) neighboursOn++;
            if (GetSafe(x + 1, y, p2)) neighboursOn++;
            if (GetSafe(x + 1, y + 1, p2)) neighboursOn++;
            if (GetSafe(x, y + 1, p2)) neighboursOn++;
            if (GetSafe(x - 1, y + 1, p2)) neighboursOn++;
            if (GetSafe(x - 1, y, p2)) neighboursOn++;
            if (GetSafe(x, y, p2))
                nextMap[y, x] = neighboursOn == 2 || neighboursOn == 3 ? true : false;
            else
                nextMap[y, x] = neighboursOn == 3 ? true : false;
        }
    }
    map = nextMap;
}

List<string> inputList = AoCUtilities.GetInputLines();
int y = 0;
foreach (string line in inputList)
{
    int x = 0;
    foreach (char c in line)
    {
        map[y, x] = c == '#';
        x++;
    }
    y++;
}
bool[,] initialMap = (bool[,])map.Clone();
PrintMap();

P1();
map = initialMap;
P2();