using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRoll : MonoBehaviour
{
    private PlayerCredits credit;
    private BetSystem betSystem;
    private SlotRandomSprite slotRandomSprite;
    private SlotResult slotResult;
    private UIMultiplierResult uIMultiplierResult;
    public GameObject[] SlotRows;

    private int timesToChangePicture = 4;

    public int firstSlotRollTimes;
    public int secondSlotRollTimes;
    public int thirdSlotRollTimes;

    public int firstSlotStartIndex = 5;
    public int secondSlotStartIndex = 5;
    public int thirdSlotStartIndex = 5;

    public float timeInterval = 0.025f;

    private bool isRoll = false;

    // Start is called before the first frame update
    void Start()
    {
        SetScriptComponents();
    }

    private void SetScriptComponents()
    {
        credit = GameObject.FindObjectOfType<PlayerCredits>();
        betSystem = GameObject.FindObjectOfType<BetSystem>();
        slotRandomSprite = GetComponent<SlotRandomSprite>();
        slotResult = GetComponent<SlotResult>();
        uIMultiplierResult = GameObject.FindObjectOfType<UIMultiplierResult>();
    }
    
    private void RandomRollTimes()
    {
        firstSlotRollTimes = Random.Range(20, 30);
        secondSlotRollTimes = firstSlotRollTimes + Random.Range(5, 15);
        thirdSlotRollTimes = secondSlotRollTimes + Random.Range(5, 15);
    }

    private IEnumerator RollSlot1()
    {

        for(int rollIndex = 0; rollIndex < firstSlotRollTimes * timesToChangePicture; rollIndex++)
        {
            SlotRows[0].transform.localPosition -= new Vector3(0f, 75f, 0f);

            if(SlotRows[0].transform.localPosition.y < 50f)
            {
                SlotRows[0].transform.localPosition += new Vector3(0f, 1500f, 0f);
            }

            if(rollIndex % 4 == 0)
            {
                slotRandomSprite.showFruit(0);
            }
            
            yield return new WaitForSeconds(timeInterval);
        }
        
        int imageIndex = slotResult.GetResultCheck(firstSlotRollTimes, firstSlotStartIndex);
        slotResult.fruitResults[0] = slotRandomSprite.slots[0].imageSlots[imageIndex].sprite;
        
        firstSlotStartIndex = imageIndex;
        Debug.Log(slotResult.fruitResults[0].name);
    }

    private IEnumerator RollSlot2()
    {

        for(int rollIndex = 0; rollIndex < secondSlotRollTimes * timesToChangePicture; rollIndex++)
        {
            SlotRows[1].transform.localPosition -= new Vector3(0f, 75f, 0f);

            if(SlotRows[1].transform.localPosition.y < 50f)
            {
                SlotRows[1].transform.localPosition += new Vector3(0f, 1500f, 0f);
            }

            if(rollIndex % 4 == 0)
            {
                slotRandomSprite.showFruit(1);
            }
            
            yield return new WaitForSeconds(timeInterval);
        }

        int imageIndex = slotResult.GetResultCheck(secondSlotRollTimes, secondSlotStartIndex);
        slotResult.fruitResults[1] = slotRandomSprite.slots[1].imageSlots[imageIndex].sprite;

        secondSlotStartIndex = imageIndex;
        Debug.Log(slotResult.fruitResults[1].name);
    }

    private IEnumerator RollSlot3()
    {

        for(int rollIndex = 0; rollIndex < thirdSlotRollTimes * timesToChangePicture; rollIndex++)
        {
            SlotRows[2].transform.localPosition -= new Vector3(0f, 75f, 0f);

            if(SlotRows[2].transform.localPosition.y < 50f)
            {
                SlotRows[2].transform.localPosition += new Vector3(0f, 1500f, 0f);
            }

            if(rollIndex % 4 == 0)
            {
                slotRandomSprite.showFruit(2);
            }
            
            yield return new WaitForSeconds(timeInterval);
        }

        int imageIndex = slotResult.GetResultCheck(thirdSlotRollTimes, thirdSlotStartIndex);
        slotResult.fruitResults[2] = slotRandomSprite.slots[2].imageSlots[imageIndex].sprite;

        thirdSlotStartIndex = imageIndex;
        Debug.Log(slotResult.fruitResults[2].name);

        slotResult.CheckResultDuplicate();

        isRoll = false;

        betSystem.EnableInput();
    }

    public void StartRoll()
    {
        if(int.Parse(betSystem.BetInputfield.text) <= credit.playerCredit && int.Parse(betSystem.BetInputfield.text) != 0)
        {
            if(!isRoll)
            {
                isRoll = true;
                betSystem.DisableInput();
                credit.RemoveBalance(betSystem.GetBetValue());
                uIMultiplierResult.ClearText();
                RandomRollTimes();
                RollSlots();
            }
        }
    }

    private void RollSlots()
    {
        StartCoroutine(RollSlot1());
        StartCoroutine(RollSlot2());
        StartCoroutine(RollSlot3());
    }
}
