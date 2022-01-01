using AdventOfCodeUtilities;

void PrintPassword(char[] password)
{
    foreach (char c in password)
        Console.Write(c);
    Console.WriteLine();
}

#pragma warning disable CS8321 // Local function is declared but never used
bool CheckValid(char[] password)
#pragma warning restore CS8321 // Local function is declared but never used
{
    HashSet<char> existingPairs = new HashSet<char>();
    bool increasingStraight = false;
    for (int i = 0; i < password.Length - 1 && existingPairs.Count < 2; i++)
    {
        char c = password[i];

        if (c == password[i + 1] && !existingPairs.Contains(c))
        {
            existingPairs.Add(c);
        }

        // increasing straight
        if (i < password.Length - 2)
        {
            if (password[i + 1] == c + 1 && password[i + 2] == c + 2)
            {
                increasingStraight = true;
            }
        }
    }
    bool result = existingPairs.Count >= 2 && increasingStraight;
    return result;
}

Int64 GetPasswordScore(char[] password)
{
    Int64 value = 0;
    int column = 0;
    for (int i = password.Length - 1; i >= 0; i--)
    {
        value += (int)(password[i] - 'a') * (Int64)Math.Pow(26, column);
        column++;
    }
    return value;
}

Dictionary<Int64,char[]> GeneratePasswords(int length = 8)
{
    Dictionary<Int64, char[]> passwords = new Dictionary<Int64, char[]>();
    for (int straightLoc = 0; straightLoc < length - 2; straightLoc++)
    {
        for (char straightStartChar = 'a'; straightStartChar <= 'x'; straightStartChar++)
        {
            char straightEndChar = (char)(straightStartChar + 2);

            if ((straightEndChar < 'i' || straightStartChar > 'i')
                && (straightEndChar < 'l' || straightStartChar > 'l')
                && (straightEndChar < 'o' || straightStartChar > 'o'))
            {


                for (char pair1c = 'a'; pair1c <= 'z'; pair1c++)
                {
                    if (pair1c != 'i' && pair1c != 'l' && pair1c != 'o')
                    {
                        for (char pair2c = (char)(pair1c + 1); pair2c <= 'z'; pair2c++)
                        {
                            if (pair2c != 'i' && pair2c != 'l' && pair2c != 'o')
                            {
                                if (pair1c != pair2c)
                                {
                                    for (int pair1Loc = 0; pair1Loc < length - 1; pair1Loc++)
                                    {
                                        if ((pair1Loc < straightLoc - 1 || (pair1Loc == straightLoc - 1 && pair1c == straightStartChar)) ||
                                            pair1Loc > straightLoc + 2 || (pair1Loc == straightLoc + 2 && pair1c == straightEndChar))
                                        {
                                            for (int pair2Loc = 0; pair2Loc < length - 1; pair2Loc++)
                                            {
                                                if ((pair2Loc < straightLoc - 1 || (pair2Loc == straightLoc - 1 && pair2c == straightStartChar)) ||
                                                    pair2Loc > straightLoc + 2 || (pair2Loc == straightLoc + 2 && pair2c == straightEndChar))
                                                {
                                                    if (pair2Loc > pair1Loc + 1 || pair2Loc < pair1Loc - 1)
                                                    {
                                                        // fill remaining characters
                                                        char[] password = new char[length];
                                                        for (int i = 0; i < length; i++)
                                                            password[i] = '_';

                                                        password[straightLoc] = straightStartChar;
                                                        password[straightLoc + 1] = (char)(straightStartChar + 1);
                                                        password[straightLoc + 2] = straightEndChar;

                                                        password[pair1Loc] = pair1c;
                                                        password[pair1Loc + 1] = pair1c;

                                                        password[pair2Loc] = pair2c;
                                                        password[pair2Loc + 1] = pair2c;

                                                        //PrintPassword(password);

                                                        List<int> indexesStillToBeFilled = new List<int>();
                                                        for (int i = 0; i < length; i++)
                                                        {
                                                            if (password[i] == '_')
                                                            {
                                                                indexesStillToBeFilled.Add(i);
                                                                password[i] = 'a';
                                                            }
                                                        }

                                                        bool done = false;
                                                        while (!done)
                                                        {
                                                            //if (straightLoc == 1)
                                                            //    PrintPassword(password);
                                                            Int64 score = GetPasswordScore(password);
                                                            if (!passwords.ContainsKey(score))
                                                            {
                                                                char[] clone = new char[length];
                                                                password.CopyTo(clone, 0);
                                                                passwords[score] = clone;
                                                            }

                                                            int lastIndex = indexesStillToBeFilled.Count - 1;

                                                            password[indexesStillToBeFilled[lastIndex]]++;
                                                            if (password[indexesStillToBeFilled[lastIndex]] == 'i' || password[indexesStillToBeFilled[lastIndex]] == 'j' || password[indexesStillToBeFilled[lastIndex]] == 'l')
                                                                password[indexesStillToBeFilled[lastIndex]]++;

                                                            int i = lastIndex;
                                                            while (password[indexesStillToBeFilled[i]] == 'z' + 1)
                                                            {
                                                                password[indexesStillToBeFilled[i]] = 'a';
                                                                i--;
                                                                if (i > -1)
                                                                    password[indexesStillToBeFilled[i]]++;
                                                                else
                                                                    break;
                                                            }
                                                            if (i == -1)
                                                                done = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return passwords;
}

void P12(char[] password)
{
    Dictionary<Int64, char[]> passwords = GeneratePasswords();

    KeyValuePair<Int64,char[]> newPasswordKVP = new KeyValuePair<long, char[]>(Int64.MaxValue,new char[0]);
    Int64 currentPasswordScore = GetPasswordScore(password);
    foreach (var kVP in passwords)
    {
        if (kVP.Key > currentPasswordScore && kVP.Key < newPasswordKVP.Key)
        {
            newPasswordKVP = kVP;
        }
    }
    password = newPasswordKVP.Value;

    PrintPassword(password);
    Console.ReadLine();

    newPasswordKVP = new KeyValuePair<long, char[]>(Int64.MaxValue, new char[0]);
    currentPasswordScore = GetPasswordScore(password);
    foreach (var kVP in passwords)
    {
        if (kVP.Key > currentPasswordScore && kVP.Key < newPasswordKVP.Key)
        {
            newPasswordKVP = kVP;
        }
    }
    password = newPasswordKVP.Value;

    PrintPassword(password);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

char[] password = inputList[0].ToCharArray();

P12(password);