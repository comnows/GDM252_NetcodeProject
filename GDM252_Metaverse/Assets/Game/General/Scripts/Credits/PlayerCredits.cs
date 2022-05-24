using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCredits : MonoBehaviour
{
    public int playerCredit;
    private string playerName;
    private Text playerCreditsLabel;
    public event Action<int> OnBalanceChange;


    void Start()
    {
        playerCredit = 1000;
        playerName = "";
        playerCreditsLabel = GameObject.Find ("Canvas/playerCreditText").GetComponent<Text>();
        OnBalanceChange += UpdateBalanceText;
        //OnBalanceChange?.Invoke(playerCredit);
    }

    public void GetCreditsFirstPlay(string playerNames)
    {
        playerName = playerNames;
        if (PlayerPrefs.HasKey(playerName + "_Credits"))
        {
           playerCredit = PlayerPrefs.GetInt(playerName + "_Credits");
        }
        else
        {
            PlayerPrefs.SetInt(playerName + "_Credits", playerCredit);
            PlayerPrefs.Save();
        }
        OnBalanceChange?.Invoke(playerCredit);
    }

    public void UpdateBalanceText(int newBalance)
    {
        playerCreditsLabel.text = newBalance.ToString() + " Credits";
        SavePlayerCreditsData();
    }

    public void AddBalance(int amount)
    {
        playerCredit += amount;
        OnBalanceChange?.Invoke(playerCredit);
    }

    public void RemoveBalance(int amount)
    {
        playerCredit -= amount;
        OnBalanceChange?.Invoke(playerCredit);
    }

    public void SavePlayerCreditsData()
    {
        PlayerPrefs.DeleteKey(playerName + "_Credits");
        PlayerPrefs.SetInt(playerName + "_Credits", playerCredit);
        PlayerPrefs.Save();
    }
    
}
