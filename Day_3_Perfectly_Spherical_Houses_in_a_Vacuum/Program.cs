using AdventOfCodeUtilities;

void P1(string directions)
{
    int x = 0;
    int y = 0;
    Dictionary<(int,int),int> presents = new Dictionary<(int,int),int>();
    presents[(x, y)] = 1;
    foreach (var direction in directions)
    {
        switch (direction)
        {
            case 'v':
                y++;
                break;
            case '>':
                x++;
                break;
            case '<':
                x--;
                break;
            case '^':
                y--;
                break;
        }
        (int, int) key = (x, y);
        if (presents.ContainsKey(key))
            presents[key]++;
        else
            presents[key] = 1;
    }
    Console.WriteLine(presents.Count);
    Console.ReadLine();
}

void P2(string directions)
{
    int sx = 0;
    int sy = 0;
    int rx = 0;
    int ry = 0;
    bool santaTurn = true;
    Dictionary<(int, int), int> presents = new Dictionary<(int, int), int>();
    presents[(0, 0)] = 2;
    foreach (var direction in directions)
    {
        switch (direction)
        {
            case 'v':
                if (santaTurn)
                    sy++;
                else
                    ry++;
                break;
            case '>':
                if (santaTurn)
                    sx++;
                else
                    rx++;
                break;
            case '<':
                if (santaTurn)
                    sx--;
                else
                    rx--;
                break;
            case '^':
                if (santaTurn)
                    sy--;
                else
                    ry--;
                break;
        }
        (int, int) key;
        if (santaTurn)
            key = (sx, sy);
        else
            key = (rx, ry);
        if (presents.ContainsKey(key))
            presents[key]++;
        else
            presents[key] = 1;

        santaTurn = !santaTurn;
    }
    Console.WriteLine(presents.Count);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList[0]);
P2(inputList[0]);