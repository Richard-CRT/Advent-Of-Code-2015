using AdventOfCodeUtilities;

List<string> inputList = AoCUtilities.GetInputLines();
int target = int.Parse(inputList[0]);

void P1()
{
    int[] houses = new int[(target / 10) + 1];
    for (int i = 1; i <= target / 10; i++)
    {
        for (int j = i; j <= target / 10; j += i)
        {
            houses[j] += i * 10;
        }
    }

    int x;
    for (x = 0; houses[x] < 33100000; x++) ;
    Console.WriteLine(x);
    Console.ReadLine();
}

void P2()
{
    int[] houses = new int[(target / 10) + 1];
    for (int i = 1; i <= target / 10; i++)
    {
        for (int j = i; j <= Math.Min(50 * i, target / 10); j += i)
        {
            houses[j] += i * 11;
        }
    }

    int x;
    for (x = 0; houses[x] < 33100000; x++) ;
    Console.WriteLine(x);
    Console.ReadLine();
}

P1();
P2();