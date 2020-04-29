using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTurnPopup : Popup
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerNameReroll;
    [SerializeField] TextMeshProUGUI playerRerollCount;
    [SerializeField] GameObject turnGroup;
    [SerializeField] GameObject rerollGroup;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = gameManager.GetPlayer(gameManager.IsPlayerTurn).Name;
        playerNameReroll.text = playerName.text;
        playerRerollCount.text = gameManager.GetPlayer(gameManager.IsPlayerTurn).Rerolls.ToString();
        turnGroup.SetActive(!gameManager.IsReroll);
        rerollGroup.SetActive(gameManager.IsReroll);

        Invoke("ClosePopup", timeOnScreen);
    }
}
