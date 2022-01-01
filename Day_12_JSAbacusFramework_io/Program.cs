using AdventOfCodeUtilities;

void P1(JSONElement rootElement)
{
    int result = rootElement.SumNumbers();
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(JSONElement rootElement)
{
    int result = rootElement.SumNumbers(true);
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();
string jsonString = inputList[0].Trim();
JSONElement rootElement = JSONElement.Factory(jsonString, 0, jsonString.Length);

P1(rootElement);
P2(rootElement);

public abstract class JSONElement
{
    public static JSONElement Factory(string jsonString, int loc, int length)
    {
        //Console.WriteLine(jsonString.Substring(loc, length));
        char startChar = jsonString[loc];
        char endChar = jsonString[loc + length - 1];
        if (startChar == '[' && endChar == ']')
            return new ArrayJSONElement(jsonString, loc, length);
        else if (startChar == '{' && endChar == '}')
            return new ObjectJSONElement(jsonString, loc, length);
        else if (length >= 2 && startChar == '"' && endChar == '"')
            return new StringJSONElement(jsonString, loc, length);
        else
            return new NumberJSONElement(jsonString, loc, length);
    }

    public virtual int SumNumbers(bool P2 = false)
    {
        return 0;
    }

    public virtual bool IsRed()
    {
        return false;
    }
}

public class ArrayJSONElement : JSONElement
{
    public List<JSONElement> Children = new List<JSONElement>();

    public ArrayJSONElement(string jsonString, int loc, int length)
    {
        if (length > 2)
        {
            int depth = 0;
            int lastCommaLoc = loc;
            for (int i = loc + 1; i < loc + length; i++) // remove start [
            {
                if ((jsonString[i] == ',' || jsonString[i] == ']') && depth == 0)
                {
                    JSONElement newChildJSONElement = JSONElement.Factory(jsonString, lastCommaLoc + 1, i - lastCommaLoc - 1);
                    Children.Add(newChildJSONElement);
                    lastCommaLoc = i;
                }
                else if (jsonString[i] == '[' || jsonString[i] == '{')
                    depth++;
                else if (jsonString[i] == ']' || jsonString[i] == '}')
                    depth--;
            }
        }
    }

    public override int SumNumbers(bool P2 = false)
    {
        return Children.Select(x => x.SumNumbers(P2)).Sum();
    }
}

public class ObjectJSONElement : JSONElement
{
    public Dictionary<string, JSONElement> Children = new Dictionary<string, JSONElement>();

    public ObjectJSONElement(string jsonString, int loc, int length)
    {
        if (length > 2)
        {
            int depth = 0;
            int lastCommaLoc = loc;
            int lastColonLoc = -1;
            for (int i = loc + 1; i < loc + length; i++) // remove start [
            {
                if ((jsonString[i] == ',' || jsonString[i] == '}') && depth == 0)
                {
                    string key = jsonString.Substring(lastCommaLoc + 2, lastColonLoc - lastCommaLoc - 3);
                    //Console.WriteLine($"Key: {key}");
                    JSONElement newChildJSONElement = JSONElement.Factory(jsonString, lastColonLoc + 1, i - lastColonLoc - 1);

                    Children[key] = newChildJSONElement;
                    lastCommaLoc = i;
                }
                else if (jsonString[i] == ':' && depth == 0)
                    lastColonLoc = i;
                else if (jsonString[i] == '[' || jsonString[i] == '{')
                    depth++;
                else if (jsonString[i] == ']' || jsonString[i] == '}')
                    depth--;
            }
        }
    }

    public override int SumNumbers(bool P2 = false)
    {
        bool containsRed = P2 && Children.Any(x => x.Value.IsRed());
        return containsRed ? 0 : Children.Select(x => x.Value.SumNumbers(P2)).Sum();
    }
}

public class StringJSONElement : JSONElement
{
    public string String = "";

    public StringJSONElement(string jsonString, int loc, int length)
    {
        if (length >= 2)
            String = jsonString.Substring(loc + 1, length - 2);
    }

    public override bool IsRed()
    {
        return String == "red";
    }
}

public class NumberJSONElement : JSONElement
{
    public int Value = 0;

    public NumberJSONElement(string jsonString, int loc, int length)
    {
        if (!int.TryParse(jsonString.Substring(loc, length), out Value))
            throw new ArgumentException();
    }

    public override int SumNumbers(bool P2 = false)
    {
        return Value;
    }
}