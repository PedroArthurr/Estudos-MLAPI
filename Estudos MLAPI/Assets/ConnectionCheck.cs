using MLAPI;
using MLAPI.Spawning;
using UnityEngine;

public class ConnectionCheck : MonoBehaviour
{
    private void Start() 
    {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkingManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;

        string password = System.Text.Encoding.ASCII.GetString(connectionData);
        if (password == "senha")
        {
            approve = true;
        }

        Debug.Log($"Aprovação: {approve}");
        callback(true, null,approve, new Vector3(0,10,0),Quaternion.identity);
    }
}