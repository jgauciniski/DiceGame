using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPopup : Popup
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI botScore;
    [SerializeField] GameObject playerWins;
    [SerializeField] GameObject botWins;

    int playerGameScore, botGameScore;

    // Start is called before the first frame update
    void Start()
    {
        playerGameScore = gameManager.GetPlayer(gameManager.IsPlayerTurn).GameScore;
        botGameScore = gameManager.GetPlayer(!gameManager.IsPlayerTurn).GameScore;

        playerScore.text = playerGameScore.ToString();
        botScore.text = botGameScore.ToString();

        var winner = playerGameScore > botGameScore;
        playerWins.SetActive(winner);
        botWins.SetActive(!winner);

        //Save the winner in player prefs
        PlayerPrefsManager.SetPlayerWins(winner, 1);

        //Save the total games finished
        var totalMatches = PlayerPrefsManager.GetTotalMatches();
        PlayerPrefsManager.SetTotalMatches(totalMatches + 1);
    }

    public void ButtonOK()
    {
        gameManager.EnableRollButton(false);
        anim.SetTrigger("Close");
    }
}
