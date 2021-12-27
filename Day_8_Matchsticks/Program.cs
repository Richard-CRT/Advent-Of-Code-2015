using AdventOfCodeUtilities;

void P1(List<string> lines)
{
    int charsOfCode = 0;
    int charsOfString = 0;
    foreach (string line in lines)
    {
        charsOfCode += line.Length;
        string unescapedLine = "";
        for (int i = 1; i < line.Length - 1;)
        {
            char c = line[i];

            if (c == '\\' && i < line.Length - 2)
            {
                char cp1 = line[i + 1];
                if (cp1 == '\\')
                {
                    unescapedLine += '\\';
                    i += 2;
                }
                else if (cp1 == '"')
                {
                    unescapedLine += '"';
                    i += 2;
                }
                else if (cp1 == 'x')
                {
                    char newC = (char)int.Parse(line.Substring(i + 2, 2), System.Globalization.NumberStyles.HexNumber);
                    unescapedLine += newC;
                    i += 4;
                }
            }
            else
            {
                unescapedLine += c;
                i++;
            }
        }
        charsOfString += unescapedLine.Length;
    }

    int result = charsOfCode - charsOfString;
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2(List<string> lines)
{
    int charsOfCode = 0;
    int charsOfEscapedCode = 0;
    foreach (string line in lines)
    {
        charsOfCode += line.Length;
        string escapedLine = "\"";
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                escapedLine += "\\\"";
            }
            else if (c == '\\')
            {
                escapedLine += "\\\\";
            }
            else
            {
                escapedLine += c;
            }
        }
        escapedLine += "\"";
        charsOfEscapedCode += escapedLine.Length;
    }

    int result = charsOfEscapedCode - charsOfCode;
    Console.WriteLine(result);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList);
P2(inputList);