using Mirror;
public class CustomNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        gameObject.GetComponent<SteamLobbyManager>().CreateAndOpenLobbyAsync(maxConnections);
    }
}
