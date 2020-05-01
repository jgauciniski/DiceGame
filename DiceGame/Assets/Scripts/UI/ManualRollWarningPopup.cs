using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManualRollWarningPopup : Popup
{
    [SerializeField] TextMeshProUGUI[] dice;
    [SerializeField] Button uncheck;
    [SerializeField] Button buttonOK;

    private void Start()
    {
        GetManualDiceToUI();
    }

    void GetManualDiceToUI()
    {
        var manualRoll = gameManager.GetManualDice();

        if (manualRoll.Count != dice.Length)
        {
            Debug.LogError("Manual dice need to be assigned!");
            return;
        }
        else
        {
            for (int i = 0; i < manualRoll.Count; i++)
            {
                dice[i].text = manualRoll[i].ToString();
            }
        }
    }

    public void ButtonOK()
    {
        buttonOK.interactable = false;
        anim.SetTrigger("Close");
        gameManager.NextRoll();
    }

    public void UncheckManual()
    {
        uncheck.interactable = false;
        anim.SetTrigger("Close");
        gameManager.EnableNextRollManual(false);
        gameManager.NextRoll();
    }
}
