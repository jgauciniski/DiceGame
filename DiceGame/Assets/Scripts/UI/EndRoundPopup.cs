using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndRoundPopup : Popup
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI botName;
    [SerializeField] TextMeshProUGUI playerRoundScore;
    [SerializeField] TextMeshProUGUI botRoundScore;
    [SerializeField] TextMeshProUGUI rerollCounter;
    [SerializeField] Button rerollButton;

    public override void Initialize(GameManager gm)
    {
        base.Initialize(gm);

        playerName.text = gameManager.GetPlayer(true).Name;
        botName.text = gameManager.GetPlayer(false).Name;
        playerRoundScore.text = gameManager.GetPlayer(true).RoundScore.ToString();
        botRoundScore.text = gameManager.GetPlayer(false).RoundScore.ToString();
        rerollCounter.text = gameManager.GetPlayer(true).Rerolls.ToString();

        if (!gameManager.GetPlayer(true).CanReroll) { rerollButton.interactable = false; }
    }

    public void ButtonOK()
    {
        //flag player as ready
        gameManager.GetPlayer(true).IsReady = true;
        gameManager.NextRound();
        anim.SetTrigger("Close");
    }
        

    public void PlayerReroll()
    {
        if (gameManager.GetPlayer(true).Rerolls > 0)
        {
            gameManager.GetPlayer(true).Rerolls--;
            //clear result
            gameManager.GetPlayer(true).DiceResult.Clear();
            gameManager.GetPlayer(true).RoundScore = 0;
            gameManager.IsReroll = true;
            gameManager.IsPlayerTurn = true;
        }

        gameManager.NextRound();
        anim.SetTrigger("Close");
    }
}

