using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class PlayerMovement : NetworkedBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 5;
    [SerializeField] private float sensitivity = 5;
    [SerializeField] private Transform cameraTransform;

    private float pitch =0;
    void Start()
    {
        if (!IsLocalPlayer)
        {
            cameraTransform.GetComponent<Camera>().enabled = false;
            cameraTransform.GetComponent<AudioListener>().enabled = false;
        }
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if(!IsLocalPlayer) return;
        
        MovePlayer();
        Look();
        
    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = Vector3.ClampMagnitude(move, 1);

        move = transform.TransformDirection(move);
        
        controller.SimpleMove(move * speed);
    }

    void Look()
    {
        float mousex = Input.GetAxis("Mouse X") * sensitivity;
        
        transform.Rotate(0,mousex,0);

        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -45, 45);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
