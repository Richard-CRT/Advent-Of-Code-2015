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
    for (int j = 0; j < 300e3; j++)
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
                    playerInstructions[i] = Spell.MagicMissile;
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
    Console.WriteLine(minMana);
    Console.ReadLine();
}

void P2()
{
    Dictionary<int, Spell> playerInstructions = new Dictionary<int, Spell>();
    int maxTurnNum = 0;
    int minMana = int.MaxValue;
    for (int j = 0; j < 300e3; j++)
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
            if (!Character.Player.Turn(turnNum, playerInstructions))
            {
                Character.Player.Hitpoints = 0;
                break;
            }
            if (!Character.Player.Alive || !Character.Boss.Alive)
                break;
            if (!Character.Boss.Turn(turnNum))
            {
                Character.Player.Hitpoints = 0;
                break;
            }
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
                minMana = Character.Player.TotalSpentMana;
            }
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
                    playerInstructions[i] = Spell.MagicMissile;
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

    Console.WriteLine(minMana);
    Console.ReadLine();
}

P1();
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

    public bool Turn(int turnNum, Dictionary<int, Spell>? instructions = null)
    {
        if (this == Character.Player)
            AoCUtilities.DebugWriteLine("-- Player turn --");
        else if (this == Character.Boss)
            AoCUtilities.DebugWriteLine("-- Boss turn --");

        AoCUtilities.DebugWriteLine($"   Player has {Character.Player.Hitpoints} hit points, {Character.Player.Armor} armor, {Character.Player.Mana} mana");
        Character.Player.ApplyEffects();
        if (!Character.Player.Alive)
            return true;
        AoCUtilities.DebugWriteLine($"   Boss has {Character.Boss.Hitpoints} hit points, {Character.Boss.Armor} armor, {Character.Boss.Mana} mana");
        Character.Boss.ApplyEffects();
        if (!Character.Boss.Alive)
            return true;
        AoCUtilities.DebugWriteLine($"   Player has {Character.Player.Hitpoints} hit points, {Character.Player.Armor} armor, {Character.Player.Mana} mana");
        AoCUtilities.DebugWriteLine($"   Boss has {Character.Boss.Hitpoints} hit points, {Character.Boss.Armor} armor, {Character.Boss.Mana} mana");


        if (this == Character.Player)
        {
            if (instructions == null)
                throw new NotImplementedException();
            bool r;
            if (instructions.ContainsKey(turnNum))
                r = Cast(instructions[turnNum], Character.Boss);
            else
                r = Cast(Spell.MagicMissile, Character.Boss);
            if (!r)
                return false;
        }
        else
        {
            bool r;
            if (instructions == null || !instructions.ContainsKey(turnNum))
                r = AttackMelee(Character.Player);
            else
                r = Cast(instructions[turnNum], Character.Player);
            if (!r)
                return false;
        }

        AoCUtilities.DebugWriteLine();
        return true;
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

    public bool AttackMelee(Character otherCharacter)
    {
        AoCUtilities.DebugWrite($"   Swings sword: ");
        otherCharacter.ApplyDamage(Damage);
        return true;
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