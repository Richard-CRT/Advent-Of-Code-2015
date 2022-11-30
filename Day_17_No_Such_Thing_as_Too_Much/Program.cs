using AdventOfCodeUtilities;
using System.Linq;

List<string> inputList = AoCUtilities.GetInputLines();
List<int> containers = inputList.Select(x => int.Parse(x)).ToList();

List<List<int>>? Recurse(int volume, List<int> containers, int depth = 0)
{
    //Console.WriteLine(depth + " v" + volume + " c" + string.Join(", ", containers));
    if (volume < 0)
        return null;
    else if (volume == 0)
        return new List<List<int>>();
    else
    {
        List<List<int>> overallResult = new List<List<int>>();
        for (int i = 0; i < containers.Count; i++)
        {
            int selectedContainer = containers[i];
            List<int> remainingContainers = new List<int>(containers);
            remainingContainers.RemoveRange(0, i + 1);
            List<List<int>>? result = Recurse(volume - selectedContainer, remainingContainers, depth + 1);
            if (result is not null)
            {
                if (result.Count == 0)
                    overallResult.Add(new List<int> { selectedContainer });
                else
                {
                    foreach (List<int> combination in result)
                    {
                        combination.Insert(0, selectedContainer);
                        //Console.WriteLine(depth + " " + string.Join(", ", combination));
                        overallResult.Add(combination);
                    }
                }
            }
        }
        if (overallResult.Count == 0)
            return null;
        else
            return overallResult;
    }
}

void P1_2()
{
    List<List<int>>? result = Recurse(150, containers);

    if (result is not null)
    {
        int minNum = int.MaxValue;
        foreach (List<int> temp in result)
        {
            if (temp.Count < minNum)
                minNum = temp.Count;
        }
        List<List<int>> minNumCombinations = new List<List<int>>();
        foreach (List<int> temp in result)
        {
            if (temp.Count == minNum)
                minNumCombinations.Add(temp);
        }

        Console.WriteLine(result.Count);
        Console.WriteLine(minNumCombinations.Count);
        Console.ReadLine();
    }
}

P1_2();