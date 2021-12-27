using AdventOfCodeUtilities;

UInt16 P1(Dictionary<string, Net> nets)
{
    UInt16 result = nets["a"].GetValue(nets);
    Console.WriteLine(result);
    Console.ReadLine();
    return result;
}

void P2(Dictionary<string, Net> nets, UInt16 a)
{
    foreach (var kVP in nets)
        kVP.Value.ValueCached = false;

    nets["b"].SourceType = Type.Wire;
    nets["b"].Source1 = $"{a}";

    UInt16 result = nets["a"].GetValue(nets);
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

Dictionary<string, Net> nets = new Dictionary<string, Net>();
foreach (string netString in inputList)
{
    Net net = new Net(netString);
    if (net.Name == null)
        throw new Exception();
    nets[net.Name] = net;
}

UInt16 a = P1(nets);
P2(nets, a);

public class Net
{
    public string? Name;
    public Type SourceType;
    public string? Source1;
    public string? Source2;
    public int ShiftOperand;
    public UInt16 LiteralValue;

    public UInt16 CachedValue;
    public bool ValueCached = false;
    public Net(string netString)
    {
        string[] split = netString.Split(' ');
        if (split[1] == "->")
        {
            SourceType = Type.Wire;
            Source1 = split[0];
            Name = split[2];
        }
        else if (split[0] == "NOT")
        {
            // bitwise not
            SourceType = Type.NOT;
            Source1 = split[1];
            Name = split[3];
        }
        else if (split[3] == "->")
        {
            Name = split[4];
            if (split[1] == "AND")
            {
                // bitwise and
                SourceType = Type.AND;
                Source1 = split[0];
                Source2 = split[2];
            }
            if (split[1] == "OR")
            {
                // bitwise or
                SourceType = Type.OR;
                Source1 = split[0];
                Source2 = split[2];
            }
            else if (split[1] == "LSHIFT")
            {
                // bitwise lshift
                SourceType = Type.LSHIFT;
                Source1 = split[0];
                ShiftOperand = int.Parse(split[2]);
            }
            else if (split[1] == "RSHIFT")
            {
                // bitwise rshift
                SourceType = Type.RSHIFT;
                Source1 = split[0];
                ShiftOperand = int.Parse(split[2]);
            }
        }
    }

    public UInt16 GetValue(Dictionary<string, Net> nets)
    {
        if (ValueCached)
            return CachedValue;
        UInt16 result = 0;
        switch (SourceType)
        {
            case Type.Wire:
                {
                    if (Source1 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);

                    result = Source1Value;
                    break;
                }
            case Type.AND:
                {
                    if (Source1 == null || Source2 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);
                    UInt16 Source2Value;
                    if (UInt16.TryParse(Source2, out literalValue))
                        Source2Value = literalValue;
                    else
                        Source2Value = nets[Source2].GetValue(nets);

                    result = (UInt16)(Source1Value & Source2Value);
                    break;
                }
            case Type.OR:
                {
                    if (Source1 == null || Source2 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);
                    UInt16 Source2Value;
                    if (UInt16.TryParse(Source2, out literalValue))
                        Source2Value = literalValue;
                    else
                        Source2Value = nets[Source2].GetValue(nets);

                    result = (UInt16)(Source1Value | Source2Value);
                    break;
                }
            case Type.NOT:
                {
                    if (Source1 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);

                    result = (UInt16)(~Source1Value);
                    break;
                }
            case Type.LSHIFT:
                {
                    if (Source1 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);

                    result = (UInt16)(Source1Value << ShiftOperand);
                    break;
                }
            case Type.RSHIFT:
                {
                    if (Source1 == null)
                        throw new Exception();

                    UInt16 literalValue;
                    UInt16 Source1Value;
                    if (UInt16.TryParse(Source1, out literalValue))
                        Source1Value = literalValue;
                    else
                        Source1Value = nets[Source1].GetValue(nets);

                    result = (UInt16)(Source1Value >> ShiftOperand);
                    break;
                }
        }
        ValueCached = true;
        CachedValue = result;
        return result;
    }
}

public enum Type
{
    Wire,
    AND,
    OR,
    NOT,
    LSHIFT,
    RSHIFT
}