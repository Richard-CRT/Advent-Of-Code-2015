using AdventOfCodeUtilities;

void P1(List<string> strings)
{
    int niceStrings = 0;
    foreach (string s in strings)
    {
        int vowelCount = 0;
        bool repeatedC = false;
        bool forbiddenSequence = false;
        char lastC = ' ';
        foreach (char c in s)
        {
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                vowelCount++;
            if (lastC == c)
                repeatedC = true;
            if ((lastC == 'a' && c == 'b') || (lastC == 'c' && c == 'd') || (lastC == 'p' && c == 'q') || (lastC == 'x' && c == 'y'))
            {
                forbiddenSequence = true;
                break;
            }
            lastC = c;
        }
        if (vowelCount >= 3 && repeatedC && !forbiddenSequence)
            niceStrings++;
    }
    Console.WriteLine(niceStrings);
    Console.ReadLine();
}

void P2(List<string> strings)
{
    int niceStrings = 0;
    foreach (string s in strings)
    {
        bool repeats = false;
        bool pair = false;
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (i >= 2 && c == s[i - 2])
                repeats = true;

            if (i < s.Length - 1 && !pair)
            {
                int j = i + 1;

                for (int x = 0; x < s.Length - 1; x++)
                {
                    int y = x + 1;

                    if (y < i || x > j)
                    {
                        if (s[i] == s[x] && s[j] == s[y])
                        {
                            pair = true;
                            break;
                        }
                    }
                }
            }
        }

        if (repeats && pair)
            niceStrings++;
    }
    Console.WriteLine(niceStrings);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList);
P2(inputList);