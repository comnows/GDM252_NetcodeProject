using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class RandomBingo : MonoBehaviour
{
    private TableNumber tableNumber;
    private int length = 25;
    public NetworkVariable<int> randNumber = new NetworkVariable<int>();  
    private List<int> bingoRandNumList = new List<int>();
    private List<Text> numberCellList = new List<Text>();
    public Text randomNumberText;
    IEnumerator Start() 
    {
        tableNumber = GetComponent<TableNumber>();
        numberCellList = tableNumber.numberCellList;
        yield return new WaitForEndOfFrame();
    }
    
    public void EnableRandomNumber()
    {
        StartCoroutine(StartRandomNumber());
    }

    IEnumerator StartRandomNumber()
    {
        yield return new WaitForSeconds(1);
        int randNum = Random.Range(1,99);
 
            while (bingoRandNumList.Contains(randNum))
            {
                randNum = Random.Range(1,99);
            }
            bingoRandNumList.Add(randNum);
            randomNumberText.text = randNum.ToString();
            for (int i = 0; i < length; i++)
            {
                if (numberCellList[i].text == randNum.ToString())
                {
                    numberCellList[i].GetComponent<Text>().color = Color.red;
                }
            }
        FindObjectOfType<BingoCondition>().BingoCheck();
    }
}
