using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleCard
{
    [SerializeField]
    public int Attack;
    [SerializeField]
    public int Defence;
    [SerializeField]
    public int ManaCost;
    [SerializeField]
    public Buff DefaultBuff;
    [SerializeField]
    public bool CanAttack = true;
    [SerializeField]
    public bool CanBeAttacked = true;

    public BattleCard() {}

    public BattleCard(int attack, int defence, int manaCost, Buff defaultBuff)
    {
        Attack = attack;
        Defence = defence;
        ManaCost = manaCost;
        DefaultBuff = defaultBuff;
        CanAttack = true;
        CanBeAttacked = true;
    }

    public BattleCard Clone()
    {
        return MemberwiseClone() as BattleCard;
    }
}