using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject MenuPanel;

    public bool IsIngame = false;
    public bool MenuIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsIngame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(MenuIsOpen)
                {
                    CloseMenuPanel();
                }
                else
                {
                    OpenMenuPanel();
                }
            }
        }
    }

    void CloseMenuPanel()
    {
        MenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        MenuIsOpen = false;
    }

    void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        MenuIsOpen = true;
    }
}
