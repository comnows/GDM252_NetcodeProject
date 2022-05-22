using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasinoWallet : MonoBehaviour
{
    public Text balanceText;

    public event Action<int> OnBalanceChange;
    public int balance = 1000;

    
    void Start()
    {
        OnBalanceChange += UpdateBalanceText;

        OnBalanceChange?.Invoke(balance);
    }

    public void UpdateBalanceText(int newBalance)
    {
        balanceText.text = newBalance.ToString();
    }

    public void AddFunds()
    {
        AddBalance(1000);
    }

    public void AddBalance(int amount)
    {
        balance += amount;
        OnBalanceChange?.Invoke(balance);
    }

    public void RemoveBalance(int amount)
    {
        balance -= amount;
        OnBalanceChange?.Invoke(balance);
    }
}
