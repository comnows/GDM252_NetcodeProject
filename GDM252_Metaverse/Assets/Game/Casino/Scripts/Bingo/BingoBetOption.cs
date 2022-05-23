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
    void Start()
    {
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
        FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("given",100);
        UnableStartGameUI();
        FindObjectOfType<TableNumber>().RandomNumberForTable();
        FindObjectOfType<TableNumber>().GetNumberForTable();
        FindObjectOfType<BingoCondition>().GetRandomTimes(randTimeLeft);
        FindObjectOfType<RandomBingo>().EnableRandomNumber();
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
        FindObjectOfType<PlayerInteract>().ExitMiniGame("BingoGame");
    }

    public void GetPrize()
    {
        switch (randomTimeLeft)
        {
            case 20:
            FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("receive",10000);
            break;

            case 30:
            FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("receive",5000);
            break;

            case 40:
            FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("receive",2000);
            break;

            case 50:
            FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("receive",700);
            break;

            case 70:
            FindObjectOfType<PlayerCredits>().UpdatePlayerCredits("receive",300);
            break;
        }
    }

    public void ResetBingoGame()
    {
        FindObjectOfType<TableNumber>().ClearTable();
    }

}
