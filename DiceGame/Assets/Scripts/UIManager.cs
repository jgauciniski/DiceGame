using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform popupParent;
    [SerializeField] TextMeshProUGUI playerScoreUI;
    [SerializeField] TextMeshProUGUI botScoreUI;
    [SerializeField] TextMeshProUGUI roundUI;

    public GameObject firstPopup;
    public GameObject infoTurnPopup;
    public GameObject roundWinPopup;
    public GameObject rerollPopup;
    public GameObject tiePopup;


    public void InstantiatePopup(GameObject popup)
    {
        Instantiate(popup, popupParent);
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
