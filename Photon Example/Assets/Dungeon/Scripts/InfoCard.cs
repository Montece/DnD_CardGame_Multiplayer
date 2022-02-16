using UnityEngine;

public class InfoCard
{
    [SerializeField]
    public string Title;
    [SerializeField]
    public Sprite Icon;
    [SerializeField]
    /*public AudioClip PlaceSound;
    [SerializeField]
    public AudioClip AttackSound;
    [SerializeField]
    public AudioClip DeathSound;
    [SerializeField]*/
    public CardRarity Rare;
    [SerializeField]
    public BattleCard BattleReference; //Эталон карты, которая будет сражаться
}