using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;
using MLAPI.NetworkedVar;
public class PlayerShooting : NetworkedBehaviour
{
    [SerializeField] private ParticleSystem bulletParticleSystem;
    private ParticleSystem.EmissionModule em;

    private float fireRate = 10;
    private float shootTimer = 0;
    

    private NetworkedVarBool shooting = new NetworkedVarBool(new NetworkedVarSettings{WritePermission = NetworkedVarPermission.OwnerOnly}, false);
    private void Start()
    {
        em = bulletParticleSystem.emission;
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            shooting.Value = Input.GetMouseButton(0);
            shootTimer += Time.deltaTime;
            if (shooting.Value && shootTimer >= 1 / fireRate)
            {
                shootTimer = 0;
                InvokeServerRpc(Shoot);//
            }
        }
        em.rateOverTime = shooting.Value ? fireRate : 0;
    }
    
    [ServerRPC]//Client -->> To run in server
    void Shoot()
    {
        Ray ray = new Ray(bulletParticleSystem.transform.position, bulletParticleSystem.transform.forward);
        
        if(Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
            if (player)
            {
                //hit
                player.TakeDamage(10);
            }
            
        }
    }
}
