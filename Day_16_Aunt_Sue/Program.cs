using AdventOfCodeUtilities;

List<string> inputList = AoCUtilities.GetInputLines();
List<Sue> AllSues = inputList.Select(x => new Sue(x)).ToList();
Sue TargetSue = new Sue(3, 7, 2, 3, 0, 0, 5, 3, 2, 1);

void P1()
{
    List<Sue> candidateSues = new List<Sue>(AllSues);

    for (int i = candidateSues.Count - 1; i >= 0; i--)
    {
        Sue candidateSue = candidateSues[i];
        bool match = true;
        foreach (KeyValuePair<string,int> kVP in candidateSue.Properties)
        {
            string name = kVP.Key;
            int count = kVP.Value;
            if (candidateSue.Properties.ContainsKey(name))
            {
                if (candidateSue.Properties[name] != TargetSue.Properties[name])
                {
                    match = false;
                    break;
                }
            }
        }
        if (!match)
            candidateSues.RemoveAt(i);
    }

    Console.WriteLine(candidateSues[0].ID);
    Console.ReadLine();
}

void P2()
{
    List<Sue> candidateSues = new List<Sue>(AllSues);

    for (int i = candidateSues.Count - 1; i >= 0; i--)
    {
        Sue candidateSue = candidateSues[i];
        bool match = true;
        foreach (KeyValuePair<string, int> kVP in candidateSue.Properties)
        {
            string name = kVP.Key;
            int count = kVP.Value;
            if (candidateSue.Properties.ContainsKey(name))
            {
                switch (name)
                {
                    case "cats":
                    case "trees":
                        if (candidateSue.Properties[name] <= TargetSue.Properties[name])
                            match = false;
                        break;
                    case "pomeranians":
                    case "goldfish":
                        if (candidateSue.Properties[name] >= TargetSue.Properties[name])
                            match = false;
                        break;
                    default:
                        if (candidateSue.Properties[name] != TargetSue.Properties[name])
                            match = false;
                        break;
                }
                if (!match)
                    break;
            }
        }
        if (!match)
            candidateSues.RemoveAt(i);
    }

    Console.WriteLine(candidateSues[0].ID);
    Console.ReadLine();
}

P1();
P2();

public class Sue
{
    public int ID { get; set; }

    public Dictionary<string, int> Properties { get; set; } = new Dictionary<string, int>();

    public Sue(int children, int cats, int samoyeds, int pomeranians, int akitas, int vizslas, int goldfish, int trees, int cars, int perfumes)
    {
        ID = -1;
        Properties["children"] = children;
        Properties["cats"] = cats;
        Properties["samoyeds"] = samoyeds;
        Properties["pomeranians"] = pomeranians;
        Properties["akitas"] = akitas;
        Properties["vizslas"] = vizslas;
        Properties["goldfish"] = goldfish;
        Properties["trees"] = trees;
        Properties["cars"] = cars;
        Properties["perfumes"] = perfumes;
    }

    public Sue(string sueString)
    {
        string startRegion = sueString.Split(':')[0];
        this.ID = int.Parse(startRegion.Split(' ')[1]);
        string[] temp = sueString.Substring(startRegion.Length + 1).Split(',');
        foreach (string str in temp)
        {
            string[] split = str.Trim().Split(':');
            string name = split[0].Trim();
            int count = int.Parse(split[1].Trim());
            Properties[name] = count;
        }
    }
}