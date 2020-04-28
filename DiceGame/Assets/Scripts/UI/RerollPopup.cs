using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RerollPopup : MonoBehaviour
{
    GameManager gm;
    Animator anim;

    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI botName;
    [SerializeField] TextMeshProUGUI playerRoundScore;
    [SerializeField] TextMeshProUGUI botRoundScore;
    [SerializeField] TextMeshProUGUI rerollCounter;
    [SerializeField] Button rerollButton;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerName.text = gm.GetPlayer(true).Name;
        botName.text = gm.GetPlayer(false).Name;
        playerRoundScore.text = gm.GetPlayer(true).RoundScore.ToString();
        botRoundScore.text = gm.GetPlayer(false).RoundScore.ToString();
        rerollCounter.text = gm.GetPlayer(true).Rerolls.ToString();
    }

    private void Update()
    {
        if (!gm.GetPlayer(true).CanReroll)
        {
            rerollButton.interactable = false;
        }
    }

    public void ButtonOK()
    {
        if (gm && anim)
        {
            //flag player as ready
            gm.GetPlayer(true).IsReady = true;
            gm.NextRound();
            anim.SetTrigger("Close");
        }
        else
        {
            Debug.LogError("Game Manager or Animator not found!" + gameObject.name);
        }

    }

    public void PlayerReroll()
    {
        if (gm.GetPlayer(true).Rerolls > 0)
        {
            gm.GetPlayer(true).Rerolls--;
            //clear result
            gm.GetPlayer(true).DiceResult.Clear();
            gm.GetPlayer(true).RoundScore = 0;
            gm.IsReroll = true;
            gm.IsPlayerTurn = true;
        }
        
        print("PLAYER REROLL " + gm.GetPlayer(true).Rerolls);
        

        gm.NextRound();
        anim.SetTrigger("Close");
    }

    public void DestroyPopup()
    {
        Destroy(transform.parent.gameObject);
    }
}

