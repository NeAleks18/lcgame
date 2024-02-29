using Steamworks;
using System.Threading.Tasks;
using UnityEngine;

public class SteamLobbyManager : MonoBehaviour
{
    
    public void CreateAndOpenLobbyAsync(int maxMembers) => SteamMatchmaking.CreateLobbyAsync(maxMembers);

    private void OnEnable() 
    {
        SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += HandleImEnterInLobby;
        SteamFriends.OnGameLobbyJoinRequested += OnLobbyRequestJoinAsync;
    }

    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= HandleImEnterInLobby;
        SteamFriends.OnGameLobbyJoinRequested -= OnLobbyRequestJoinAsync;
    }


    private void HandleLobbyCreated(Result result, Steamworks.Data.Lobby lobby)
    {
        if (result == Result.OK)
        {
            Debug.Log("Лобби успешно создано!"+ lobby.Id);
            // Здесь вы можете добавить дополнительный код, который должен выполняться после успешного создания лобби.
            lobby.SetFriendsOnly();
        }
        else
        {
            Debug.Log("Не удалось создать лобби.");
            // Здесь вы можете добавить дополнительный код для обработки ошибок.
        }
    }

    private void HandleImEnterInLobby(Steamworks.Data.Lobby lobby)
    {
        if (lobby.Owner.Id == SteamClient.SteamId) return;
        Debug.Log($"I'm Join to {lobby.Id} and Owner is {lobby.Owner.Name}");
        gameObject.GetComponent<CustomNetworkManager>().networkAddress = lobby.Owner.Id.ToString();
        gameObject.GetComponent<CustomNetworkManager>().StartClient();
    }

    private void OnLobbyRequestJoinAsync(Steamworks.Data.Lobby lobby, SteamId id) => SteamMatchmaking.JoinLobbyAsync(lobby.Id);
}
