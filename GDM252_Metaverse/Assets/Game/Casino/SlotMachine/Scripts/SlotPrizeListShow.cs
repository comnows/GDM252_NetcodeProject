using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPrizeListShow : MonoBehaviour
{
    public GameObject prizeListImg;

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

    public void LeaveGame()
    {
        FindObjectOfType<PlayerInteract>().ExitMiniGame("SlotMachineGame");
    }
}
