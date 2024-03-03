using Discord;
using Mirror.FizzySteam;
using System;
using UnityEngine;

public class DiscordController : MonoBehaviour
{
    Discord.Discord discord = new Discord.Discord(1213812279858958376, (UInt64)CreateFlags.Default);
    OverlayManager overlaymanager;


    private void Start()
    {
        overlaymanager = discord.GetOverlayManager();
        if (!overlaymanager.IsEnabled())
        {
            Debug.Log("Overlay is not enabled. Modals will be shown in the Discord client instead");
        }
        if (overlaymanager.IsLocked())
        {
            overlaymanager.SetLocked(true, (res) =>
            {
                Debug.Log("Input in the overlay is now accessible again");
            });
        }

        discord.SetLogHook(LogLevel.Debug, (level, message) =>
        {
            Debug.Log($"Log[{level}] {message}");
        });

        UpdateActivity(false, gameObject.GetComponent<FizzyFacepunch>().SteamAppID, "unity_c0cvdmzbeo");
    }

    public void UpdateActivity(bool partyStarted, uint steamappid, string img, short curplayers = 1, string partyId = "0")
    {
        var activityManager = discord.GetActivityManager();
        activityManager.RegisterSteam(steamappid);
        activityManager.RegisterCommand("game.exe");

        Activity activity;

        if (partyStarted)
        {
            activity = new Activity
            {
                Assets =
                {
                    SmallImage = img,
                },
                Timestamps =
                {
                    Start = 0,
                },
                Party =
                {
                    Id = partyId,
                    Size = {
                        CurrentSize = curplayers,
                        MaxSize = 4,
                    },
                },
                Instance = true,
            };
        } else
        {
            activity = new Activity
            {
                Assets =
                {
                    SmallImage = img,
                },
                Timestamps =
                {
                    Start = 0,
                },
                Instance = true,
            };
        }

        activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Result.Ok)
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }

    private void Update()
    {
        discord.RunCallbacks();
    }

    private void OnDestroy()
    {
        discord.Dispose();
    }
}
