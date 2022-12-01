using AdventOfCodeUtilities;
using System.Diagnostics;

List<string> inputList = AoCUtilities.GetInputLines();
int bossHitpoints = int.Parse(inputList[0].Split(": ")[1]);
int bossDamage = int.Parse(inputList[1].Split(": ")[1]);

void P1()
{
    Dictionary<int, Spell> playerInstructions = new Dictionary<int, Spell>();
    int maxTurnNum = 0;
    int minMana = int.MaxValue;
    while (true)
    {
        AoCUtilities.DebugWriteLine("Player Instructions: ");
        foreach (KeyValuePair<int, Spell> kVP in playerInstructions)
        {
            AoCUtilities.DebugWriteLine($"   {kVP.Key}: {kVP.Value}");
        }
        AoCUtilities.DebugWriteLine();
        AoCUtilities.DebugReadLine();

        Character.Boss = new Character(-1, bossHitpoints, bossDamage, 0);
        Character.Player = new Character(-1, 50, 0, 500);
        int turnNum = 0;
        while (true)
        {
            Character.Player.Turn(turnNum, playerInstructions);
            if (!Character.Player.Alive || !Character.Boss.Alive)
                break;
            Character.Boss.Turn(turnNum);
            if (!Character.Player.Alive || !Character.Boss.Alive)
                break;

            turnNum++;
        }
        if (turnNum > maxTurnNum)
            maxTurnNum = turnNum;

        bool playerWins = Character.Player.Alive;
        if (playerWins)
        {
            /*
            Console.WriteLine("Player Wins");

            Console.WriteLine("Player Instructions: ");
            foreach (KeyValuePair<int, Spell> kVP in playerInstructions)
            {
                Console.WriteLine($"   {kVP.Key}: {kVP.Value}");
            }
            Console.WriteLine();

            Console.WriteLine(Character.Player.TotalSpentMana);
            */
            if (Character.Player.TotalSpentMana < minMana)
            {
                minMana = Character.Player.TotalSpentMana;
                Console.WriteLine(minMana);
            }

            //Console.ReadLine();
        }
        else
            AoCUtilities.DebugWriteLine("Boss Wins");

        int i = 0;
        bool notGoingToWin = false;
        while (true)
        {
            if (i > maxTurnNum)
            {
                notGoingToWin = true;
                break;
            }
            if (playerInstructions.ContainsKey(i))
            {
                if (playerInstructions[i] == Spell.Recharge)
                {
                    playerInstructions.Remove(i);
                    i++;
                }
                else
                {
                    playerInstructions[i] = (Spell)((int)playerInstructions[i] + 1);
                    break;
                }
            }
            else
            {
                playerInstructions[i] = Spell.MagicMissile;
                break;
            }
        }
        if (notGoingToWin)
            break;
    }
}

void P2()
{
    //Dictionary<int, Spell> playerInstructions = new Dictionary<int, Spell>();
    Dictionary<int, Spell> playerInstructions = new Dictionary<int, Spell> { { 0, Spell.Poison }, { 1, Spell.Recharge }, { 2, Spell.Shield }, { 3, Spell.Poison }, { 5, Spell.Shield }, { 6, Spell.Poison } };
    int maxTurnNum = 0;
    int minMana = int.MaxValue;
    while (true)
    {
        AoCUtilities.DebugWriteLine("Player Instructions: ");
        foreach (KeyValuePair<int, Spell> kVP in playerInstructions)
        {
            AoCUtilities.DebugWriteLine($"   {kVP.Key}: {kVP.Value}");
        }
        AoCUtilities.DebugWriteLine();
        AoCUtilities.DebugReadLine();

        Character.Boss = new Character(-1, bossHitpoints, bossDamage, 0);
        Character.Player = new Character(-1, 50, 0, 500);
        int turnNum = 0;
        while (true)
        {
            Character.Player.Hitpoints -= 1;
            if (!Character.Player.Alive)
                break;
            Character.Player.Turn(turnNum, playerInstructions);
            if (!Character.Player.Alive || !Character.Boss.Alive)
                break;
            Character.Boss.Turn(turnNum);
            if (!Character.Player.Alive || !Character.Boss.Alive)
                break;

            turnNum++;
        }
        if (turnNum > maxTurnNum)
            maxTurnNum = turnNum;

        bool playerWins = Character.Player.Alive;
        if (playerWins)
        {
            if (Character.Player.TotalSpentMana < minMana)
            {
                Console.WriteLine("Player Wins");

                Console.WriteLine("Player Instructions: ");
                foreach (KeyValuePair<int, Spell> kVP in playerInstructions)
                {
                    Console.WriteLine($"   {kVP.Key}: {kVP.Value}");
                }
                Console.WriteLine();

                Console.WriteLine(Character.Player.TotalSpentMana);
                minMana = Character.Player.TotalSpentMana;
            }

            //Console.ReadLine();
        }
        else
            AoCUtilities.DebugWriteLine("Boss Wins");

        int i = 0;
        bool notGoingToWin = false;
        while (true)
        {
            if (playerInstructions.ContainsKey(i))
            {
                if (playerInstructions[i] == Spell.Recharge)
                {
                    playerInstructions.Remove(i);
                    i++;
                }
                else
                {
                    playerInstructions[i] = (Spell)((int)playerInstructions[i] + 1);
                    break;
                }
            }
            else
            {
                playerInstructions[i] = Spell.MagicMissile;
                break;
            }
        }
        if (notGoingToWin)
            break;
    }
}

//P1();
P2();

public enum Spell
{
    MagicMissile = 0,
    Drain = 1,
    Shield = 2,
    Poison = 3,
    Recharge = 4
}

