using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private int Width;
    [SerializeField]
    private int Height;

    private Hexagon[,] HexMap;
    private PhotonView PhotonView;

    public static MapManager Instance;

    private void Awake()
    {
        //Singleton
        Instance = this;
    }

    public void Initialize(int width, int height)
    {
        PhotonView = GetComponent<PhotonView>();

        Width = width;
        Height = height;

        //Заполняем карту
        InitMap();

        //Хост генерирует карту
        if (PhotonNetwork.IsMasterClient)
        {
            //Генерируем карту
            GenerateFlat(HexType.Grass);
            GenerateBorders(HexType.Mountain);
            GenerateStrips(HexType.Mountain, 1, 3, 30);
            GenerateStrips(HexType.Water, 10, 20, 2);
            GenerateRandomDungeons(HexType.Grass, 5);
            GenerateRandomTreasures(HexType.Grass, 10);

            //Обновляем у клиентов сгенерированную карту
            SyncMap();
        }
    }

    private void InitMap()
    {
        bool offset = false;

        HexMap = new Hexagon[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                HexMap[x, y] = new Hexagon(new Coordinates(x, y), offset);
            }

            offset = !offset;
        }
    }

    private void GenerateFlat(HexType type)
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                HexMap[x, y].SetHexType(type);
    }

    private void GenerateBorders(HexType type)
    {
        //Первый слой
        for (int y = 0; y < Height; y++) HexMap[0, y].SetHexType(type);
        for (int y = 0; y < Height; y++) HexMap[Width - 1, y].SetHexType(type);
        for (int x = 0; x < Width; x++) HexMap[x, 0].SetHexType(type);
        for (int x = 0; x < Width; x++) HexMap[x, Height - 1].SetHexType(type);

        //Второй слой
        for (int y = 0; y < Height; y++) HexMap[1, y].SetHexType(type);
        for (int y = 0; y < Height; y++) HexMap[Width - 2, y].SetHexType(type);
        for (int x = 0; x < Width; x++) HexMap[x, 1].SetHexType(type);
        for (int x = 0; x < Width; x++) HexMap[x, Height - 2].SetHexType(type);
    }

    private void GenerateStrips(HexType type, int minLength, int maxLength, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int length = Random.Range(minLength, maxLength + 1);
            Hexagon previousHex = null;
            Hexagon currentHex = GetRandomHex(HexType.Grass);

            for (int j = 0; j < length; j++)
            {
                currentHex.SetHexType(type);
                SyncHex(new MapUpdateParams() { Coordinates = currentHex.GetCoordinates(), NewHexType = type });

                previousHex = currentHex;

                List<Hexagon> hexes = GetHexNeighbours(currentHex).ToList().FindAll(h => h != null && h != previousHex);
                currentHex = hexes[Random.Range(0, hexes.Count)];
            }
        } 
    }

    private void GenerateRandomDungeons(HexType hexType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Hexagon hex = GetRandomHex(hexType);
            BuildingDungeon dungeon = new BuildingDungeon()
            {
                EnemyDeck = new List<Card>()
            };
            hex.SetBuilding(dungeon);
        }
    }

    private void GenerateRandomTreasures(HexType hexType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Hexagon hex = GetRandomHex(hexType);
            BuildingTreasure treasure = new BuildingTreasure()
            {
                Reward = new List<Item>()
                {
                    GameResources.Instance.GetRandomCardsPackItem()
                }
            };

            switch (Random.Range(0, 2 + 1))
            {
                case 0:
                    treasure.Reward.Add(Item.CreateItem("i_healing_potion"));
                    break;
                case 1:
                    treasure.Reward.Add(Item.CreateItem("i_health_stone"));
                    break;
                case 2:
                    treasure.Reward.Add(Item.CreateItem("i_mana_stone"));
                    break;
                default:
                    break;
            }
            hex.SetBuilding(treasure);
        }
    }

    public Hexagon GetRandomHex(HexType? type = null)
    {
        Hexagon hexagon = null;

        if (!type.HasValue)
            hexagon = HexMap[Random.Range(0, Width), Random.Range(0, Height)];
        else
            do
            {
                hexagon = HexMap[Random.Range(0, Width), Random.Range(0, Height)];
            }
            while (hexagon.GetHexType() != type);

        return hexagon;
    }

    public void ChangeHex(Hexagon hexagon)
    {
        hexagon.SetHexType(HexType.Water);
        SyncHex(new MapUpdateParams(hexagon));
    } 

    public Hexagon GetHexByCoords(Coordinates coords)
    {
        if (coords.X < 0 || coords.X >= Width || coords.Y < 0 || coords.Y >= Height) return null;
        return HexMap[coords.X, coords.Y];
    }

    public void MovePlayer(Player player, Coordinates coord)
    {
        PhotonView.RPC("RpcMovePlayer", RpcTarget.MasterClient, player.ActorNumber, coord);
    }

    public void TeleportPlayer(Player player, Coordinates coord)
    {
        if (IsBlockedHex(GetHexByCoords(coord))) return;

        if (player.Coordinates != Coordinates.WrongPosition) HexMap[player.Coordinates.X, player.Coordinates.Y].RemovePlayer(player.ActorNumber);
        player.Coordinates = coord;
        HexMap[coord.X, coord.Y].AddPlayer(player.ActorNumber);

        if (player.PlayerModel == null)
        {
            player.PlayerModel = Instantiate(GameManager.Instance.PlayerPrefab).GetComponent<PlayerModel>();
            player.PlayerModel.SetModelNickname(player.Nickname);
        }
        player.SetModelPosition(HexMap[coord.X, coord.Y].GetInstance().transform.position);
        PhotonView.RPC("RpcSetModelPosition", RpcTarget.Others, player.ActorNumber, HexMap[coord.X, coord.Y].GetInstance().transform.position);

        HostManager.Instance.SyncPlayers();
    }

    public void ActivateBuilding(Player player, Coordinates coord)
    {
        PhotonView.RPC("RpcActivateBuilding", RpcTarget.MasterClient, player.ActorNumber, coord);
    }

    [PunRPC]
    public void RpcMovePlayer(int playerActorNumber, Coordinates coord)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        Hexagon startHex = GetHexByCoords(player.Coordinates);
        List<Hexagon> Path = FindPath(startHex, GetHexByCoords(coord));

        if (Path == null) return;

        Path.RemoveAt(0);
        Path = Path.GetRange(0, player.MoveDistance > Path.Count ? Path.Count : player.MoveDistance);

        if (Path.Count <= 0) return;

        startHex.RemovePlayer(playerActorNumber);
        while (Path.Count != 0)
        {
            //Move to every hex
            Hexagon previousHex = HexMap[player.Coordinates.X, player.Coordinates.Y];
            Hexagon currentHex = Path[0];
            if (currentHex == null) return;
            if (IsBlockedHex(currentHex)) return;

            player.Coordinates = currentHex.GetCoordinates();
            HexMap[previousHex.GetCoordinates().X, previousHex.GetCoordinates().Y].RemovePlayer(player.ActorNumber);
            HexMap[currentHex.GetCoordinates().X, currentHex.GetCoordinates().Y].AddPlayer(player.ActorNumber);
            player.SetModelPosition(HexMap[currentHex.GetCoordinates().X, currentHex.GetCoordinates().Y].GetInstance().transform.position);
            PhotonView.RPC("RpcSetModelPosition", RpcTarget.Others, player.ActorNumber, HexMap[currentHex.GetCoordinates().X, currentHex.GetCoordinates().Y].GetInstance().transform.position);
            HostManager.Instance.SyncPlayers();
            SyncHex(new MapUpdateParams(HexMap[player.Coordinates.X, player.Coordinates.Y].GetPlayerActors(), player.Coordinates, HexMap[player.Coordinates.X, player.Coordinates.Y].GetHexType(), HexMap[player.Coordinates.X, player.Coordinates.Y].GetBuildingType()));
            
            //Play hex event
            Path.RemoveAt(0);
        }
    }

    [PunRPC]
    public void RpcActivateBuilding(int playerActorNumber, Coordinates coord)
    {
        Player player = HostManager.Instance.GetPlayer(playerActorNumber);
        Hexagon hex = GetHexByCoords(coord);
        BuildingType buildingType = GetHexByCoords(coord).GetBuildingType();

        if (buildingType == null) return;

        if (Helper.IsType<BuildingDungeon>(buildingType))
        {

        }
        else if (Helper.IsType<BuildingTreasure>(buildingType))
        {
            BuildingTreasure treasure = (BuildingTreasure)buildingType;

            player.Inventory.AddItems(treasure.Reward);

            hex.ResetBuilding();

            SyncHex(new MapUpdateParams(hex));
            HostManager.Instance.SyncPlayers();

            //Показать игроку его награду
            PhotonNetwork.RaiseEvent(NetworkHelper.CODE_UI_SHOW_REWARD, new SlotItemContainer() { Items = treasure.Reward }, new RaiseEventOptions { TargetActors = new int[] { playerActorNumber } }, SendOptions.SendReliable);
        }
        else
        {
            //Нет построек
        }
    }

    public void SyncMap()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                SyncHex(new MapUpdateParams(HexMap[x, y].GetPlayerActors(), new Coordinates(x, y), HexMap[x, y].GetHexType(), HexMap[x, y].GetBuildingType()));
    }

    public void SyncHex(MapUpdateParams mapUpdateParams)
    {
        PhotonView.RPC("RpcUpdateHex", RpcTarget.Others, mapUpdateParams);
    }

    [PunRPC]
    private void RpcUpdateHex(MapUpdateParams parameters)
    {
        HexMap[parameters.Coordinates.X, parameters.Coordinates.Y].UpdateHex(parameters.NewPlayerActorNumbers, parameters.NewHexType, parameters.NewBuildingType);
    }

    public void ForceMapRender()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                HexMap[x, y].ForceRenderHex();
        SyncMap();
    }
    
    #region Find Path

    ///<summary> Main Find Path Function </summary>
    public List<Hexagon> FindPath(Hexagon from, Hexagon to)
    {
        if (IsBlockedHex(to)) return null;
       
        List<PathNode> ClosedNodes = new List<PathNode>();
        List<PathNode> OpenedNodes = new List<PathNode>();

        PathNode StartNode = new PathNode()
        {
            Hex = from,
            CameFrom = null,
            PathLengthFromStart = 0,
            MaybePathLength = GetHeuristicPathLength(from, to)
        };

        OpenedNodes.Add(StartNode);

        while (OpenedNodes.Count > 0)
        {
            PathNode CurrentNode = OpenedNodes.OrderBy(node => node.GetFullPathLength()).First();

            if (CurrentNode.Hex.GetCoordinates().Equals(to.GetCoordinates()))
            {
                //Found path
                List<Hexagon> FullPath = GetPathForNode(CurrentNode);
                return FullPath;
            }

            OpenedNodes.Remove(CurrentNode);
            ClosedNodes.Add(CurrentNode);

            foreach (PathNode neighbourNode in GetPathNodeNeighbours(CurrentNode, to))
            {
                if (ClosedNodes.Count(node => node.Hex.GetCoordinates().Equals(neighbourNode.Hex.GetCoordinates())) > 0) continue;
                PathNode openNode = OpenedNodes.FirstOrDefault(node => node.Hex.GetCoordinates().Equals(neighbourNode.Hex.GetCoordinates()));

                if (openNode == null) OpenedNodes.Add(neighbourNode);
                else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    openNode.CameFrom = CurrentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }

        return null;
    }

    public bool IsBlockedHex(Hexagon hex)
    {
        if (hex.GetHexType() == HexType.Mountain || hex.GetHexType() == HexType.Water) return true;
        return false;
    }

    public Hexagon[] GetHexNeighbours(Hexagon hex)
    {
        if (hex.GetCoordinates().Y % 2 != 0)
        {
            return new Hexagon[]
            {
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(1, 0)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(-1, 0)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(0, 1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(0, -1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(1, 1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(1, -1))
            };
        }
        else
        {
            return new Hexagon[]
            {
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(1, 0)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(-1, 0)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(0, 1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(0, -1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(-1, 1)),
                GetHexByCoords(hex.GetCoordinates() + new Coordinates(-1, -1))
            };
        }
    } 

    private int GetHeuristicPathLength(Hexagon from, Hexagon to)
    {
        return Mathf.Abs(from.GetCoordinates().X - to.GetCoordinates().X) + Mathf.Abs(from.GetCoordinates().Y - to.GetCoordinates().Y);
    }

    private List<PathNode> GetPathNodeNeighbours(PathNode pathNode, Hexagon to)
    {
        List<PathNode> result = new List<PathNode>();

        Hexagon[] neighbourPoints = GetHexNeighbours(pathNode.Hex);

        foreach (Hexagon hex in neighbourPoints)
        {
            if (hex == null) continue;
            if (IsBlockedHex(hex)) continue;

            result.Add(new PathNode()
            {
                Hex = hex,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                MaybePathLength = GetHeuristicPathLength(hex, to)
            });
        }

        return result;
    }

    private List<Hexagon> GetPathForNode(PathNode pathNode)
    {
        List<Hexagon> result = new List<Hexagon>();

        PathNode CurrentNode = pathNode;

        while (CurrentNode != null)
        {
            result.Add(CurrentNode.Hex);
            CurrentNode = CurrentNode.CameFrom;
        }
        result.Reverse();

        return result;
    }

    #endregion
}