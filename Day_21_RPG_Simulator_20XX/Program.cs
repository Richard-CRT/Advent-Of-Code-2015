using AdventOfCodeUtilities;
using static System.Net.Mime.MediaTypeNames;

List<string> inputList = AoCUtilities.GetInputLines();
int bossHitpoints = int.Parse(inputList[0].Split(": ")[1]);
int bossDamage = int.Parse(inputList[1].Split(": ")[1]);
int bossArmor = int.Parse(inputList[2].Split(": ")[1]);

List<(int, int, int)> storeWeapons = new List<(int, int, int)>
    {
        ( 8, 4, 0),
        (10, 5, 0),
        (25, 6, 0),
        (40, 7, 0),
        (74, 8, 0),
    };
List<(int, int, int)> storeArmor = new List<(int, int, int)>
    {
        ( 13, 0, 1),
        ( 31, 0, 2),
        ( 53, 0, 3),
        ( 75, 0, 4),
        (102, 0, 5),
    };
List<(int, int, int)> storeRings = new List<(int, int, int)>
    {
        ( 25, 1, 0),
        ( 50, 2, 0),
        (100, 3, 0),
        ( 20, 0, 1),
        ( 40, 0, 2),
        ( 80, 0, 3),
    };

(bool, bool, bool) BuyWeapon(int availableGold, List<(int, int, int)> storeWeapons, List<(int, int, int)> storeArmor, List<(int, int, int)> storeRings, int runningDamage, int runningArmor, bool mustUseAllGold = false)
{
    bool simulationRan = false;
    bool possibleToWin = false;
    bool possibleToLose = false;
    for (int i = 0; i < storeWeapons.Count; i++)
    {
        (int weaponCost, int weaponDamage, int weaponArmor) = storeWeapons[i];
        if (availableGold >= weaponCost)
        {
            List<(int, int, int)> copyStoreWeapons = new List<(int, int, int)>(storeWeapons);
            copyStoreWeapons.RemoveAt(i);
            for (int armourCountToBuy = 0; armourCountToBuy <= 1; armourCountToBuy++)
            {
                (bool sR, bool r1, bool r2) = BuyArmor(availableGold - weaponCost, armourCountToBuy, storeArmor, storeRings, runningDamage + weaponDamage, runningArmor + weaponArmor, mustUseAllGold);
                simulationRan = simulationRan || sR;
                possibleToWin = possibleToWin || r1;
                possibleToLose = possibleToLose || r2;
            }
        }
    }
    return (simulationRan, possibleToWin, possibleToLose);
}

(bool, bool, bool) BuyArmor(int availableGold, int armorsToBuy, List<(int, int, int)> storeArmor, List<(int, int, int)> storeRings, int runningDamage, int runningArmor, bool mustUseAllGold = false)
{
    bool simulationRan = false;
    bool possibleToWin = false;
    bool possibleToLose = false;
    if (armorsToBuy == 0)
    {
        for (int ringCountToBuy = 0; ringCountToBuy <= 2; ringCountToBuy++)
        {
            (bool sR, bool r1, bool r2) = BuyRings(availableGold, ringCountToBuy, storeRings, runningDamage, runningArmor, mustUseAllGold);
            simulationRan = simulationRan || sR;
            possibleToWin = possibleToWin || r1;
            possibleToLose = possibleToLose || r2;
        }
    }
    else
    {
        for (int i = 0; i < storeArmor.Count; i++)
        {
            (int armorCost, int armorDamage, int armorArmor) = storeArmor[i];
            if (availableGold >= armorCost)
            {
                List<(int, int, int)> copyStoreArmor = new List<(int, int, int)>(storeArmor);
                copyStoreArmor.RemoveAt(i);
                (bool sR, bool r1, bool r2) = BuyArmor(availableGold - armorCost, armorsToBuy - 1, copyStoreArmor, storeRings, runningDamage + armorDamage, runningArmor + armorArmor, mustUseAllGold);
                simulationRan = simulationRan || sR;
                possibleToWin = possibleToWin || r1;
                possibleToLose = possibleToLose || r2;
            }
        }
    }
    return (simulationRan, possibleToWin, possibleToLose);
}

(bool, bool, bool) BuyRings(int availableGold, int ringsToBuy, List<(int, int, int)> storeRings, int runningDamage, int runningArmor, bool mustUseAllGold = false)
{
    if (ringsToBuy == 0 && (!mustUseAllGold || availableGold == 0))
    {
        bool result = Simulate(runningDamage, runningArmor);
        return (true, result, !result);
    }
    else
    {
        bool simulationRan = false;
        bool possibleToWin = false;
        bool possibleToLose = false;
        for (int i = 0; i < storeRings.Count; i++)
        {
            (int ringCost, int ringDamage, int ringArmor) = storeRings[i];
            if (availableGold >= ringCost)
            {
                List<(int, int, int)> copyStoreRings = new List<(int, int, int)>(storeRings);
                copyStoreRings.RemoveAt(i);
                (bool sR, bool r1, bool r2) = BuyRings(availableGold - ringCost, ringsToBuy - 1, copyStoreRings, runningDamage + ringDamage, runningArmor + ringArmor, mustUseAllGold);
                simulationRan = simulationRan || sR;
                possibleToWin = possibleToWin || r1;
                possibleToLose = possibleToLose || r2;
            }
        }
        return (simulationRan, possibleToWin, possibleToLose);
    }
}

bool Simulate(int damage, int armor)
{
    //Console.WriteLine($"Simulating: DMG {damage} ARMOR {armor}");
    int bossHealth = bossHitpoints;
    int yourHealth = 100;
    while (bossHealth > 0 && yourHealth > 0)
    {
        bossHealth -= Math.Max(1, damage - bossArmor);
        if (bossHealth <= 0)
            break;
        yourHealth -= Math.Max(1, bossDamage - armor);
    }
    if (bossHealth > 0)
    {
        //Console.WriteLine("Boss wins!");
        return false;
    }
    else
    {
        //Console.WriteLine("You win!");
        return true;
    }
}

void P1()
{
    int availableGold = 0;
    while (true)
    {
        //Console.WriteLine($"Simulating: GOLD {availableGold}");
        (bool simulationRan, bool possibleToWin, bool possibleToLose) = BuyWeapon(availableGold, storeWeapons, storeArmor, storeRings, 0, 0);
        if (simulationRan && possibleToWin)
            break;

        availableGold++;
    }

    Console.WriteLine(availableGold);
    Console.ReadLine();
}

void P2()
{
    int availableGold = 74 + 102 + 100 + 80;
    while (true)
    {
        //Console.WriteLine($"Simulating: GOLD {availableGold}");
        (bool simulationRan, bool possibleToWin, bool possibleToLose) = BuyWeapon(availableGold, storeWeapons, storeArmor, storeRings, 0, 0, true);
        if (simulationRan && possibleToLose)
            break;

        availableGold--;
    }

    Console.WriteLine(availableGold);
    Console.ReadLine();
}

P1();
P2();