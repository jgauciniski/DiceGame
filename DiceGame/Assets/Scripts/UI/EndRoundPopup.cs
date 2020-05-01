using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndRoundPopup : Popup
{
    [SerializeField] TextMeshProUGUI playerRoundScore;
    [SerializeField] TextMeshProUGUI botRoundScore;
    [SerializeField] TextMeshProUGUI rerollCounter;
    [SerializeField] GameObject[] doublesAreZero;
    [SerializeField] GameObject tieLabel;
    [SerializeField] GameObject doubleOdds;
    [SerializeField] Button rerollButton;
    [SerializeField] Button buttonOK;

    public override void Initialize(GameManager gm)
    {
        base.Initialize(gm);

        playerRoundScore.text = gameManager.GetPlayer(true).RoundScore.ToString();
        botRoundScore.text = gameManager.GetPlayer(false).RoundScore.ToString();
        rerollCounter.text = gameManager.GetPlayer(true).Rerolls.ToString();

        //reset doubles label
        doublesAreZero[0].SetActive(false);
        doublesAreZero[1].SetActive(false);
        //reset double-odds label
        doubleOdds.SetActive(false);

        //doubles
        if (gameManager.GetPlayer(true).RoundScore == 0)
        {
            doublesAreZero[0].SetActive(true);

            //if it's double-odds, enable double-odds label
            if (gameManager.GetPlayer(true).IsDouble && gameManager.GetPlayer(true).DiceResult[0] % 2 != 0)
            {
                doubleOdds.SetActive(true);
            }
        }

        if (gameManager.GetPlayer(false).RoundScore == 0)
        {
            doublesAreZero[1].SetActive(true);
        }

        if (!gameManager.GetPlayer(true).CanReroll)
        {
            rerollButton.interactable = false;
        }
        else { rerollButton.interactable = true; }

        //reset tie label
        tieLabel.SetActive(false);

        //Tie
        if (gameManager.IsTie())
        {
            tieLabel.SetActive(true);
        }
    }

    public void ButtonOK()
    {
        buttonOK.interactable = false;
        //flag player as ready
        gameManager.GetPlayer(true).IsReady = true;
        gameManager.NextRound();
        anim.SetTrigger("Close");
    }

    public void PlayerReroll()
    {
        rerollButton.interactable = false;

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