public class Character
{
    public static Character Player { get; set; } = new Character();
    public static Character Boss { get; set; } = new Character();

    public bool Alive
    {
        get
        {
            return Hitpoints > 0;
        }
    }
    public int Hitpoints { get; set; }
    public int Damage { get; set; }
    public int Armor
    {
        get
        {
            return Shield != null ? 7 : 0;
        }
    }
    public int Mana { get; set; }
    public int MaxTotalSpentMana { get; set; } = 0;
    public int TotalSpentMana { get; set; } = 0;
    public int? Poison { get; set; }
    public int? Shield { get; set; }
    public int? Recharge { get; set; }

    public Character()
    {
        this.MaxTotalSpentMana = -1;
        this.Hitpoints = 0;
        this.Damage = 0;
        this.Mana = 0;
    }

    public Character(int maxAllowedTotalMana, int hitpoints, int damage, int mana)
    {
        this.MaxTotalSpentMana = maxAllowedTotalMana;
        this.Hitpoints = hitpoints;
        this.Damage = damage;
        this.Mana = mana;
    }

    public void ApplyDamage(int damage)
    {
        int damageToApply = Math.Max(1, damage - this.Armor);
        AoCUtilities.DebugWriteLine($"hits for {damageToApply} damage.");
        this.Hitpoints -= damageToApply;
        if (!this.Alive)
        {
            AoCUtilities.DebugWriteLine("   Died!");
        }
    }

    public void Turn(int turnNum, Dictionary<int, Spell>? instructions = null)
    {
        if (this == Character.Player)
            AoCUtilities.DebugWriteLine("-- Player turn --");
        else if (this == Character.Boss)
            AoCUtilities.DebugWriteLine("-- Boss turn --");

        AoCUtilities.DebugWriteLine($"   Player has {Character.Player.Hitpoints} hit points, {Character.Player.Armor} armor, {Character.Player.Mana} mana");
        Character.Player.ApplyEffects();
        if (!Character.Player.Alive)
            return;
        AoCUtilities.DebugWriteLine($"   Boss has {Character.Boss.Hitpoints} hit points, {Character.Boss.Armor} armor, {Character.Boss.Mana} mana");
        Character.Boss.ApplyEffects();
        if (!Character.Boss.Alive)
            return;
        AoCUtilities.DebugWriteLine($"   Player has {Character.Player.Hitpoints} hit points, {Character.Player.Armor} armor, {Character.Player.Mana} mana");
        AoCUtilities.DebugWriteLine($"   Boss has {Character.Boss.Hitpoints} hit points, {Character.Boss.Armor} armor, {Character.Boss.Mana} mana");


        if (this == Character.Player)
        {
            if (instructions == null)
                throw new NotImplementedException();
            if (instructions.ContainsKey(turnNum))
                Cast(instructions[turnNum], Character.Boss);
        }
        else
        {
            if (instructions == null || !instructions.ContainsKey(turnNum))
                AttackMelee(Character.Player);
            else
                Cast(instructions[turnNum], Character.Player);

        }

        AoCUtilities.DebugWriteLine();
    }

    public void ApplyEffects()
    {
        AoCUtilities.DebugWriteLine("      Effects: ");
        if (Poison != null)
        {
            Poison = Poison > 1 ? Poison - 1 : null;
            AoCUtilities.DebugWrite($"         Poison deals damage: ");
            this.ApplyDamage(3);
        }
        if (Shield != null)
        {
            Shield = Shield > 1 ? Shield - 1 : null;
            AoCUtilities.DebugWriteLine($"         Shield is active");
        }
        if (Recharge != null)
        {
            Mana += 101;
            Recharge = Recharge > 1 ? Recharge - 1 : null;
            AoCUtilities.DebugWriteLine($"         Recharge provides 101 mana");
        }
    }

    public void AttackMelee(Character otherCharacter)
    {
        AoCUtilities.DebugWrite($"   Swings sword: ");
        otherCharacter.ApplyDamage(Damage);
    }

    public bool SpendMana(int mana)
    {
        if (Mana >= mana && (MaxTotalSpentMana == -1 || TotalSpentMana + mana <= MaxTotalSpentMana))
        {
            TotalSpentMana += mana;
            Mana -= mana;
            return true;
        }
        else
            return false;
    }

    public bool Cast(Spell spell, Character? otherCharacter = null)
    {
        switch (spell)
        {
            case Spell.MagicMissile:
                if (otherCharacter != null)
                {
                    if (SpendMana(53))
                    {
                        AoCUtilities.DebugWrite("   Casts Magic Missile: ");
                        otherCharacter.ApplyDamage(4);
                        return true;
                    }
                }
                break;
            case Spell.Drain:
                if (otherCharacter != null)
                {
                    if (SpendMana(73))
                    {
                        AoCUtilities.DebugWrite("   Casts Drain: ");
                        otherCharacter.ApplyDamage(2);
                        this.Hitpoints += 2;
                        return true;
                    }
                }
                break;
            case Spell.Shield:
                if (this.Shield == null && SpendMana(113))
                {
                    AoCUtilities.DebugWriteLine("   Casts Shield.");
                    Shield = 6;
                    return true;
                }
                break;
            case Spell.Poison:
                if (otherCharacter != null)
                {
                    if (otherCharacter.Poison == null && SpendMana(173))
                    {
                        AoCUtilities.DebugWriteLine("   Casts Poison.");
                        otherCharacter.Poison = 6;
                        return true;
                    }
                }
                break;
            case Spell.Recharge:
                if (this.Recharge == null && SpendMana(229))
                {
                    AoCUtilities.DebugWriteLine("   Casts Recharge.");
                    Recharge = 5;
                    return true;
                }
                break;
        }
        return false;
    }
}