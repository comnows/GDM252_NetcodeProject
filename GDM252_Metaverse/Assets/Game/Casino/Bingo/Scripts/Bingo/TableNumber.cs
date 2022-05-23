using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TableNumber : MonoBehaviour
{
    public List<Text> numberCellList = new List<Text>();
    private List<int> randNumList = new List<int>();
    private List<int> bingoRandNumList = new List<int>();
    private int length = 25;
    private bool isRowBingo;
    private bool isColumnBingo;
    private bool isDiagonalBingo;
    
    public void RandomNumberForTable()
    {
        randNumList = new List<int>(new int[length]);
        for (int i = 0; i < length; i++)
        {
            int randNum = Random.Range(1,99);
 
            while (randNumList.Contains(randNum))
            {
                randNum = Random.Range(1,99);
            }
            
            randNumList[i] = randNum;
        }
    }

    public void GetNumberForTable()
    {
        for (int i = 0; i < length; i++)
        {
            numberCellList[i].text = randNumList[i].ToString();
        }
    }

    public void ClearTable()
    {
        for (int i = 0; i < length; i++)
        {
            numberCellList[i].GetComponent<Text>().color = Color.black;
            numberCellList[i].text = " ";
        }
    }
}
