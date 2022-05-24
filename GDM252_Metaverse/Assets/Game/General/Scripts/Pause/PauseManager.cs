using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject MenuPanel;
    private PlayerMovement [] players;
    private PlayerMovement player; 
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

    public void FindMovementScript() {
        players = GameObject.FindObjectsOfType<PlayerMovement>();
        foreach (PlayerMovement n in players)
        {
            if (n.IsOwner)
            {
                player = n;
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
        if (player == null)
        {
        FindMovementScript();
        }
        player.enabled = false;
    }

    public void EnablePlayerMovement()
    {
        if (player == null)
        {
        FindMovementScript();
        }
        player.enabled = true;
    }
}
