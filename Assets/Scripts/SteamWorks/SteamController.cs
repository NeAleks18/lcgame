using UnityEngine;

public class SteamController : MonoBehaviour
{
    [SerializeField]
    private short AppID = 480;

    [SerializeField]
    private bool InitializeSteamClient = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (InitializeSteamClient)
        {
            try
            {
                Steamworks.SteamClient.Init((uint)AppID);
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
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        if (InitializeSteamClient) Steamworks.SteamClient.Shutdown();
    }
}
