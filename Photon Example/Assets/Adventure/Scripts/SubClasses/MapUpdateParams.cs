using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class MapUpdateParams
{
    [SerializeField]
    public Coordinates Coordinates;
    [SerializeField]
    public HexType NewHexType;
    [SerializeField]
    public BuildingType NewBuildingType;
    [SerializeField]
    public List<int> NewPlayerActorNumbers;

    public MapUpdateParams() { }

    public MapUpdateParams(Hexagon hexagon)
    {
        Coordinates = hexagon.GetCoordinates();
        NewHexType = hexagon.GetHexType();
    }

    public MapUpdateParams(List<int> newplayerActorNumbers, Coordinates newCoordinates, HexType newHexType, BuildingType newBuildingType)
    {
        NewPlayerActorNumbers = newplayerActorNumbers;
        Coordinates = newCoordinates;
        NewHexType = newHexType;
        NewBuildingType = newBuildingType;
    }
}