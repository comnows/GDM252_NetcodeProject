using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject MenuPanel;
    private GameObject player; 
    public bool IsIngame = false;
    public bool IsMenuOpen = false;

    void Update()
    {
        if(!IsIngame)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsMenuOpen)
            {
                CloseMenuPanel();
            }
            else
            {
                OpenMenuPanel();
            }
        }
    }

    void CloseMenuPanel()
    {
        MenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        EnablePlayerMovement();
        IsMenuOpen = false;
    }

    void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        DisablePlayerMovement();
        IsMenuOpen = true;
    }

    public void DisablePlayerMovement()
    {
        player = GameObject.Find("Player(Clone)");
        if (player.GetComponent<PlayerMovement>().IsOwner)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void EnablePlayerMovement()
    {
        player = GameObject.Find("Player(Clone)");
        if (player.GetComponent<PlayerMovement>().IsOwner)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
