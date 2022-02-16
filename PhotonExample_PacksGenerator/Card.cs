using Newtonsoft.Json;
using System;

[Serializable]
public class CustomCardsPack
{
    public string PackItem_Title;
    public string PackItem_Description;
    public string PackItem_ID;
    public string ID;
    public CustomInfoCard[] CustomInfoCards;
    [JsonIgnore]
    public string IconFilepath;
}

[Serializable]
public class CustomInfoCard
{
    public string Title;
    public string ID;
    public CardRarity Rare;
    public int Attack;
    public int Defence;
    public int ManaCost;
    public Buff DefaultBuff;
    [JsonIgnore]
    public string IconFilepath;

    public override string ToString()
    {
        return Title;
    }
}

[Serializable]
public enum CardRarity
{
    Bad,
    Normal,
    Good,
    Best
}