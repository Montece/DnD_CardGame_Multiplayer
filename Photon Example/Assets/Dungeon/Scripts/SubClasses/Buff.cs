using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Buff {}

/* ---=== BUFFS ===---*/

[System.Serializable]
public class GuardianBuff : Buff {}

[System.Serializable]
public class DashBuff : Buff {}

[System.Serializable]
public class AddCardBuff : Buff
{
    [SerializeField]
    public string CardID;
    [SerializeField]
    public string CardPackID;

    public AddCardBuff() { }

    public AddCardBuff(string cardID, string cardPackID)
    {
        CardID = cardID;
        CardPackID = cardPackID;
    }
}

[System.Serializable]
public class MyStatsIncreaseAllBuff : Buff
{
    [SerializeField]
    public int Attack;
    [SerializeField]
    public int Defence;

    public MyStatsIncreaseAllBuff() {}

    public MyStatsIncreaseAllBuff(int attack, int defence)
    {
        Attack = attack;
        Defence = defence;
    }
}

[System.Serializable]
public class EnemyStatsDecreaseAllBuff : Buff
{
    [SerializeField]
    public int Attack;
    [SerializeField]
    public int Defence;

    public EnemyStatsDecreaseAllBuff() {}

    public EnemyStatsDecreaseAllBuff(int attack, int defence)
    {
        Attack = attack;
        Defence = defence;
    }
}

[System.Serializable]
public class AddManaBuff : Buff
{
    [SerializeField]
    public int Mana;

    public AddManaBuff() {}

    public AddManaBuff(int mana)
    {
        Mana = mana;
    }
}