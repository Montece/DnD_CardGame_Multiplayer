using UnityEngine;
using UnityEngine.UI;

public class RewardChild : MonoBehaviour
{
    [SerializeField]
    public Text TitleText;
    [SerializeField]
    private Image IconImage;
    [SerializeField]
    private Text DescriptionText;

    public void ShowRewardInfo(Item item)
    {
        TitleText.text = item.InfoItem.Title;
        IconImage.sprite = item.InfoItem.Icon;
        DescriptionText.text = item.InfoItem.Description;
    }
}
