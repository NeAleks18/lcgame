using UnityEngine;

// Временный скрипт с установлением SteamID справа сверху
public class SteamID64Text : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Steamworks.SteamClient.SteamId.ToString();
    }
}
