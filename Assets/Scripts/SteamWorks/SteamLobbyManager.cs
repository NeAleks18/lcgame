using Steamworks;
using System.Threading.Tasks;
using UnityEngine;

public class SteamLobbyManager : MonoBehaviour
{
    
    // �������� �����
    public void CreateAndOpenLobbyAsync(int maxMembers) => SteamMatchmaking.CreateLobbyAsync(maxMembers);

    // �������� �������
    private void OnEnable() 
    {
        SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += HandleImEnterInLobby;
        SteamFriends.OnGameLobbyJoinRequested += OnLobbyRequestJoinAsync;
    }

    // ������� �������
    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= HandleImEnterInLobby;
        SteamFriends.OnGameLobbyJoinRequested -= OnLobbyRequestJoinAsync;
    }


    // You created a lobby
    private void HandleLobbyCreated(Result result, Steamworks.Data.Lobby lobby)
    {
        if (result == Result.OK)
        {
            Debug.Log("����� ������� �������!"+ lobby.Id);
            // ����� �� ������ �������� �������������� ���, ������� ������ ����������� ����� ��������� �������� �����.
            lobby.SetFriendsOnly();
        }
        else
        {
            Debug.Log("�� ������� ������� �����.");
            // ����� �� ������ �������� �������������� ��� ��� ��������� ������.
        }
    }

    // You joined a lobby
    private void HandleImEnterInLobby(Steamworks.Data.Lobby lobby)
    {
        if (lobby.Owner.Id == SteamClient.SteamId) return;
        Debug.Log($"I'm Join to {lobby.Id} and Owner is {lobby.Owner.Name}");
        gameObject.GetComponent<CustomNetworkManager>().networkAddress = lobby.Owner.Id.ToString();
        gameObject.GetComponent<CustomNetworkManager>().StartClient();
    }

    // Called when the user tries to join a lobby from their friends list game client should attempt to connect to specified lobby when this is received
    private void OnLobbyRequestJoinAsync(Steamworks.Data.Lobby lobby, SteamId id) => SteamMatchmaking.JoinLobbyAsync(lobby.Id);
}
