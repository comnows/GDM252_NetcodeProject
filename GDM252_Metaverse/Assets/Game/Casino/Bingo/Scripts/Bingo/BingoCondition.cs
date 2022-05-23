using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BingoCondition : MonoBehaviour
{
    private TableNumber tableNumber;
    private List<Text> numberCellList = new List<Text>();
    public bool isBingo;
    public bool isRowBingo;
    public bool isColumnBingo;
    public bool isDiagonalBingo;
    private int numberOfRowColumn = 5;
    private int randomTimeLeft;
    public GameObject backButton;
    public GameObject winTextLabel;
    public Text winText;
    public Text randomTimeLeftText;
    IEnumerator Start() 
    {
        isBingo = false;
        isRowBingo = false;
        isColumnBingo = false;
        isDiagonalBingo = false;
        randomTimeLeft = 0;
        tableNumber = GetComponent<TableNumber>();
        numberCellList = tableNumber.numberCellList;
        yield return new WaitForEndOfFrame();
    }

    public void GetRandomTimes(int randTimes)
    {
        randomTimeLeft = randTimes;
        randomTimeLeftText.text = randomTimeLeft.ToString();
    }

    public void BingoCheck()
    {
            for (int i = 0; i < numberCellList.Count; i+=5 )
            {
                if (!isRowBingo)
                {
                    CheckBingoRow(i,i+4);
                }
            }

            for (int i = 0; i < numberOfRowColumn; i++)
            {   
                if (!isColumnBingo)
                {
                CheckBingoColumn(i,i+20);
                }
            }

            CheckBingoLeftDiagonal(0,20);

            if (!isDiagonalBingo)
            {
                CheckBingoRightDiagonal(4,24);
            }
        
        if (isRowBingo || isColumnBingo || isDiagonalBingo)
        {
            Debug.Log("CheckBingo");
            EnableBackButton("You Win");
            FindObjectOfType<BingoBetOption>().GetPrize();
        }
        else if (!isRowBingo && !isColumnBingo && !isDiagonalBingo)
        {
            randomTimeLeft -= 1;
            randomTimeLeftText.text = randomTimeLeft.ToString();
            if (randomTimeLeft > 0)
            {
                FindObjectOfType<RandomBingo>().EnableRandomNumber();
            }
            else if(randomTimeLeft == 0)
            {
                Debug.Log("You Lose");
                EnableBackButton("You Lose");
            }
        }
    }

    private void CheckBingoRow(int firstCellInRow, int lastCellInRow)
    {
        bool isRowBingoAreTrue = true;  
        for (int i = firstCellInRow; i < lastCellInRow + 1; i++)
        {
            bool isNumberCellRed = false;
            if (numberCellList[i].GetComponent<Text>().color == Color.red)
            {
                isNumberCellRed = true;
            }
            isRowBingoAreTrue = isRowBingoAreTrue & isNumberCellRed; 
        }
        isBingo = isRowBingoAreTrue;
        isRowBingo = isBingo;
    }

    private void CheckBingoColumn(int firstCellInColumn, int lastCellInColumn)
    {
        bool isColumnBingoAreTrue = true;  
        for (int i = firstCellInColumn; i < lastCellInColumn + 1; i+=5)
        {
            bool isNumberCellRed = false;
            if (numberCellList[i].GetComponent<Text>().color == Color.red)
            {
                isNumberCellRed = true;
            }
            isColumnBingoAreTrue = isColumnBingoAreTrue & isNumberCellRed; 
        }
        isBingo = isColumnBingoAreTrue;
        isColumnBingo = isBingo;
    }
    
    private void CheckBingoLeftDiagonal(int firstCellInDiagonal, int lastCellInDiagonal)
    {
        
        bool isDiagonalBingoAreTrue = true;  
        for (int i = firstCellInDiagonal; i < lastCellInDiagonal + 5; i+=6)
        {
            bool isNumberCellRed = false;
            if (numberCellList[i].GetComponent<Text>().color == Color.red)
            {
                isNumberCellRed = true;
            }
            isDiagonalBingoAreTrue = isDiagonalBingoAreTrue & isNumberCellRed; 
        }
        isBingo = isDiagonalBingoAreTrue;
        isDiagonalBingo = isBingo;
    }

    private void CheckBingoRightDiagonal(int firstCellInDiagonal, int lastCellInDiagonal)
    {
        
        bool isDiagonalBingoAreTrue = true;  
        for (int i = firstCellInDiagonal; i < lastCellInDiagonal - 1; i+=4)
        {
            bool isNumberCellRed = false;
            if (numberCellList[i].GetComponent<Text>().color == Color.red)
            {
                isNumberCellRed = true;
            }
            isDiagonalBingoAreTrue = isDiagonalBingoAreTrue & isNumberCellRed; 
        }
        isBingo = isDiagonalBingoAreTrue;
        isDiagonalBingo = isBingo;
    }

    private void EnableBackButton(string result)
    {
        isBingo = false;
        isRowBingo = false;
        isColumnBingo = false;
        isDiagonalBingo = false;
        winText.text = result;
        winTextLabel.SetActive(true);
        backButton.SetActive(true);
    }
}
