using AdventOfCodeUtilities;

List<string> inputList = AoCUtilities.GetInputLines();
int i;
for (i = 0; inputList[i] != ""; i++)
{
    string[] split = inputList[i].Split(" => ");
    string origin = split[0];
    string destination = split[1];
    Formula.SafeMap(origin).PossibleReplacements?.Add(Formula.SafeMap(destination));
    Formula.ReverseMap[destination] = Formula.SafeMap(origin);
}
i++;
Formula InitialFormula = Formula.SafeMap(inputList[i]);

void P1()
{
    List<Formula> possibleFormulae = InitialFormula.GetPossibleFormulas();
    Console.WriteLine(possibleFormulae.Count);
    Console.ReadLine();
}

void P2()
{
    List<Formula> reducedFormulae = new List<Formula>() { InitialFormula };
    int steps = 0;
    while (true)
    {
        List<Formula> newReducedFormulae = new List<Formula>();
        foreach (Formula formula in reducedFormulae)
            newReducedFormulae.AddRange(formula.Reduce());
        if (newReducedFormulae.Count == 0)
            break;
        var temp1 = newReducedFormulae.Distinct();
        List<Formula> temp2 = temp1.OrderBy(x => x.SubFormulae.Count).ToList();
        reducedFormulae = temp2.GetRange(0,Math.Min(100, temp2.Count));
        steps++;
    }
    Console.WriteLine(steps);
    Console.ReadLine();
}

P1();
P2();

public class Formula
{
    public static Dictionary<string, Formula> ReverseMap = new Dictionary<string, Formula>();
    public static Dictionary<string, Formula> Map = new Dictionary<string, Formula>();
    public static Formula SafeMap(string str, Formula? value = null)
    {
        if (!Formula.Map.ContainsKey(str))
        {
            if (value is not null)
                Formula.Map[str] = value;
            else
                Formula.Map[str] = new Formula(str);

        }
        return Formula.Map[str];
    }

    public string Name { get; set; } = "";
    public bool Leaf { get; set; }
    public List<Formula> SubFormulae = new List<Formula>();
    public List<Formula>? PossibleReplacements;

    public Formula(List<Formula> subFormulae)
    {
        this.Leaf = false;
        this.SubFormulae = subFormulae;
    }

    public Formula(string strRepr)
    {
        if (strRepr == "e")
        {
            this.Name = "e";
            this.Leaf = true;
            this.PossibleReplacements = new List<Formula>();
        }
        else
        {
            List<string> newFormulaeStrings = new List<string>();
            int lastCapitalIndex = 0;
            string subStrRepr;
            for (int i = 0; i < strRepr.Length; i++)
            {
                char c = strRepr[i];
                if (i != lastCapitalIndex && c >= 'A' && c <= 'Z')
                {
                    subStrRepr = strRepr.Substring(lastCapitalIndex, i - lastCapitalIndex);
                    newFormulaeStrings.Add(subStrRepr);

                    lastCapitalIndex = i;
                }
            }
            subStrRepr = strRepr.Substring(lastCapitalIndex, strRepr.Length - lastCapitalIndex);
            newFormulaeStrings.Add(subStrRepr);
            if (newFormulaeStrings.Count == 1)
            {
                this.Name = strRepr;
                this.Leaf = true;
                this.PossibleReplacements = new List<Formula>();
            }
            else
            {
                this.SubFormulae = new List<Formula>(newFormulaeStrings.Select(x => SafeMap(x)));
            }
        }
    }

    public List<Formula> GetPossibleFormulas()
    {
        if (this.Leaf)
        {
            return this.PossibleReplacements;
        }
        else
        {
            if (this.PossibleReplacements != null)
                return this.PossibleReplacements;
            List<Formula> possibleReplacements = new List<Formula>();
            HashSet<string> uniqueStringRepresentations = new HashSet<string>();
            for (int i = 0; i < this.SubFormulae.Count; i++)
            {
                List<Formula> replacementParts = this.SubFormulae[i].GetPossibleFormulas();
                for (int j = 0; j < replacementParts.Count; j++)
                {
                    List<Formula> copy = new List<Formula>(this.SubFormulae);
                    copy[i] = replacementParts[j];
                    Formula newFormula = new Formula(copy);
                    string repr = newFormula.ToString();
                    if (!uniqueStringRepresentations.Contains(repr))
                    {
                        uniqueStringRepresentations.Add(repr);
                        possibleReplacements.Add(Formula.SafeMap(repr, newFormula));
                    }
                }
            }
            this.PossibleReplacements = possibleReplacements;
            return this.PossibleReplacements;
        }
    }

    public List<Formula> Reduce()
    {
        List<Formula> result = new List<Formula>();
        string strRepr = this.ToString();
        foreach (string subStrRepr in Formula.ReverseMap.Keys)
        {
            for (int i = 0; i < strRepr.Length - subStrRepr.Length + 1; i++)
            {
                if (strRepr.Substring(i, subStrRepr.Length) == subStrRepr)
                {
                    string strReplacement = Formula.ReverseMap[subStrRepr].ToString();
                    string strReprCopy = strRepr.Substring(0, i) + strReplacement + strRepr.Substring(i + subStrRepr.Length);
                    result.Add(Formula.SafeMap(strReprCopy));
                }
            }
        }
        return result;
    }

    public override string ToString()
    {
        return this.Leaf ? this.Name : string.Join("", this.SubFormulae);
    }
}
