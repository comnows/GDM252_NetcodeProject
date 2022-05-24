using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Text;

public class LoginManager : MonoBehaviour
{
    public Text playerNameInputField;
    public Text passwordInputField;
    public Text passwordText;

    public GameObject loginPanel;
    public GameObject MenuPanel;
    public Transform[] SpawnPointsList;
    public List<string> playerNameLists = new List<string>();

    public PauseManager pauseManager;

    private string password;

    // Start is called before the first frame update
    private void Start() 
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;

        SetUiVisibility(false);
    }

    private void SetUiVisibility(bool isUserLogin)
    {
        if(isUserLogin)
        {
            loginPanel.SetActive(false);
            MenuPanel.SetActive(false);
        }
        else
        {
            loginPanel.SetActive(true);
            MenuPanel.SetActive(false);
        }
    }

    private void OnDestroy() 
    {
        if(NetworkManager.Singleton == null) {return;}
        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
    }

    private void HandleClientDisconnect(ulong obj)
    {
        if(obj == NetworkManager.Singleton.LocalClientId)
        {
            SetUiVisibility(false);
        }
    }

    private void HandleServerStarted()
    {

    }

    private void HandleClientConnected(ulong clientId)
    {
        if(clientId == NetworkManager.Singleton.LocalClientId)
        {
            SetUiVisibility(true);
        }
        Debug.Log("client id = " + clientId);
    }

    public void Leave()
    {
        if(NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.Shutdown();
            playerNameLists.Clear();
            password = "";
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }
        else if(NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
        }
        pauseManager.IsIngame = false;
        SetUiVisibility(false);

    }

    public void Host() 
    {
        CreatePassword();
        pauseManager.IsIngame = true;
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
    {
        string payload = Encoding.ASCII.GetString(connectionData);
        var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);

        bool approveConnection = true;

        if (connectionPayload == null)
        {
            playerNameLists.Add(playerNameInputField.text);
            FindObjectOfType<PlayerCredits>().GetCreditsFirstPlay(playerNameInputField.text);
        }
        else
        {
            approveConnection = !playerNameLists.Contains(connectionPayload.playerName) && connectionPayload.inputPassword == password;
            if (approveConnection)
            {
                playerNameLists.Add(connectionPayload.playerName);
                FindObjectOfType<PlayerCredits>().GetCreditsFirstPlay(playerNameInputField.text);
            }
        }
        
        bool createPlayerObject = true;

        Quaternion spawnRot = Quaternion.identity;
        Vector3 spawnPos = Vector3.zero;
        int randomRotationNum = Random.Range(0,360);
        
        spawnPos = SpawnPointsList[UnityEngine.Random.Range(0, SpawnPointsList.Length)].position;
        spawnRot = Quaternion.Euler(0f,randomRotationNum,0f);

        Debug.Log(spawnPos);

        callback(createPlayerObject, null, approveConnection, spawnPos, spawnRot);
    }

    public void Client()
    {
        var payload = JsonUtility.ToJson(new ConnectionPayload()
        {
            inputPassword = passwordInputField.text,
            playerName = playerNameInputField.text
        });

        byte[] payloadBytes = Encoding.ASCII.GetBytes(payload);

        NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;
        NetworkManager.Singleton.StartClient(); 
        passwordText.text = password;
        pauseManager.IsIngame = true;   
    }

    private void CreatePassword()
    {
        for (int randNum = 0; randNum < 5; randNum++)
        {
            int randomPassword = Random.Range(0,9);
            password = password + randomPassword.ToString();
        }
        passwordText.text = password;
    }
}
