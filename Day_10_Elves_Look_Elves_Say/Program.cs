using AdventOfCodeUtilities;

List<int> ApplyProcess(List<int> sequence)
{
    List<int> newSequence = new List<int>();

    int runVal = -1;
    int runLen = 0;

    foreach (int i in sequence)
    {
        if (i != runVal)
        {
            if (runLen != 0)
            {
                newSequence.Add(runLen);
                newSequence.Add(runVal);
            }
            runVal = i;
            runLen = 1;
        }
        else
        {
            runLen++;
        }
    }
    newSequence.Add(runLen);
    newSequence.Add(runVal);
    return newSequence;
}

void P1(string digits)
{ 
    List<int> sequence = new List<int>();
    foreach (char c in digits)
        sequence.Add(int.Parse(c.ToString()));

    for (int i = 0; i < 40; i++)
    {
        sequence = ApplyProcess(sequence);
    }

    int result = sequence.Count;
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(string digits)
{
    List<int> sequence = new List<int>();
    foreach (char c in digits)
        sequence.Add(int.Parse(c.ToString()));

    for (int i = 0; i < 50; i++)
    {
        sequence = ApplyProcess(sequence);
        Console.WriteLine(i);
    }

    int result = sequence.Count;
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList[0]);
P2(inputList[0]);