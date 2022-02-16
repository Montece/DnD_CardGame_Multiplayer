using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInterface : MonoBehaviour
{
    [SerializeField]
    public Transform My_FieldTransform; //Игровое поле игрока 
    [SerializeField]
    public Transform Enemy_FieldTransform; //Игровое поле противника
    [SerializeField]
    public Transform My_ArmTransform; //Игровая рука игрока
    [SerializeField]
    public Transform Enemy_ArmTransform; //Игровая рука противника

    [SerializeField]
    private CardDetails CardDetails; //Игровое поле игрока 
    [SerializeField]
    private Button NextTurnButton; //Кнопка передать ход
    [SerializeField]
    private GameObject MyTurnPanel; //Надпись ваш ход
    [SerializeField]
    private Text MyManaText; //Надпись маны
    [SerializeField]
    private Text MyDefenceText; //Надпись моих жизней
    [SerializeField]
    private Text EnemyDefenceText; //Надпись жизней противника
    [SerializeField]
    private Text MyNicknameText; //Надпись мой никнейм
    [SerializeField]
    private Text EnemyNicknameText; //Надпись никнейм противника

    [SerializeField]
    public GameObject MyArmCardPrefab;
    [SerializeField]
    public GameObject EnemyArmCardPrefab;
    [SerializeField]
    public GameObject MyFieldCardPrefab;
    [SerializeField]
    public GameObject EnemyFieldCardPrefab;

    public static DungeonInterface Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
    }

    private void Start()
    {
        HideCardDetails();
    }

    public void ShowCardDetails(Card card)
    {
        CardDetails.gameObject.SetActive(true);
        CardDetails.ShowCardDetails(card);
    }

    public void HideCardDetails()
    {
        CardDetails.gameObject.SetActive(false);
    }

    public void SetNextTurnStatus(bool status)
    {
        NextTurnButton.interactable = status;
    }

    public void ShowMyTurn()
    {
        MyTurnPanel.SetActive(true);
    }

    public void SwitchTurn()
    {
        DungeonManager.Instance.SwitchTurn();
    }

    public void ShowMana(int my_mana_current, int my_mana_local_maximum)
    {
        MyManaText.text = $"{my_mana_current}/{my_mana_local_maximum}";
    }

    public void ShowMyDefence(int my_defence_current, int my_defence_maximum)
    {
        MyDefenceText.text = $"{my_defence_current}/{my_defence_maximum}";
    }

    public void ShowEnemyDefence(int enemy_defence_current, int enemy_defence_maximum)
    {
        EnemyDefenceText.text = $"{enemy_defence_current}/{enemy_defence_maximum}";
    }

    public void ShowMyNickname(string nickname)
    {
        MyNicknameText.text = nickname;
    }

    public void ShowEnemyNickname(string nickname)
    {
        EnemyNicknameText.text = nickname;
    }
}
