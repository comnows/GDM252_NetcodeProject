using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetSystem : MonoBehaviour
{
    public InputField BetInputfield;
    public Button allInButton;
    public Button halfBetButton;
    public Button leaveButton;

    private PlayerCredits credit;
    private SlotResult slotResult; 
    
    void Start()
    {
        credit = GameObject.FindObjectOfType<PlayerCredits>();
        slotResult = GameObject.FindObjectOfType<SlotResult>();

        slotResult.onPrizeMultiplierChange += BetResult;

        SetZeroWhenBlank();
    }

    public void AllInBet()
    {
        BetInputfield.text = $"{credit.playerCredit}";
    }

    public void HalfBet()
    {
        BetInputfield.text = $"{credit.playerCredit / 2}";
    }

    public void SetZeroWhenBlank()
    {
        if(string.IsNullOrEmpty(BetInputfield.text))
        {
            BetInputfield.text = "0";
        }
    }

    public void SetZeroWhenBelowZero()
    {
        if(int.Parse(BetInputfield.text) < 0)
        {
            BetInputfield.text = "0";
        }
    }

    public void SetMaxBetWhenOverBalance()
    {
        if(int.Parse(BetInputfield.text) > credit.playerCredit)
        {
            BetInputfield.text = $"{credit.playerCredit}";
        }
    }

    private void BetResult(int multiplier)
    {
        int prize = int.Parse(BetInputfield.text) * multiplier;

        credit.AddBalance(prize);
    }

    public int GetBetValue()
    {
        int bet = int.Parse(BetInputfield.text);
        return bet;
    }

    public void DisableInput()
    {
        BetInputfield.interactable = false;
        allInButton.interactable = false;
        halfBetButton.interactable = false;
        leaveButton.interactable = false;
    }

    public void EnableInput()
    {
        BetInputfield.interactable = true;
        allInButton.interactable = true;
        halfBetButton.interactable = true;
        leaveButton.interactable = true;
    }
}
