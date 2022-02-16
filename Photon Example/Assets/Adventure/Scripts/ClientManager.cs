using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using PhotonPlayer = Photon.Realtime.Player;

public class ClientManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, PlayerModel> PlayersModels;
 
    public static ClientManager Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;

        PlayersModels = new Dictionary<int, PlayerModel>();
    }

    private List<Player> GetPlayersList()
    {
        object rawObject = PhotonNetwork.CurrentRoom.CustomProperties["players"];
        if (rawObject == null) return null;

        PlayersListContainer PLC = (PlayersListContainer)rawObject;
        if (PLC == null) return null;

        return PLC.PlayersList;
    }

    public Player GetCurrentPlayer()
    {
        return GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public Player GetPlayer(int playerActorNumber)
    {
        return GetPlayersList()?.Find(p => p.ActorNumber == playerActorNumber);
    }

    [PunRPC]
    public void RpcSetModelPosition(int playerActorNumber, Vector3 position)
    {
        if (!PlayersModels.ContainsKey(playerActorNumber))
        {
            PlayerModel playerObject = Instantiate(GameManager.Instance.PlayerPrefab).GetComponent<PlayerModel>();
            playerObject.SetModelNickname(GetPlayer(playerActorNumber).Nickname);
            PlayersModels.Add(playerActorNumber, playerObject);
        }

        if (PlayersModels.ContainsKey(playerActorNumber) && PlayersModels[playerActorNumber] == null)
        {
            PlayerModel playerObject = Instantiate(GameManager.Instance.PlayerPrefab).GetComponent<PlayerModel>();
            playerObject.SetModelNickname(GetPlayer(playerActorNumber).Nickname);
            PlayersModels[playerActorNumber] = playerObject;
        }

        PlayersModels[playerActorNumber].SetModelPosition(position);
    }

    public override void OnPlayerLeftRoom(PhotonPlayer otherPlayer)
    {
        PhotonNetwork.LoadLevel("Lobby");

        Debug.Log($"Host disconnected.");
    }
}
