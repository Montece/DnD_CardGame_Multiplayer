using UnityEngine;
using UnityEngine.UI;

public class MyArmCard : MonoBehaviour
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
    private Text ManaCost;

    [SerializeField]
    private Card Card;

    public void Init(Card card)
    {
        Card = card;
        Title.text = card.InfoCard.Title;
        Icon.sprite = card.InfoCard.Icon;
        Attack.text = card.BattleCard.Attack.ToString();
        Defence.text = card.BattleCard.Defence.ToString();
        ManaCost.text = card.BattleCard.ManaCost.ToString();
    }

    public Card GetCard()
    {
        return Card;
    }
}
