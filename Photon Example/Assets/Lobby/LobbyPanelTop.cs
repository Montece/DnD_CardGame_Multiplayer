using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelTop : MonoBehaviour
{
    private readonly string connectionStatusMessage = "    Статус соединения: ";

    [Header("UI References")]
    public Text ConnectionStatusText;

    public void Update()
    {
        ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
    }
}
