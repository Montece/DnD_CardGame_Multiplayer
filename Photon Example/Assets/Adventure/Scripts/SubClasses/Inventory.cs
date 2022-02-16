using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    public List<Item> Items;
    [SerializeField]
    public List<Card> Cards;

    public Inventory()
    {
        Items = new List<Item>();
        Cards = new List<Card>();
    }

    public List<Item> GetItems()
    {
        return Items;
    }

    public List<Card> GetCards()
    {
        return Cards;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void AddItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++) AddItem(items[i]);
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void AddCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++) AddCard(cards[i]);
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item)) Items.Remove(item);
    }

    public void RemoveCard(Card card)
    {
        if (Cards.Contains(card)) Cards.Remove(card);
    }

    public void ClearItems()
    {
        Items.Clear();
    }

    public void ClearCards()
    {
        Cards.Clear();
    }
}
