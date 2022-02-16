using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private bool IsMyTurn;

    private PhotonView PhotonView;

    public static DungeonManager Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
    }

    public void Initialize()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    public void StartDungeon(int playerActorNumberAttacker, int playerActorNumberDefender)
    {
        PhotonView.RPC("RpcStartDungeon", RpcTarget.MasterClient, playerActorNumberAttacker, playerActorNumberDefender);
    }

    public void ShowMyArm(List<Card> cards)
    {
        foreach (Transform child in DungeonInterface.Instance.My_ArmTransform) Destroy(child.gameObject);

        for (int i = 0; i < cards.Count; i++)
        {
            Transform card = Instantiate(DungeonInterface.Instance.MyArmCardPrefab, DungeonInterface.Instance.My_ArmTransform, false).transform;
            card.GetComponent<MyArmCard>().Init(cards[i]);
        }
    }

    public void ShowEnemyArm(int count)
    {
        foreach (Transform child in DungeonInterface.Instance.Enemy_ArmTransform) Destroy(child.gameObject);

        for (int i = 0; i < count; i++)
        {
            Transform card = Instantiate(DungeonInterface.Instance.EnemyArmCardPrefab, DungeonInterface.Instance.Enemy_ArmTransform, false).transform;
        }
    }

    public void ShowMyField(List<Card> cards)
    {
        foreach (Transform child in DungeonInterface.Instance.My_FieldTransform) Destroy(child.gameObject);

        for (int i = 0; i < cards.Count; i++)
        {
            Transform card = Instantiate(DungeonInterface.Instance.MyFieldCardPrefab, DungeonInterface.Instance.My_FieldTransform, false).transform;
            card.GetComponent<MyFieldCard>().Init(cards[i], IsMyTurn);
        }
    }

    public void ShowEnemyField(List<Card> cards)
    {
        foreach (Transform child in DungeonInterface.Instance.Enemy_FieldTransform) Destroy(child.gameObject);

        for (int i = 0; i < cards.Count; i++)
        {
            Transform card = Instantiate(DungeonInterface.Instance.EnemyFieldCardPrefab, DungeonInterface.Instance.Enemy_FieldTransform, false).transform;
            card.GetComponent<EnemyFieldCard>().Init(cards[i]);
        }
    }

    [PunRPC]
    public void RpcSetTurn(bool host_turn)
    {
        if (PhotonNetwork.IsMasterClient == host_turn)
        {
            //Мой ход
            IsMyTurn = true;
            DungeonInterface.Instance.SetNextTurnStatus(true);
            DungeonInterface.Instance.ShowMyTurn();
        }
        else
        {
            //Не мой ход
            IsMyTurn = false;
            DungeonInterface.Instance.SetNextTurnStatus(false);
        }
    }

    [PunRPC]
    public void RpcRenderDefences(int host_defence_current, int host_defence_maximum, int client_defence_current, int client_defence_maximum)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Хост
            DungeonInterface.Instance.ShowMyDefence(host_defence_current, host_defence_maximum);
            DungeonInterface.Instance.ShowEnemyDefence(client_defence_current, client_defence_maximum);
        }
        else
        {
            //Клиент
            DungeonInterface.Instance.ShowMyDefence(client_defence_current, client_defence_maximum);
            DungeonInterface.Instance.ShowEnemyDefence(host_defence_current, host_defence_maximum);
        }
    }

    [PunRPC]
    public void RpcRenderNicknames(string host_nickname, string client_nickname)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Хост
            DungeonInterface.Instance.ShowMyNickname(host_nickname);
            DungeonInterface.Instance.ShowEnemyNickname(client_nickname);
        }
        else
        {
            //Клиент
            DungeonInterface.Instance.ShowMyNickname(client_nickname);
            DungeonInterface.Instance.ShowEnemyNickname(host_nickname);
        }
    }

    public void SwitchTurn()
    {
        PhotonView.RPC("RpcSwitchTurn", RpcTarget.MasterClient, GameManager.Instance.GetPlayer().ActorNumber);
    }

    public void MoveCardToField(Card card)
    {
        PhotonView.RPC("RpcMoveCardToField", RpcTarget.MasterClient, GameManager.Instance.GetPlayer().ActorNumber, card.Guid);
    }

    public void AttackCard(Card myCard, Card enemyCard)
    {
        PhotonView.RPC("RpcAttackCard", RpcTarget.MasterClient, GameManager.Instance.GetPlayer().ActorNumber, myCard.Guid, enemyCard.Guid);
    }

    public void AttackPlayer(Card myCard)
    {
        PhotonView.RPC("RpcAttackPlayer", RpcTarget.MasterClient, GameManager.Instance.GetPlayer().ActorNumber, myCard.Guid);
    }
}
