using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MouseLook : NetworkBehaviour
{
    private float mouseX;
    private float mouseY;
    public float mouseSensitivity = 100f;
    
    public Transform playerBody;

    private PauseManager pauseManager;

    float xRotation = 0f;

    void Start()
    {
        if(!IsOwner)
        {
            DestroyCamera();
        }
        else
        {
            pauseManager = FindObjectOfType<PauseManager>();
            HideCursor();
        }
    }

    private void DestroyCamera()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(IsClient && IsOwner)
        {
            if(!pauseManager.IsMenuOpen)
            {
                GetInput();
                RotateCamera();
                RotatePlayer();
            }
        }
    }

    private void GetInput()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    private void RotateCamera()
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void RotatePlayer()
    {
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
