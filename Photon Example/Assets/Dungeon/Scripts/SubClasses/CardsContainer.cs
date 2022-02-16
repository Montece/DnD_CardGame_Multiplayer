using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardsContainer
{
    [SerializeField]
    public List<Card> Cards;

    public CardsContainer()
    {
        Cards = new List<Card>();
    }
}