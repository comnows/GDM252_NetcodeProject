using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class PlayerInteract : NetworkBehaviour
{
    public GameObject bingoGameUIPrefabs;
    public GameObject slotMachineGameUIPrefabs;
    GameObject bingoGameUI;
    GameObject slotMachineGameUI;
    private bool isBingoGame;
    private bool isSlotMachineGame;
    private bool isIngame;
    int m_IndexNumber;
    void Update()
    {
        if(IsClient && IsOwner)
        {
            
            GetInput();
        }
    }
    private void Start() 
    {
        isBingoGame = false;
        isSlotMachineGame = false;
        isIngame = false;
        m_IndexNumber = 0;
    }

    private void GetInput()
    {   
        if (!isIngame)
        {
            if (Input.GetKeyDown(KeyCode.E) && isBingoGame)
            {
                InteractGame("EnterBingoGame");
                DisablePlayerMovement();
            }

            else if (Input.GetKeyDown(KeyCode.E) && isSlotMachineGame)
            {
                InteractGame("EnterSlotMachineGame");
                DisablePlayerMovement();
            }
        }
    }

    private void InteractGame(string activities)
    {
        switch (activities)
        {
            case "EnterBingoGame":
            bingoGameUI = Instantiate(bingoGameUIPrefabs, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            bingoGameUI.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
            bingoGameUI.transform.SetSiblingIndex(m_IndexNumber);
            ChangePrefabsLayerInHierarchy(bingoGameUI);
            break;

            case "EnterSlotMachineGame":
            slotMachineGameUI = Instantiate(slotMachineGameUIPrefabs, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            slotMachineGameUI.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
            slotMachineGameUI.transform.SetSiblingIndex(m_IndexNumber);
            ChangePrefabsLayerInHierarchy(slotMachineGameUI);
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "BingoGame")
        {
            isBingoGame = true;
            Debug.Log("EnterBingoZone");
        }
        else if (other.gameObject.tag == "SlotMachineGame")
        {
            isSlotMachineGame = true;
            Debug.Log("EnterSlotMachineZone");
        }
    }

    private void OnTriggerExit(Collider other) 
    {
         if (other.gameObject.tag == "BingoGame")
        {
            isBingoGame = false;
        }
        else if (other.gameObject.tag == "SlotMachineGame")
        {
            isSlotMachineGame = false;
        }
    }

    public void ExitMiniGame(string minigame)
    {
        switch (minigame)
        {
            case "BingoGame":
            Destroy(bingoGameUI);
            break;
            case "SlotMachineGame":
            Destroy(slotMachineGameUI);
            break;
        }    
        EnblePlayerMovement();
    }

    private void ChangePrefabsLayerInHierarchy(GameObject gameUI)
    {
       if (m_IndexNumber <= gameUI.transform.GetSiblingIndex())
            {
                m_IndexNumber = 0;
            }
    }

    private void DisablePlayerMovement()
    {
        isIngame = true;
        FindObjectOfType<PauseManager>().DisablePlayerMovement();
        Cursor.lockState = CursorLockMode.None;
    }

     private void EnblePlayerMovement()
    {
        isIngame = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<PauseManager>().EnablePlayerMovement();
    }
}
