using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public short maxplayers = 4;
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
