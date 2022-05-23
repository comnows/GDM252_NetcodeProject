using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCredits : MonoBehaviour
{
    private float playerCredit;
    private string playerName;
    private Text playerCreditsLabel;

    void Start()
    {
        playerCredit = 1000;
        playerName = "";
        playerCreditsLabel = GameObject.Find ("Canvas/playerCreditText").GetComponent<Text>();
    }

    public void GetCreditsFirstPlay(string playerNames)
    {
        playerName = playerNames;
        if (PlayerPrefs.HasKey(playerName + "_Credits"))
        {
           playerCredit = PlayerPrefs.GetFloat(playerName + "_Credits");
        }
        else
        {
            PlayerPrefs.SetFloat(playerName + "_Credits", playerCredit);
            PlayerPrefs.Save();
        }
        playerCreditsLabel.text = playerCredit.ToString() + " Credits";
    }

    public void UpdatePlayerCredits(string action, float amount)
    {
        switch (action)
        {
            case "receive":
            playerCredit += amount;
            break;
            
            case "given":
            playerCredit -= amount;
            break;
        }
        playerCreditsLabel.text = playerCredit.ToString() + " Credits";
        SavePlayerCreditsData();
    }
    
    public void SavePlayerCreditsData()
    {
        PlayerPrefs.DeleteKey(playerName + "_Credits");
        PlayerPrefs.SetFloat(playerName + "_Credits", playerCredit);
        PlayerPrefs.Save();
    }
}
