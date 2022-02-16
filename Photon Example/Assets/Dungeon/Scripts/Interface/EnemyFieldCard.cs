using UnityEngine;
using UnityEngine.UI;

public class EnemyFieldCard : MonoBehaviour
{
    [SerializeField]
    private Text Title;
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private Text Attack;
    [SerializeField]
    private Text Defence;
    [SerializeField]
    private GameObject GuardianBuff;

    [SerializeField]
    private Card Card;

    public void Init(Card card)
    {
        Card = card;
        Title.text = card.InfoCard.Title;
        Icon.sprite = card.InfoCard.Icon;
        Attack.text = card.BattleCard.Attack.ToString();
        Defence.text = card.BattleCard.Defence.ToString();
        GuardianBuff.SetActive(card.IsBuffedCard<GuardianBuff>());
    }

    public Card GetCard()
    {
        return Card;
    }
}
