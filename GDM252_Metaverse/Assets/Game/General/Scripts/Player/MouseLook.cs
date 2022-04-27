using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    
    public Transform playerBody;

    private PauseManager pauseManager;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        else
        {
            pauseManager = FindObjectOfType<PauseManager>();
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsClient && IsOwner)
        {
            if(!pauseManager.MenuIsOpen)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
