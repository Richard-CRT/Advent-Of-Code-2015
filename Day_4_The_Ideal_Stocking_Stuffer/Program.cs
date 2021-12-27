using AdventOfCodeUtilities;
using System.Security.Cryptography;
using System.Text;

string CreateMD5Hash(string input)
{
    // Step 1, calculate MD5 hash from input
    MD5 md5 = System.Security.Cryptography.MD5.Create();
    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    byte[] hashBytes = md5.ComputeHash(inputBytes);

    // Step 2, convert byte array to hex string
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < hashBytes.Length; i++)
    {
        sb.Append(hashBytes[i].ToString("X2"));
    }
    return sb.ToString();
}

void P1(string key)
{
    int num = 0;
    string hash = "11111";
    while (hash[0] != '0' || hash[1] != '0' || hash[2] != '0' || hash[3] != '0' || hash[4] != '0')
    {
        hash = CreateMD5Hash($"{key}{num}");
        num++;
    }
    Console.WriteLine(num - 1);
    Console.ReadLine();
}

void P2(string key)
{
    int num = 0;
    string hash = "11111";
    while (hash[0] != '0' || hash[1] != '0' || hash[2] != '0' || hash[3] != '0' || hash[4] != '0' || hash[5] != '0')
    {
        hash = CreateMD5Hash($"{key}{num}");
        num++;
    }
    Console.WriteLine(num - 1);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

P1(inputList[0]);
P2(inputList[0]);