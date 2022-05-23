using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetSystem : MonoBehaviour
{
    public InputField BetInputfield;
    public Button allInButton;
    public Button halfBetButton;

    private CasinoWallet casinoWallet;
    private SlotResult slotResult; 
    
    void Start()
    {
        casinoWallet = GameObject.FindObjectOfType<CasinoWallet>();
        slotResult = GameObject.FindObjectOfType<SlotResult>();

        slotResult.onPrizeMultiplierChange += BetResult;

        SetZeroWhenBlank();
    }

    public void AllInBet()
    {
        BetInputfield.text = $"{casinoWallet.balance}";
    }

    public void HalfBet()
    {
        BetInputfield.text = $"{casinoWallet.balance / 2}";
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
        if(int.Parse(BetInputfield.text) > casinoWallet.balance)
        {
            BetInputfield.text = $"{casinoWallet.balance}";
        }
    }

    private void BetResult(int multiplier)
    {
        int prize = int.Parse(BetInputfield.text) * multiplier;

        casinoWallet.AddBalance(prize);
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
    }

    public void EnableInput()
    {
        BetInputfield.interactable = true;
        allInButton.interactable = true;
        halfBetButton.interactable = true;
    }
}
