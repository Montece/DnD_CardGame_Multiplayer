using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private GameObject CanPathHex;
    [SerializeField]
    private GameObject CannotPathHex;
    [SerializeField]
    private GameObject HexagonHoverPrefab;

    private Camera GameCam;
    private PhotonView PhotonView;
    private HexagonModel HoverHexModel = null; //Шестиугольник, который мы выделили
    private Coordinates StartMoveCoordinates = Coordinates.WrongPosition; //Начальная цель, откуда пойдет игрок
    private HexagonModel EndMoveHexModel = null; //Конечная цель, куда пойдет игрок
    private GameObject HexagonHoverInstance; //Объект выделения шестиугольнка
    private List<GameObject> PathHexes = new List<GameObject>();

    private void Awake()
    {
        GameCam = GetComponent<Camera>();
        PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        //Создаем HexagonHover
        HexagonHoverInstance = Instantiate(HexagonHoverPrefab);
        HexagonHoverInstance.SetActive(false);
    }

    private void Update()
    {
        //Перемещение камеры
        Movement();

        Ray ray = GameCam.ScreenPointToRay(Input.mousePosition);
        bool onGameObject = Physics.Raycast(ray, out RaycastHit hit);

        //Наведен ли курсор на шестиугольник?
        HexagonModel hexModel = null;
        if (onGameObject) hexModel = hit.collider.GetComponent<HexagonModel>();

        if (hexModel != null) //Если курсор на шестиугольнике
        {
            //Выделяем клетку
            DoHoverHex(hexModel);

            //Передвижение игрока
            bool MovingMode = Input.GetKey(KeyCode.Mouse1);
            if (MovingMode)
            {
                //Отрисовываем путь единожды
                if (EndMoveHexModel != hexModel || StartMoveCoordinates != GameManager.Instance.GetPlayer().Coordinates)
                {
                    Player player = GameManager.Instance.GetPlayer();
                    if (player == null) return;

                    StartMoveCoordinates = player.Coordinates;
                    EndMoveHexModel = hexModel;
                    UndrawPathToHex();

                    List<Hexagon> Path = MapManager.Instance.FindPath(MapManager.Instance.GetHexByCoords(GameManager.Instance.GetPlayer().Coordinates), hexModel.GetHex());
                    if (Path != null)
                    {
                        //Удаляем клетку на которой стоит игрок
                        Path.RemoveAt(0);
                        List<Hexagon> CanPath = Path.GetRange(0, GameManager.Instance.GetPlayer().MoveDistance > Path.Count ? Path.Count : GameManager.Instance.GetPlayer().MoveDistance);
                        DrawCanPathToHex(CanPath);

                        if (GameManager.Instance.GetPlayer().MoveDistance < Path.Count)
                        {
                            List<Hexagon> CannotPath = Path.GetRange(GameManager.Instance.GetPlayer().MoveDistance, Path.Count - CanPath.Count);
                            DrawCannotPathToHex(CannotPath);
                        }
                    }
                }

                //Try move player
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartMoveCoordinates = Coordinates.WrongPosition;
                    EndMoveHexModel = null;

                    MapManager.Instance.MovePlayer(GameManager.Instance.GetPlayer(), hexModel.GetHex().GetCoordinates());
                }
            }
            else
            {
                //Скрываем путь единожды
                if (EndMoveHexModel != null)
                {
                    UndrawPathToHex();
                    StartMoveCoordinates = Coordinates.WrongPosition;
                    EndMoveHexModel = null;
                }
            }
        }

        //Активация клетки, на которой стоит игрок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<int> Players = MapManager.Instance.GetHexByCoords(GameManager.Instance.GetPlayer().Coordinates).GetPlayerActors();
            if (Players != null)
            {
                if (Players.Count == 2)
                {
                    int id = Players.Where(p => p != GameManager.Instance.GetPlayer().ActorNumber).First();
                    DungeonManager.Instance.StartDungeon(GameManager.Instance.GetPlayer().ActorNumber, id);
                }
                else
                {
                    MapManager.Instance.ActivateBuilding(GameManager.Instance.GetPlayer(), GameManager.Instance.GetPlayer().Coordinates);
                }
            }
        }

        //Показ инвентаря
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameInterface.Instance.ToggleInventory();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * Time.deltaTime * MovementSpeed;
        }
    }

    ///<summary> Выделяем шестиугольник </summary>
    private void DoHoverHex(HexagonModel hexModel)
    {
        if (HoverHexModel == hexModel) return; //Если мы пытаемся выделить то что уже выделено, не делаем
        if (HoverHexModel != null) DoUnhoverHex(); //Если было выделение - снимаем
        HoverHexModel = hexModel; //Задаем и отрисовываем новое выделение
        HexagonHoverInstance.transform.position = HoverHexModel.transform.position + Vector3.up * 3F;
        HexagonHoverInstance.SetActive(true);
    }

    ///<summary> Снимаем выделение шестиугольника </summary>
    private void DoUnhoverHex()
    {
        HexagonHoverInstance.SetActive(false);
        HoverHexModel = null;
    }

    ///<summary> Удаляем визуальный возможный путь </summary>
    private void UndrawPathToHex()
    {
        for (int i = 0; i < PathHexes.Count; i++) Destroy(PathHexes[i]);
        PathHexes.Clear();
    }

    ///<summary> Показываем визуальный путь, который можем пройти </summary>
    private void DrawCanPathToHex(List<Hexagon> path)
    {
        if (path == null) return;

        for (int i = 0; i < path.Count; i++)
        {
            GameObject hex = Instantiate(CanPathHex);
            hex.transform.position = path[i].GetInstance().transform.position + Vector3.up * 3F;
            PathHexes.Add(hex);
        }
    }

    ///<summary> Показываем визуальный путь, который не можем пройти </summary>
    private void DrawCannotPathToHex(List<Hexagon> path)
    {
        if (path == null) return;

        for (int i = 0; i < path.Count; i++)
        {
            GameObject hex = Instantiate(CannotPathHex);
            hex.transform.position = path[i].GetInstance().transform.position + Vector3.up * 3F;
            PathHexes.Add(hex);
        }
    }
}
