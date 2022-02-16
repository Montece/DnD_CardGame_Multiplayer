using UnityEngine;

public class InfoItem
{
    [SerializeField]
    public string Title;
    [SerializeField]
    public Sprite Icon;
    [SerializeField]
    public string Description;
    [SerializeField]
    public UseItem UseItemAction;
    [SerializeField]
    public object AdditionInfo;
}

public delegate void UseItem(int playerActorNumber, Item item);