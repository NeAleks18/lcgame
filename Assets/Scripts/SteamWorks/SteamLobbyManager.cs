using Steamworks;
using System.Threading.Tasks;
using UnityEngine;

public class SteamLobbyManager : MonoBehaviour
{
    
    // Создание лобби
    public void CreateAndOpenLobbyAsync(int maxMembers) => SteamMatchmaking.CreateLobbyAsync(maxMembers);

    // Подписка событий
    private void OnEnable() 
    {
        SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += HandleImEnterInLobby;
        SteamFriends.OnGameLobbyJoinRequested += OnLobbyRequestJoinAsync;
    }

    // Отписка событий
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
