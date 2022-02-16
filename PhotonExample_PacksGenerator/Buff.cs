using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Buff { }

/* ---=== BUFFS ===---*/

[Serializable]
public class GuardianBuff : Buff { }

[Serializable]
public class DashBuff : Buff { }

[Serializable]
public class AddCardBuff : Buff
{
    public string CardID;
    public string CardPackID;

    public AddCardBuff() { }

    public AddCardBuff(string cardID, string cardPackID)
    {
        CardID = cardID;
        CardPackID = cardPackID;
    }
}

[Serializable]
public class MyStatsIncreaseAllBuff : Buff
{
    public int Attack;
    public int Defence;

    public MyStatsIncreaseAllBuff() { }

    public MyStatsIncreaseAllBuff(int attack, int defence)
    {
        Attack = attack;
        Defence = defence;
    }
}

[Serializable]
public class EnemyStatsDecreaseAllBuff : Buff
{
    public int Attack;
    public int Defence;

    public EnemyStatsDecreaseAllBuff() { }

    public EnemyStatsDecreaseAllBuff(int attack, int defence)
    {
        Attack = attack;
        Defence = defence;
    }
}

[Serializable]
public class AddManaBuff : Buff
{
    public int Mana;

    public AddManaBuff() { }

    public AddManaBuff(int mana)
    {
        Mana = mana;
    }
}