using AdventOfCodeUtilities;
using System.Linq;

List<List<int>>? foo(int target, List<int> options, int startIndex, int length, int depth = 0)
{
    List<List<int>>? ret = null;
    for (int chosenOptionIndex = startIndex; chosenOptionIndex < options.Count - length + 1; chosenOptionIndex++)
    {
        int chosenOption = options[chosenOptionIndex];
        /*
        for (int i = 0; i < depth; i++)
            Console.Write(" ");
        Console.WriteLine(chosenOption);
        */
        if (length == 1)
        {
            if (target == chosenOption)
            {
                if (ret is null)
                    ret = new List<List<int>>();
                ret.Add(new List<int> { chosenOption });
            }
        }
        else
        {
            List<List<int>>? val = foo(target - chosenOption, options, chosenOptionIndex + 1, length - 1, depth + 1);
            if (val != null)
            {
                foreach (List<int> l in val)
                {
                    l.Insert(0, chosenOption);
                    if (ret is null)
                        ret = new List<List<int>>();
                    ret.Add(l);
                }
            }
        }
    }
    return ret;
}

List<string> inputList = AoCUtilities.GetInputLines();
List<int> PackageWeights = inputList.Select(x => int.Parse(x)).ToList();
int totalWeight = PackageWeights.Sum();

void P1()
{
    int targetWeight = totalWeight / 3;

    List<List<int>>? fewestCombinations = null;
    for (int i = 1; i < PackageWeights.Count; i++)
    {
        fewestCombinations = foo(targetWeight, PackageWeights, 0, i);
        if (fewestCombinations != null)
            break;
    }

    if (fewestCombinations != null)
    {
        Int64 minQuantumEntanglement = Int64.MaxValue;
        foreach (List<int> combination in fewestCombinations)
        {
            Int64 quantumEntanglement = 1;
            foreach (int val in combination)
                quantumEntanglement *= val;
            if (quantumEntanglement < minQuantumEntanglement)
                minQuantumEntanglement = quantumEntanglement;
        }
        Console.WriteLine(minQuantumEntanglement);
    }
    else
        Console.WriteLine("null");
    Console.ReadLine();
}

void P2()
{
    int targetWeight = totalWeight / 4;

    List<List<int>>? fewestCombinations = null;
    for (int i = 1; i < PackageWeights.Count; i++)
    {
        fewestCombinations = foo(targetWeight, PackageWeights, 0, i);
        if (fewestCombinations != null)
            break;
    }

    if (fewestCombinations != null)
    {
        Int64 minQuantumEntanglement = Int64.MaxValue;
        foreach (List<int> combination in fewestCombinations)
        {
            Int64 quantumEntanglement = 1;
            foreach (int val in combination)
                quantumEntanglement *= val;
            if (quantumEntanglement < minQuantumEntanglement)
                minQuantumEntanglement = quantumEntanglement;
        }
        Console.WriteLine(minQuantumEntanglement);
    }
    else
        Console.WriteLine("null");
    Console.ReadLine();
}

P1();
P2();
