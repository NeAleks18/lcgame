using UnityEngine;

public class SteamController : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            Steamworks.SteamClient.Init(480);
        }
        catch (System.Exception e)
        {
            // Something went wrong - it's one of these:
            //
            //     Steam is closed?
            //     Can't find steam_api dll?
            //     Don't have permission to play app?
            //

            Debug.LogError(e.Message);
        }
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
