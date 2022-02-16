using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    [SerializeField]
    private GameObject InventoryLayer;
    [SerializeField]
    private Text NicknameText;
    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private Text ManaText;
    [SerializeField]
    private Transform InventoryItemGrid;
    [SerializeField]
    private Transform InventoryItemPrefab;
    [SerializeField]
    private Transform InventoryCardGrid;
    [SerializeField]
    private Transform InventoryCardPrefab;
    [SerializeField]
    private GameObject RewardLayer;
    [SerializeField]
    private Transform RewardGrid;
    [SerializeField]
    private Transform RewardPrefab;

    public static GameInterface Instance;

    private void Awake()
    {
        Instance = this;

        //—крываем все окна если в editor не закрыты они
        InventoryLayer.SetActive(false);
        RewardLayer.SetActive(false);
    }

    public void HideReward()
    {
        RewardLayer.SetActive(false);
        foreach (Transform child in RewardGrid) Destroy(child.gameObject);
    }

    public void ShowReward(List<Item> items)
    {
        HideReward();
        RewardLayer.SetActive(true);
        for (int i = 0; i < items.Count; i++) Instantiate(RewardPrefab, RewardGrid, false).GetComponent<RewardChild>().ShowRewardInfo(items[i]);
    }

    public void HideInventory()
    {
        InventoryLayer.SetActive(false);
        foreach (Transform child in InventoryCardGrid) Destroy(child.gameObject);
        foreach (Transform child in InventoryItemGrid) Destroy(child.gameObject);
    }

    public void ShowInventory()
    {
        HideInventory();
        InventoryLayer.SetActive(true);
        List<Item> Items = GameManager.Instance.GetPlayer().Inventory.Items;
        List<Card> Cards = GameManager.Instance.GetPlayer().Inventory.Cards;
        for (int i = 0; i < Items.Count; i++) Instantiate(InventoryItemPrefab, InventoryItemGrid, false).GetComponent<ItemChild>().ShowItemInfo(Items[i]);
        for (int i = 0; i < Cards.Count; i++) Instantiate(InventoryCardPrefab, InventoryCardGrid, false).GetComponent<CardChild>().ShowCardInfo(Cards[i]);

        NicknameText.text = GameManager.Instance.GetPlayer().Nickname;
        HealthText.text = GameManager.Instance.GetPlayer().HealthCurrent + "/" + GameManager.Instance.GetPlayer().HealthMaximum;
        ManaText.text = GameManager.Instance.GetPlayer().ManaMaximum.ToString();
    }

    public void ToggleInventory()
    {
        if (InventoryLayer.activeSelf) HideInventory();
        else ShowInventory();
    }
}
