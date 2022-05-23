using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotResult : MonoBehaviour
{
    [HideInInspector]public Sprite[] fruitResults; 

    private string fruitName;

    public event Action<int> onPrizeMultiplierChange;
    public int prizeMultiplier = 0;

    public void CheckResultDuplicate()
    {
        if(fruitResults[0].name == fruitResults[1].name && fruitResults[0].name == fruitResults[2].name)
        {
            fruitName = fruitResults[0].name;
            prizeMultiplier = GetDuplicateAllPrizeMultiplier(fruitName);
        }
        else if(fruitResults[0].name == fruitResults[1].name || fruitResults[0].name == fruitResults[2].name)
        {
            fruitName = fruitResults[0].name;
            prizeMultiplier = GetDuplicatePrizeMultiplier(fruitName);
        }
        else if(fruitResults[1].name == fruitResults[2].name)
        {
            fruitName = fruitResults[1].name;
            prizeMultiplier = GetDuplicatePrizeMultiplier(fruitName);
        }
        else
        {
            prizeMultiplier = 0;
        }

        if(prizeMultiplier == 0)
        {
            prizeMultiplier = GetNoDuplicatePrizeMultiplier();
        }

        Debug.Log(prizeMultiplier);
        onPrizeMultiplierChange?.Invoke(prizeMultiplier);
    }

    public int GetDuplicateAllPrizeMultiplier(string name)
    {
        int multiplier = 0;

        switch(name)
        {
            case "banana":
                multiplier = 200;
                break;
            case "cherry":
                multiplier = 10;
                break;
            case "lemon":
                multiplier = 30;
                break;
            case "orange":
                multiplier = 120;
                break;
            case "pear":
                multiplier = 80;
                break;
            case "strawberry":
                multiplier = 500;
                break;
        }

        return multiplier;
    }

    public int GetDuplicatePrizeMultiplier(string name)
    {
        int multiplier = 0;

        switch(name)
        {
            case "banana":
                multiplier = 0;
                break;
            case "cherry":
                multiplier = 0;
                break;
            case "lemon":
                multiplier = 0;
                break;
            case "orange":
                multiplier = 0;
                break;
            case "pear":
                multiplier = 0;
                break;
            case "strawberry":
                multiplier = 5;
                break;
        }

        return multiplier;
    }

    public int GetNoDuplicatePrizeMultiplier()
    {
        int multiplier = 0;

        if(fruitResults[0].name == "strawberry" || fruitResults[1].name == "strawberry" || fruitResults[2].name == "strawberry")
        {
            multiplier = 3;
        }

        return multiplier;
    }

    public int GetResultCheck(int rollTimes, int startIndex)
    {
        int resultIndex = 0;

        switch((rollTimes - startIndex) % 5)
        {
            case 0:
                resultIndex = 0;
                break;
            case 1:
                resultIndex = 4;
                break;
            case 2:
                resultIndex = 3;
                break;
            case 3:
                resultIndex = 2;
                break;
            case 4:
                resultIndex = 1;
                break;
        }

        return resultIndex;
    }
}
