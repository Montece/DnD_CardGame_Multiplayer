using Newtonsoft.Json;
using UnityEngine;

/// <summary> Каждый предмет уникален. Имеет ссылку на общее его описание в виде ID и InfoItem </summary>
[System.Serializable]
public class Item
{
    [SerializeField]
    public string Guid; //Уникальный номер, делает предмет уникальным
    [SerializeField]
    public string ID; //Нужен для получения информации о предмете и его функицонале

    [JsonIgnore]
    public InfoItem InfoItem
    {
        get
        {
            if (infoItem == null) LoadItemInfo();
            return infoItem;
        }
        private set
        {
            infoItem = value;
        }
    }

    [JsonIgnore]
    private InfoItem infoItem;

    private Item() {}

    private void LoadItemInfo()
    {
        infoItem = GameResources.Instance.GetItemInfo(ID);
    }

    public static Item CreateItem(string id)
    {
        Item item = new Item()
        {
            Guid = System.Guid.NewGuid().ToString(),
            ID = id
        };

        return item;
    }

    public override bool Equals(object item)
    {
        if (item == null) return false;
        if (Helper.IsType<Item>(item)) return ((Item)item).Guid == Guid;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
