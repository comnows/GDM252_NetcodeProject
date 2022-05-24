using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class PlayerInteract : NetworkBehaviour
{
    public GameObject bingoGameUIPrefabs;
    public GameObject slotMachineGameUIPrefabs;
    public GameObject airHockeyCameraPrefabs;
    public GameObject airHockeyScoreLabel;
    public GameObject playerCamera;
    GameObject airHockeyZone;
    GameObject airHockeyCamera;
    GameObject bingoGameUI;
    GameObject slotMachineGameUI;
    private bool isBingoGame;
    private bool isSlotMachineGame;
    public bool isAirHockeyGame;
    public bool isIngame;
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
        airHockeyZone = GameObject.FindGameObjectWithTag("AirHockeyGame");
        airHockeyScoreLabel = GameObject.FindGameObjectWithTag("HockeyScoreText");
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

            else if (Input.GetKeyDown(KeyCode.E) && isAirHockeyGame)
            {
                InteractGame("EnterAirHockeyGame");
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

            case "EnterAirHockeyGame":
            playerCamera.SetActive(false);
            airHockeyCamera = Instantiate(airHockeyCameraPrefabs, new Vector3(82.43f,8,0.2f), airHockeyCameraPrefabs.transform.rotation) as GameObject;
            airHockeyZone.SetActive(false);
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag)
        {
            case "BingoGame":
            isBingoGame = true;
            Debug.Log("EnterBingoZone");
            break;

            case "SlotMachineGame":
            isSlotMachineGame = true;
            Debug.Log("EnterSlotMachineZone");
            break;

            case "AirHockeyGame":
            isAirHockeyGame = true;
            Debug.Log("EnterAirHockeyZone");
            break;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "BingoGame":
            isBingoGame = false;
            break;

            case "SlotMachineGame":
            isSlotMachineGame = false;
            break;

            case "AirHockeyGame":
            isAirHockeyGame = false;
            break;
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

                case "AirHockeyGame":
                Destroy(airHockeyCamera);
                playerCamera.SetActive(true);
                airHockeyZone.SetActive(true);
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
        if(IsOwner)
        {
        isIngame = true;
        FindObjectOfType<PauseManager>().DisablePlayerMovement();
        Cursor.lockState = CursorLockMode.None;
        }
    }

     private void EnblePlayerMovement()
    {
        if(IsOwner)
        {
        isIngame = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<PauseManager>().EnablePlayerMovement();
        }
    }
}
