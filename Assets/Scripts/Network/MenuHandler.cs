using Mirror;
using Mirror.FizzySteam;
using System;
using UnityEngine;
using UnityEngine.UI;
public class MenuHandler : MonoBehaviour
{
    [Header("Multiplayer Menu Handler")]

    [field: SerializeField]
    [field: Tooltip("Button for Host")]
    private Button _hostbutton;

    [field: SerializeField]
    [field: Tooltip("Button for Connect")]
    private Button _connectbutton;

    [field: SerializeField]
    [field: Tooltip("Text IP")]
    private TMPro.TMP_InputField _steamidtext;

    [field: SerializeField]
    [field: Tooltip("Text for Attempting Connection")]
    private TMPro.TextMeshProUGUI _attemptingConnect;

    [field: SerializeField]
    [field: Tooltip("Button for Cancel Connection")]
    private Button _cancelConnect;

    private CustomNetworkManager _networkmanager;

    private void Awake() => _networkmanager = FindObjectOfType<CustomNetworkManager>();

    private void OnEnable()
    {
        _steamidtext.text = _networkmanager.GetComponent<FizzyFacepunch>().SteamUserID.ToString();
        _hostbutton.onClick.AddListener(HostingSession);
        _connectbutton.onClick.AddListener(ConnectSession);
        _steamidtext.onValueChanged.AddListener(IDChanged);
        _cancelConnect.onClick.AddListener(_networkmanager.StopClient);
        _networkmanager.GetComponent<FizzyFacepunch>().OnClientError += ClientError;
    }

    private void OnDisable()
    {
        _cancelConnect.onClick.RemoveListener(_networkmanager.StopClient);
        _networkmanager.GetComponent<FizzyFacepunch>().OnClientError -= ClientError;
        _hostbutton.onClick.RemoveListener(HostingSession);
        _connectbutton.onClick.RemoveListener(ConnectSession);
        _steamidtext.onValueChanged.RemoveListener(IDChanged);
    }

    private void Update()
    {
        if (_networkmanager.isNetworkActive)
        {
            _attemptingConnect.gameObject.SetActive(true);
            _cancelConnect.gameObject.SetActive(true);
        }
        else
        {
            _attemptingConnect.gameObject.SetActive(false);
            _cancelConnect.gameObject.SetActive(false);
        }
    }

    private void ClientError(TransportError error, string arg1) => _networkmanager.StopClient();

    private void IDChanged(string id) => _networkmanager.networkAddress = id;

    private void HostingSession()
    {
        if (_networkmanager.isNetworkActive) _networkmanager.StopClient();
        _networkmanager.StartHost();
    }

    private void ConnectSession()
    {
        if (_networkmanager.isNetworkActive) _networkmanager.StopClient();
        _networkmanager.networkAddress = _steamidtext.text;
        _networkmanager.StartClient();
    }
}
