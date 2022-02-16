using UnityEngine;
using UnityEngine.UI;

public class CardDetails : MonoBehaviour
{
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Image IconImage;
    [SerializeField]
    private Text DescriptionText;
    [SerializeField]
    private Text AttackText;
    [SerializeField]
    private Text DefenceText;
    [SerializeField]
    private Text ManaCostText;

    public static CardDetails Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCardDetails(Card card)
    {
        TitleText.text = card.InfoCard.Title;
        IconImage.sprite = card.InfoCard.Icon;
        AttackText.text = card.BattleCard.Attack.ToString();
        DefenceText.text = card.BattleCard.Defence.ToString();
        ManaCostText.text = card.BattleCard.ManaCost.ToString();
        DescriptionText.text = "";
        if (Helper.IsType<GuardianBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Guardian";
        if (Helper.IsType<DashBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Dash";
        if (Helper.IsType<AddCardBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Spawn cards";
        if (Helper.IsType<MyStatsIncreaseAllBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Increase stats";
        if (Helper.IsType<EnemyStatsDecreaseAllBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Decrease stats";
        if (Helper.IsType<AddManaBuff>(card.BattleCard.DefaultBuff)) DescriptionText.text = "Add mana";
    }
}
