using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject MenuPanel;

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
        IsMenuOpen = false;
    }

    void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        IsMenuOpen = true;
    }
}
