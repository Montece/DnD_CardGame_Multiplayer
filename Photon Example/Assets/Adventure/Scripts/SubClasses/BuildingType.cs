using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BuildingType {}

[System.Serializable]
public class BuildingDungeon : BuildingType
{
    [SerializeField]
    public List<Card> EnemyDeck;
}

[System.Serializable]
public class BuildingTreasure : BuildingType
{
    [SerializeField]
    public List<Item> Reward;
}