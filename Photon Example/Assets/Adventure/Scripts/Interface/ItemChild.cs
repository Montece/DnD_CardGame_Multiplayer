using UnityEngine;
using UnityEngine.UI;

public class ItemChild : MonoBehaviour
{
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Image IconImage;
    [SerializeField]
    private GameObject UseButton;

    private Item Item;

    public void ShowItemInfo(Item item)
    {
        Item = item;
        TitleText.text = item.InfoItem.Title;
        IconImage.sprite = item.InfoItem.Icon;
        UseButton.SetActive(item.InfoItem.UseItemAction != null);
    }

    public void Use()
    {
        GameManager.Instance.Use(Item);
    }
}
