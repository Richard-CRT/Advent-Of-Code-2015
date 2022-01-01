using AdventOfCodeUtilities;

void P1(Dictionary<string, Dictionary<string, int>> people)
{
    List<string> peopleNames = people.Keys.ToList();
    int result = FindOptimalArrangement(people, peopleNames[0], peopleNames[0], peopleNames.GetRange(1, peopleNames.Count - 1));
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(Dictionary<string, Dictionary<string, int>> people)
{
    List<string> peopleNames = people.Keys.ToList();
    int result = FindOptimalArrangement(people, peopleNames[0], peopleNames[0], peopleNames.GetRange(1, peopleNames.Count - 1));
    Console.WriteLine(result);
    Console.ReadLine();
}

int FindOptimalArrangement(Dictionary<string, Dictionary<string, int>> people, string firstPerson, string lastPersonToSit, List<string> peopleToBeSat, int depth = 0)
{
    /*
    for (int i = 0; i < depth; i++)
        Console.Write("    ");
    Console.WriteLine(lastPersonToSit);
    */

    if (peopleToBeSat.Count == 0)
        return people[lastPersonToSit][firstPerson] + people[firstPerson][lastPersonToSit];

    int maxTotalHappiness = int.MinValue;

    foreach (string personToBeSat in peopleToBeSat)
    {
        int inc = FindOptimalArrangement(people, firstPerson, personToBeSat, peopleToBeSat.Where(x => x != personToBeSat).ToList(), depth + 1);
        maxTotalHappiness = Math.Max(people[lastPersonToSit][personToBeSat] + people[personToBeSat][lastPersonToSit] + inc, maxTotalHappiness);
        /*
        for (int i = 0; i < depth + 1; i++)
            Console.Write("    ");
        Console.WriteLine(maxTotalHappiness);
        */
    }

    return maxTotalHappiness;
}

List<string> inputList = AoCUtilities.GetInputLines();

Dictionary<string, Dictionary<string, int>> people = new Dictionary<string, Dictionary<string, int>>();

foreach (string str in inputList)
{
    string[] split = str.Split(' ');
    string personA = split[0];
    string personB = split[10].Substring(0, split[10].Length - 1);
    int units = int.Parse(split[3]);
    if (split[2] == "lose")
        units = -units;
    if (!people.ContainsKey(personA))
        people[personA] = new Dictionary<string, int>();

    people[personA][personB] = units;
}

P1(people);

people["You"] = new Dictionary<string, int>();
foreach (var kVP in people)
{
    people["You"][kVP.Key] = 0;
    kVP.Value["You"] = 0;
}
P2(people);