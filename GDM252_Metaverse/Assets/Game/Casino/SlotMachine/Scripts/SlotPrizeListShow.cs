using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPrizeListShow : MonoBehaviour
{
    public GameObject prizeListImg;
    private PlayerInteract [] playerInteracts;
    private PlayerInteract playerInteract;
    private void Start() 
    {
        FindPlayerInteractScript();    
    }
    public void ShowPrizeList()
    {
        if (prizeListImg.activeSelf == false)
        {
            prizeListImg.SetActive(true);
        }
        else
        {
            prizeListImg.SetActive(false);
        }
    }

      public void FindPlayerInteractScript() {
        playerInteracts = GameObject.FindObjectsOfType<PlayerInteract>();
        foreach (PlayerInteract n in playerInteracts)
        {
            if (n.IsOwner)
            {
                playerInteract = n;
            }
        } 
    }

    public void LeaveGame()
    {
        playerInteract.ExitMiniGame("SlotMachineGame");
    }
}
