using System;
using UnityEngine;
using UnityEngine.UI;

public class MyFieldCard : MonoBehaviour
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
    public bool CanAttack;
    [SerializeField]
    private GameObject GuardianBuff;

    [SerializeField]
    private Card Card;
    [SerializeField]
    private Outline Outline;

    public void Init(Card card, bool isMyTurn)
    {
        Card = card;
        Title.text = card.InfoCard.Title;
        Icon.sprite = card.InfoCard.Icon;
        Attack.text = card.BattleCard.Attack.ToString();
        Defence.text = card.BattleCard.Defence.ToString();
        GuardianBuff.SetActive(card.IsBuffedCard<GuardianBuff>());
        CanAttack = card.BattleCard.CanAttack;

        if (!isMyTurn) Lock();
        else if (!CanAttack) Lock();
        else Unlock();
    }

    public Card GetCard()
    {
        return Card;
    }

    private void Unlock()
    {
        Outline.effectColor = Color.green;
    }

    private void Lock()
    {
        Outline.effectColor = Color.red;
    }
}
