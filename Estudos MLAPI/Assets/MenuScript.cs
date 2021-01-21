using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Transports.UNET;
using TMPro;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private TMP_InputField inputField;
    public void Host()
    {
        NetworkingManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }

    public void Join()
    {
        NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress =
            inputField.text.Length <= 0 ? "127.0.0.1" : inputField.text;
        
        
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("senha");
        NetworkingManager.Singleton.StartClient();
        
        menuPanel.SetActive(false);
        
        
    }

    public void Server()
    {
        NetworkingManager.Singleton.StartServer();
        menuPanel.SetActive(false);
    }
}
