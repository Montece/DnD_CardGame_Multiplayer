using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hexagon
{
    [JsonIgnore]
    private GameObject HexInstance = null;
    [SerializeField]
    private bool DoOffset;
    [SerializeField]
    private Coordinates Coordinates;
    [SerializeField]
    private HexType HexType;
    //Не работает с ToJson Upcast Downcast, выкручиваемся Newtonosft.Json
    [SerializeField]
    private BuildingType BuildingType = null;
    [SerializeField]
    private List<int> PlayerActorNumbers = new List<int>();

    public Hexagon() {}

    public Hexagon(Coordinates coords, bool doOffset)
    {
        Coordinates = coords;
        DoOffset = doOffset;
    }

    public Coordinates GetCoordinates()
    {
        return Coordinates;
    }

    public HexType GetHexType()
    {
        return HexType;
    }

    public void SetHexType(HexType hexType)
    {
        UpdateHex(PlayerActorNumbers, hexType, BuildingType);
    }

    public void SetBuilding(BuildingType buildingType)
    {
        UpdateHex(PlayerActorNumbers, HexType, buildingType);
    }

    public void ResetBuilding()
    {
        UpdateHex(PlayerActorNumbers, HexType, null);
    }

    public void UpdateHex(List<int> playerActorNumber, HexType newHexType, BuildingType newBuildingType)
    {
        PlayerActorNumbers = playerActorNumber;
        HexType = newHexType;
        BuildingType = newBuildingType;
        RenderHex();
    }

    public BuildingType GetBuildingType()
    {
        return BuildingType;
    }

    public void AddPlayer(int playerActorNumber)
    {
        PlayerActorNumbers.Add(playerActorNumber);
        UpdateHex(PlayerActorNumbers, HexType, BuildingType);
    }

    public void RemovePlayer(int playerActorNumber)
    {
        if (PlayerActorNumbers.Contains(playerActorNumber))
        {
            PlayerActorNumbers.Remove(playerActorNumber);
            UpdateHex(PlayerActorNumbers, HexType, BuildingType);
        }
    }

    public List<int> GetPlayerActors()
    {
        return PlayerActorNumbers;
    }

    ///<summary> Получение модели хексагона на карте </summary>
    public GameObject GetInstance()
    {
        return HexInstance;
    }

    /// <summary> Автоматический вызов при измененях </summary>
    private void RenderHex()
    {
        if (HexInstance != null)
        {
            GameObject.Destroy(HexInstance);
            HexInstance = null;
        }

        GameObject HexPrefab = null;
        GameObject BuildingPrefab = null;
        switch (HexType)
        {
            case HexType.Grass:
                HexPrefab = Resources.Load<GameObject>("Hexagones/HexagonGrass");
                break;
            case HexType.Water:
                HexPrefab = Resources.Load<GameObject>("Hexagones/HexagonWater");
                break;
            case HexType.Mountain:
                HexPrefab = Resources.Load<GameObject>("Hexagones/HexagonMountain");
                break;
            default:
                HexPrefab = null;
                break;
        }

        if (BuildingType != null)
        {
            if (Helper.IsType<BuildingDungeon>(BuildingType))
            {
                BuildingPrefab = Resources.Load<GameObject>("Buildings/BuildingDungeon");
            }
            else if (Helper.IsType<BuildingTreasure>(BuildingType))
            {
                BuildingPrefab = Resources.Load<GameObject>("Buildings/BuildingTreasure");
            }
        }

        if (HexPrefab == null) return;

        float size = 0.5F * HexPrefab.transform.localScale.x;
        Transform MapHolder = GameObject.FindGameObjectWithTag("MapHolder").transform;
        Vector3 pos = new Vector3(size * Mathf.Sqrt(3) * Coordinates.X + (DoOffset ? size * Mathf.Sqrt(3) / 2F : 0F), 0F, 3F / 2F * size * Coordinates.Y);
        HexInstance = GameObject.Instantiate(HexPrefab, pos, Quaternion.identity, MapHolder);
        HexInstance.transform.Rotate(new Vector3(0F, 90F, 0F));
        HexInstance.name = $"Hexagon {Coordinates.X} {Coordinates.Y}";
        HexInstance.GetComponent<HexagonModel>().SetHexagon(this);

        if (BuildingPrefab != null)
        {
            Transform building = GameObject.Instantiate(BuildingPrefab).transform;
            building.transform.position = HexInstance.transform.position + new Vector3(0F, 1F, 0F);
            building.SetParent(HexInstance.transform);
        }
    }

    public void ForceRenderHex()
    {
        RenderHex();
    }
}
