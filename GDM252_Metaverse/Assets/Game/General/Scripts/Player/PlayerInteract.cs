using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class PlayerInteract : NetworkBehaviour
{
    public GameObject bingoGameUIPrefabs;
    GameObject bingoGameUI;
    private bool isBingoGame;
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
                isIngame = true;
                FindObjectOfType<PauseManager>().DisablePlayerMovement();
                Cursor.lockState = CursorLockMode.None;
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
            ChangePrefabsLayerInHierarchy();
            Debug.Log("EnterBingoGame");
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "BingoGame")
        {
            isBingoGame = true;
            Debug.Log("EnterBingoZone");
        }
    }

    public void ExitMiniGame(string minigame)
    {
        switch (minigame)
        {
            case "BingoGame":
            Destroy(bingoGameUI);
            isIngame = false;
            Cursor.lockState = CursorLockMode.Locked;
            FindObjectOfType<PauseManager>().EnablePlayerMovement();
            break;
        }
    }

    private void ChangePrefabsLayerInHierarchy()
    {
       if (m_IndexNumber <= bingoGameUI.transform.GetSiblingIndex())
            {
                m_IndexNumber = 0;
            }
    }
}
