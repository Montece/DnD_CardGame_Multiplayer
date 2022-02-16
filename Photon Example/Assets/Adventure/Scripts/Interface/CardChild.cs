using UnityEngine;
using UnityEngine.UI;

public class CardChild : MonoBehaviour
{
    [SerializeField]
    public Text TitleText;
    [SerializeField]
    private Image IconImage;
    [SerializeField]
    private Text AttackText;
    [SerializeField]
    private Text DefenceText;
    [SerializeField]
    private Text ManaCostText;

    private Card Card;

    public void ShowCardInfo(Card card)
    {
        TitleText.text = card.InfoCard.Title;
        IconImage.sprite = card.InfoCard.Icon;
        AttackText.text = card.BattleCard.Attack.ToString();
        DefenceText.text = card.BattleCard.Defence.ToString();
        ManaCostText.text = card.BattleCard.ManaCost.ToString();
        Card = card;
    }

    public void Delete()
    {
        GameManager.Instance.DeleteCard(Card);
    }
}
