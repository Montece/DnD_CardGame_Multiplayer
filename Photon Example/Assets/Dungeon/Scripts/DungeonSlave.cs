using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSlave : MonoBehaviour
{
    public static DungeonSlave Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
    }

    [PunRPC]
    public void RpcRenderCardsClient(int enemy_arm_count)
    {
        DungeonManager.Instance.ShowMyArm(GetClientArm());
        DungeonManager.Instance.ShowEnemyArm(enemy_arm_count);
        DungeonManager.Instance.ShowMyField(GetClientField());
        DungeonManager.Instance.ShowEnemyField(GetHostField());
    }

    [PunRPC]
    private void RpcRenderManaClient(int my_mana_current, int my_mana_local_maximum)
    {
        DungeonInterface.Instance.ShowMana(my_mana_current, my_mana_local_maximum);
    }

    private List<Card> GetClientArm()
    {
        object rawObject = PhotonNetwork.CurrentRoom.CustomProperties["client_arm"];
        if (rawObject == null) return null;

        CardsContainer CC = (CardsContainer)rawObject;
        if (CC == null) return null;

        return CC.Cards;
    }

    private List<Card> GetClientField()
    {
        object rawObject = PhotonNetwork.CurrentRoom.CustomProperties["client_field"];
        if (rawObject == null) return null;

        CardsContainer CC = (CardsContainer)rawObject;
        if (CC == null) return null;

        return CC.Cards;
    }

    private List<Card> GetHostField()
    {
        object rawObject = PhotonNetwork.CurrentRoom.CustomProperties["host_field"];
        if (rawObject == null) return null;

        CardsContainer CC = (CardsContainer)rawObject;
        if (CC == null) return null;

        return CC.Cards;
    }
}
