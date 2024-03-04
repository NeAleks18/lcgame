using Steamworks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Logging : MonoBehaviour
{
    public static Logging instance { get; private set; }

    private DiscordWebhookAPI api;

    public Task Log(DiscordWebhookAPI webhookAPI, string log, bool getmsgdata = false)
    {
        webhookAPI.SendMessage(false, $"[Log] {DateTime.Now.ToString("h:mm:ss tt")} {log}", SteamClient.Name, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false, getmsgdata);
        return Task.CompletedTask;
    }

    private Task ErrorLog(DiscordWebhookAPI webhookAPI, string errorlog)
    {
        webhookAPI.SendMessage(false, $"[ERROR] {DateTime.Now.ToString("h:mm:ss tt")} {errorlog}", SteamClient.Name, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false);
        return Task.CompletedTask;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // Перебираем все объекты с типом DiscordWebhookAPI
            foreach (var obj in FindObjectsOfType<DiscordWebhookAPI>())
            {
                // Проверяем, содержит ли объект необходимую переменную ArchorName с нужным значением
                if (obj.ArchorName == "Logging")
                {
                    // Делаем что-то со найденным объектом
                    api = obj;
                }
            }
            if (api == null)
            {
                Debug.Log("Объект с нужным Якорем не найден!");
            }
            return;
        }
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private async void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error) await ErrorLog(api, logString);
        //else await Log(api, logString);
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        // Initialize
        Log(api, $"Build Pre-Alpha, {Screen.currentResolution}, GPU: {SystemInfo.graphicsDeviceName}, CPU: {SystemInfo.processorType}, OS: {SystemInfo.operatingSystem}, RAM Available: {SystemInfo.systemMemorySize}");
    }
}
