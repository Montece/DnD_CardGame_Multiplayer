using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IOnEventCallback
{
    [SerializeField]
    public GameObject PlayerPrefab; //Префаб модели игрока

    public const int PLAYER_BASE_MOVESPEED = 5;

    private PhotonView photonView;

    public static GameManager Instance;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") != null)
        {
            Destroy(gameObject);
            return;
        }

        //Singleton
        Instance = this;

        gameObject.tag = "GameManager";
        gameObject.AddComponent<GameResources>();
        gameObject.AddComponent<MapManager>();
        gameObject.AddComponent<DungeonManager>();
        photonView = gameObject.AddComponent<PhotonView>();
        photonView.ViewID = 1;
        PhotonNetwork.RegisterPhotonView(photonView);
        DontDestroyOnLoad(gameObject);

        //Регистрация новых типов для работы сериализации в синхронизации
        //Заняты байты W V Q P
        PhotonPeer.RegisterType(typeof(Player), (byte)'A', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(PlayersListContainer), (byte)'B', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(MapUpdateParams), (byte)'C', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(Coordinates), (byte)'D', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(BuildingType), (byte)'E', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(SlotItemContainer), (byte)'F', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(Item), (byte)'H', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(CardsContainer), (byte)'I', NetworkHelper.Serialize, NetworkHelper.Deserialize);
        PhotonPeer.RegisterType(typeof(Card), (byte)'J', NetworkHelper.Serialize, NetworkHelper.Deserialize);
    }

    private void Start()
    {
        /* ---=== Во время игры подключаться нельзя! ===--- */

        //Инициализация всех скриптов
        GameResources.Instance.Initialize();
        MapManager.Instance.Initialize(30, 30);
        DungeonManager.Instance.Initialize();

        //Активация нужных скриптов в зависимости от стороны (хост или клиент)
        if (PhotonNetwork.IsMasterClient)
        {
            //Активируем хоста
            name = "Host Manager";
            gameObject.AddComponent<HostManager>();
            gameObject.AddComponent<DungeonMaster>();
        }
        else
        {
            //Активируем клиента
            name = "Client Manager";
            gameObject.AddComponent<ClientManager>();
            gameObject.AddComponent<DungeonSlave>();
        }
    }

    public Player GetPlayer()
    {
        if (PhotonNetwork.IsMasterClient) return HostManager.Instance.CurrentPlayer;
        else return ClientManager.Instance.GetCurrentPlayer();
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case NetworkHelper.CODE_UI_SHOW_REWARD:
                GameInterface.Instance.ShowReward(((SlotItemContainer)photonEvent.CustomData).Items);
                break;
            case NetworkHelper.CODE_UI_SHOW_INVENTORY:
                GameInterface.Instance.ShowInventory();
                break;
            default:
                break;
        }
    }

    public void Use(Item item)
    {
        if (item.InfoItem.UseItemAction != null)
        {
            photonView.RPC(item.InfoItem.UseItemAction.Method.Name, RpcTarget.MasterClient, GetPlayer().ActorNumber, item);
            GameInterface.Instance.ShowInventory();
        }
    }

    public void DeleteCard(Card card)
    {
        if (card != null)
        {
            photonView.RPC("RpcDeleteCard", RpcTarget.MasterClient, GetPlayer().ActorNumber, card);
            GameInterface.Instance.ShowInventory();
        }
    }

    ///<summary> Используем пак карточек и добавляем карточки игроку </summary>
    [PunRPC]
    public void RpcAddCardsFromPack(int playerActorNumber, Item item)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        int count = Random.Range(3, 6);
        CardsPack pack = GameResources.Instance.GetCardsPack(item.InfoItem.AdditionInfo.ToString());
        for (int i = 0; i < count; i++)
        {
            player.Inventory.AddCard(pack.GetRandomCard());
        }

        player.Inventory.RemoveItem(item);

        HostManager.Instance.SyncPlayers();

        //Показать игроку его новый инвентарь
        PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_INVENTORY, null, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
    }

    ///<summary> Восстанавливаем здоровье игрока </summary>
    [PunRPC]
    public void RpcRestorePlayerHealth(int playerActorNumber, Item item)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        player.HealthCurrent = player.HealthMaximum;

        player.Inventory.RemoveItem(item);

        HostManager.Instance.SyncPlayers();

        //Показать игроку его новый инвентарь
        PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_INVENTORY, null, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
    }

    ///<summary> Увеличиваем максимальное здоровье игрока </summary>
    [PunRPC]
    public void RpcAddMaxPlayerHealth(int playerActorNumber, Item item)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        player.HealthMaximum += int.Parse(item.InfoItem.AdditionInfo.ToString());

        player.Inventory.RemoveItem(item);

        HostManager.Instance.SyncPlayers();

        //Показать игроку его новый инвентарь
        PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_INVENTORY, null, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
    }

    ///<summary> Увеличиваем максимальную ману игрока </summary>
    [PunRPC]
    public void RpcAddMaxPlayerMana(int playerActorNumber, Item item)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        player.ManaMaximum += int.Parse(item.InfoItem.AdditionInfo.ToString());

        player.Inventory.RemoveItem(item);

        HostManager.Instance.SyncPlayers();

        //Показать игроку его новый инвентарь
        PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_INVENTORY, null, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
    }

    ///<summary> Удаляем карту из инвентаря игрока </summary>
    [PunRPC]
    public void RpcDeleteCard(int playerActorNumber, Card card)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        player.Inventory.RemoveCard(card);

        HostManager.Instance.SyncPlayers();

        //Показать игроку его новый инвентарь
        PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_INVENTORY, null, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}