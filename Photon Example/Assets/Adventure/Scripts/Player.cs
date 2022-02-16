using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using UnityEngine;

using PhotonPlayer = Photon.Realtime.Player;

[System.Serializable]
public class Player
{
    [SerializeField]
    public string Nickname;
    [SerializeField]
    public int MoveDistance;
    [SerializeField]
    public int HealthCurrent;
    [SerializeField]
    public int HealthMaximum;
    [SerializeField]
    public int ManaMaximum;
    [SerializeField]
    public Coordinates Coordinates;
    [SerializeField]
    public Inventory Inventory; //Инвентарь предметов

    [JsonIgnore]
    public PlayerModel PlayerModel;

    //Упрощенная ссылка на PhotonPlayer (синхронизируется)
    [SerializeField]
    public int ActorNumber;

    public Player()
    {
        Inventory = new Inventory();
    }

    public void SetModelPosition(Vector3 position)
    {
        PlayerModel.SetModelPosition(position);
    }
}