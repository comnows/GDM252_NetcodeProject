using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HockeyScore : MonoBehaviour
{
    public HockeyPuck hockeyPuck;
    public Text scoreText;
    public GameObject winText;
    public GameObject leaveGameButton;
    private int player1Score = 0;
    private int player2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        hockeyPuck.onGoal += OnGoalScore;
        //ShowScore();
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
            winText.gameObject.GetComponent<Text>().text = "Player1 Win!";
            winText.SetActive(true);
            leaveGameButton.SetActive(true);
        }
        else if(player2Score == 7)
        {
            winText.gameObject.GetComponent<Text>().text = "Player2 Win!";
            winText.SetActive(true);
            leaveGameButton.SetActive(true);
        }
    }
    
    public void LeaveHockeyGame()
    {
        winText.SetActive(false);
        player1Score = 0;
        player2Score = 0;
        hockeyPuck.ResetOriginPosition();
        ShowScore();
        FindObjectOfType<PlayerInteract>().ExitMiniGame("AirHockeyGame");
        leaveGameButton.SetActive(false);
    }
}
