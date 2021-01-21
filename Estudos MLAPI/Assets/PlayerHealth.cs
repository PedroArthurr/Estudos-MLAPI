using System;
using System.Collections;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealth : NetworkedBehaviour
{
    public NetworkedVarFloat health = new NetworkedVarFloat(100);
    private CharacterController controller;
    private MeshRenderer[] renderers;
    //Rodando no server
    //Invocado por client, roda no server


    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(float _damage)
    {
        health.Value -= _damage;
        
        if (health.Value <= 0)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 4,Random.Range(-10,10));

            InvokeClientRpcOnEveryone(ClientRespawn,pos);
            health.Value = 100;
        }
    }
    
    [ClientRPC]
    void ClientRespawn(Vector3 _position)
    {
        StartCoroutine(Respawn(_position));
    }

    IEnumerator Respawn(Vector3 _pos)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        yield return new WaitForSeconds(1);
        
        controller.enabled = false;
        transform.position = _pos;
        controller.enabled = true;
        
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }


        
    }
}