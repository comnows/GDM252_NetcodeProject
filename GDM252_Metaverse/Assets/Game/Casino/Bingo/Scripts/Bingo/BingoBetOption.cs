using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BingoBetOption : MonoBehaviour
{
    public int randomTimeLeft;
    public GameObject startBingoGameUI;
    public GameObject inBingoGameUI;
    public GameObject backButton;
    public GameObject winLabel;
    private PlayerCredits credit;
    private PlayerInteract [] playerInteracts;
    private PlayerInteract playerInteract;

    void Start()
    {
        credit = GameObject.FindObjectOfType<PlayerCredits>();
        FindPlayerInteractScript();
        randomTimeLeft = 0;
    }

    public void firstRandomOption()
    {
        randomTimeLeft = 20;
        StartBingoGame(randomTimeLeft);
    }

    public void secondRandomOption()
    {
        randomTimeLeft = 30;
        StartBingoGame(randomTimeLeft);

    }

    public void thirdRandomOption()
    {
        randomTimeLeft = 40;
        StartBingoGame(randomTimeLeft);
    }

    public void fouthRandomOption()
    {
        randomTimeLeft = 50;
        StartBingoGame(randomTimeLeft);
    }

    public void fifthRandomOption()
    {
        randomTimeLeft = 70;
        StartBingoGame(randomTimeLeft);
    }

    private void StartBingoGame(int randTimeLeft)
    {
        if (credit.playerCredit > 100)
        {
            credit.RemoveBalance(100);
            UnableStartGameUI();
            FindObjectOfType<TableNumber>().RandomNumberForTable();
            FindObjectOfType<TableNumber>().GetNumberForTable();
            FindObjectOfType<BingoCondition>().GetRandomTimes(randTimeLeft);
            FindObjectOfType<RandomBingo>().EnableRandomNumber();
        }
    }

    private void UnableStartGameUI()
    {
        startBingoGameUI.SetActive(false);
        inBingoGameUI.SetActive(true);
    }

    private void EnableStartGameUI()
    {
        startBingoGameUI.SetActive(true);
        inBingoGameUI.SetActive(false);
    }

    public void BackButton()
    {
        winLabel.SetActive(false);
        EnableStartGameUI();
        ResetBingoGame();
        backButton.SetActive(false);
    }

    public void LeaveBingoGameButton()
    {   
        playerInteract.ExitMiniGame("BingoGame");
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

    public void GetPrize()
    {
        switch (randomTimeLeft)
        {
            case 20:
            credit.AddBalance(20000);
            break;

            case 30:
            credit.AddBalance(10000);
            break;

            case 40:
            credit.AddBalance(5000);
            break;

            case 50:
            credit.AddBalance(1000);
            break;

            case 70:
            credit.AddBalance(300);
            break;
        }
    }

    public void ResetBingoGame()
    {
        FindObjectOfType<TableNumber>().ClearTable();
    }
}
