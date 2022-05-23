using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMultiplierResult : MonoBehaviour
{
    private SlotResult slotResult;
    public Text multiplierText;

    // Start is called before the first frame update
    void Start()
    {
        slotResult = GameObject.FindObjectOfType<SlotResult>();

        slotResult.onPrizeMultiplierChange += ShowMultiplierText;
    }

    private void ShowMultiplierText(int value)
    {
        multiplierText.text = "x" + $"{value}";
    }

    public void ClearText()
    {
        multiplierText.text = "";
    }
}
