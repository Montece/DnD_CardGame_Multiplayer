using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PhotonPlayer = Photon.Realtime.Player;

public class HostManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Player CurrentPlayer; //Текущий игрок

    //ОТ ЛЮБОГО ДЕЙСТВИЯ ВНУТРИ ВЫЗЫВАТЬ SYNC PLAYERS
    [SerializeField]
    private PlayersListContainer PlayersContainer;

    public static HostManager Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;

        SceneManager.sceneLoaded += SceneLoaded;

        PlayersContainer = new PlayersListContainer { PlayersList = new List<Player>() };
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Adventure") StartAdventure();
    }

    private void StartAdventure()
    {
        SyncPlayers();

        for (int i = 0; i < PlayersContainer.PlayersList.Count; i++)
        {
            MapManager.Instance.TeleportPlayer(PlayersContainer.PlayersList[i], PlayersContainer.PlayersList[i].Coordinates);
        }

        MapManager.Instance.ForceMapRender();
        MapManager.Instance.SyncMap();
    }

    private void Start()
    {
        //Инициализация списка игроков
        foreach (PhotonPlayer photonPlayer in PhotonNetwork.PlayerList)
        {
            Player player = new Player()
            {
                Nickname = photonPlayer.NickName,
                HealthCurrent = 1,
                HealthMaximum = 1,
                ManaMaximum = 7,
                MoveDistance = GameManager.PLAYER_BASE_MOVESPEED,
                PlayerModel = null,
                ActorNumber = photonPlayer.ActorNumber,
                Coordinates = Coordinates.WrongPosition
            };

            if (photonPlayer.ActorNumber.Equals(PhotonNetwork.LocalPlayer.ActorNumber)) CurrentPlayer = player;
            
            PlayersContainer.PlayersList.Add(player);

            //CardsPack pack = GameResources.Instance.GetCardsPack("p_necromancy");
            //for (int i = 0; i < 10; i++) player.Inventory.AddCard(pack.GetRandomCard());
            //for (int i = 0; i < 1; i++) player.Inventory.AddItem(Item.CreateItem("i_healing_potion"));
            //for (int i = 0; i < 5; i++) player.Inventory.AddItem(Item.CreateItem("i_health_stone"));
            //for (int i = 0; i < 5; i++) player.Inventory.AddItem(Item.CreateItem("i_mana_stone"));
            //for (int i = 0; i < 10; i++) player.Inventory.AddItem(Item.CreateItem("i_pack_necromancy"));
        }

        //Синхронизация словаря игроков
        SyncPlayers();

        //Действия над игроками, их перемещение на карту
        for (int i = 0; i < PlayersContainer.PlayersList.Count; i++)
        {
            //Сам всё синхронизирует
            MapManager.Instance.TeleportPlayer(PlayersContainer.PlayersList[i], MapManager.Instance.GetRandomHex(HexType.Grass).GetCoordinates());
        }
    }

    public void SyncPlayers()
    {
        Hashtable hash = new Hashtable { ["players"] = PlayersContainer };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public Player GetPlayer(int playerActorNumber)
    {
        return PlayersContainer.PlayersList.Find(p => p.ActorNumber == playerActorNumber);
    }

    public override void OnPlayerLeftRoom(PhotonPlayer otherPlayer)
    {
        Player player = GetPlayer(otherPlayer.ActorNumber);
        Destroy(player.PlayerModel.gameObject);
        PlayersContainer.PlayersList.Remove(player);

        Debug.Log($"Player {otherPlayer.NickName} disconnected.");
    }
}