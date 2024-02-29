using Steamworks;
using UnityEngine;
using System.Threading.Tasks;

public class SteamLobbyManager : MonoBehaviour
{
    
    public void CreateAndOpenLobbyAsync(int maxMembers) => SteamMatchmaking.CreateLobbyAsync(maxMembers);

    private void OnEnable() => SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;

    private void OnDisable() => SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;
    

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
}
