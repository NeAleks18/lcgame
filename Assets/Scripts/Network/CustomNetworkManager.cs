using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    [Header("Player Settings")]
    [SerializeField]
    private short maxplayers = 4;

    [HideInInspector]
    public short currplayers;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        gameObject.GetComponent<SteamLobbyManager>().CreateAndOpenLobbyAsync(maxplayers);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        currplayers++;
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        currplayers--;
    }
}
