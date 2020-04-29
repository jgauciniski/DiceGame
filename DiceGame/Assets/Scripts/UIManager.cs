using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform popupParent;
    [SerializeField] Transform exitPopupParent;
    [SerializeField] TextMeshProUGUI playerScoreUI;
    [SerializeField] TextMeshProUGUI botScoreUI;
    [SerializeField] TextMeshProUGUI roundUI;

    public GameObject firstPopup;
    public GameObject infoTurnPopup;
    public GameObject endRoundPopup;
    public GameObject invalidPopup;
    public GameObject gameOverPopup;
    public GameObject exitGameplayPopup;

    public void InstantiatePopup(GameObject popup, GameManager gm)
    {
        Instantiate(popup, popupParent).GetComponentInChildren<Popup>().Initialize(gm);
    }

    public void ShowExitConfirmation(GameManager gm)
    {
        Instantiate(exitGameplayPopup, exitPopupParent).GetComponentInChildren<Popup>().Initialize(gm);
    }

    public void SetUI(int player, int bot, int round)
    {
        playerScoreUI.text = player.ToString();
        botScoreUI.text = bot.ToString();
        roundUI.text = round.ToString();
    }

    public void ResetUI(int player, int bot, int round)
    {
        playerScoreUI.text = player.ToString();
        botScoreUI.text = bot.ToString();
        roundUI.text = round.ToString();
    }
}
