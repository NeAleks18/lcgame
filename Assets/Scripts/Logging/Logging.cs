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
            // ���������� ��� ������� � ����� DiscordWebhookAPI
            foreach (var obj in FindObjectsOfType<DiscordWebhookAPI>())
            {
                // ���������, �������� �� ������ ����������� ���������� ArchorName � ������ ���������
                if (obj.ArchorName == "Logging")
                {
                    // ������ ���-�� �� ��������� ��������
                    api = obj;
                }
            }
            if (api == null)
            {
                Debug.Log("������ � ������ ������ �� ������!");
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
