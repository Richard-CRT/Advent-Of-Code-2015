using AdventOfCodeUtilities;

(int, int, int, int, int) InstToCoords(string inst)
{
    string[] split = inst.Split(' ');
    string startCoord = "";
    string endCoord = "";
    int action = 0;
    if (split[0] == "toggle")
    {
        startCoord = split[1];
        endCoord = split[3];
        action = 0;
    }
    else if (split[1] == "on")
    {
        startCoord = split[2];
        endCoord = split[4];
        action = 1;
    }
    else if (split[1] == "off")
    {
        startCoord = split[2];
        endCoord = split[4];
        action = 2;
    }

    split = startCoord.Split(',');
    int startX = int.Parse(split[0]);
    int startY = int.Parse(split[1]);
    split = endCoord.Split(',');
    int endX = int.Parse(split[0]);
    int endY = int.Parse(split[1]);

    return (startX, startY, endX, endY, action);
}

void P1(List<string> instructions)
{
    bool[][] map = new bool[1000][];
    for (int y = 0; y < map.Length; y++)
        map[y] = new bool[1000];

    foreach (string inst in instructions)
    {
        var (startX, startY, endX, endY, action) = InstToCoords(inst);

        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                if (action == 1)
                    map[y][x] = true;
                else if (action == 2)
                    map[y][x] = false;
                else if (action == 0)
                    map[y][x] = !map[y][x];
            }
        }
    }

    int result = 0;
    for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
            if (map[y][x])
                result++;
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(List<string> instructions)
{
    int[][] map = new int[1000][];
    for (int y = 0; y < map.Length; y++)
        map[y] = new int[1000];

    foreach (string inst in instructions)
    {
        var (startX, startY, endX, endY, action) = InstToCoords(inst);

        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                if (action == 1)
                    map[y][x]++;
                else if (action == 2)
                    map[y][x] = map[y][x] > 0 ? map[y][x] - 1 : 0;
                else if (action == 0)
                    map[y][x] += 2;
            }
        }
    }

    long result = 0;
    for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
            result += map[y][x];
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList);
P2(inputList);