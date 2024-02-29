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
}
