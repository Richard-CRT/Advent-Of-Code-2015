using AdventOfCodeUtilities;

void P1(List<string> boxStrings)
{
    int result = 0;
    foreach (string boxString in boxStrings)
    {
        string[] strSplit = boxString.Split('x');
        int l = int.Parse(strSplit[0]);
        int w = int.Parse(strSplit[1]);
        int h = int.Parse(strSplit[2]);

        int sa1 = l * h;
        int sa2 = l * w;
        int sa3 = w * h;
        int sa = (2 * sa1) + (2 * sa2) + (2 * sa3);
        sa += Math.Min(sa1, Math.Min(sa2, sa3));
        result += sa;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(List<string> boxStrings)
{
    int result = 0;
    foreach (string boxString in boxStrings)
    {
        string[] strSplit = boxString.Split('x');
        int l = int.Parse(strSplit[0]);
        int w = int.Parse(strSplit[1]);
        int h = int.Parse(strSplit[2]);

        int v = l * w * h;
        int p1 = 2 * l + 2 * h;
        int p2 = 2 * l + 2 * w;
        int p3 = 2 * h + 2 * w;
        int p = Math.Min(p1, Math.Min(p2, p3));
        result += p + v;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList);
P2(inputList);