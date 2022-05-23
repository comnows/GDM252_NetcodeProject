using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HockeyScore : MonoBehaviour
{
    public HockeyPuck hockeyPuck;

    public Text scoreText;

    private int player1Score = 0;
    private int player2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        hockeyPuck.onGoal += OnGoalScore;
        ShowScore();
    }

    private void OnGoalScore(int id)
    {
        AddScore(id);
        ShowScore();
        hockeyPuck.ResetPosition(id);
        CheckGameEnd();
    }

    private void AddScore(int id)
    {
        if(id == 1)
        {
            player2Score += 1;
        }
        else
        {
            player1Score += 1;
        }
    }

    private void ShowScore()
    {
        scoreText.text = $"{player1Score}" + " : " + $"{player2Score}";
    }

    private void CheckGameEnd()
    {
        if(player1Score == 7)
        {

        }
        else if(player2Score == 7)
        {

        }
    }
}
