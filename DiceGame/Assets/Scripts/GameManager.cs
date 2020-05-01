using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject rollButton;
    [SerializeField] Button exitButton;
    [SerializeField] UIManager UIManager;
    [SerializeField] Animator animScore;
    [SerializeField][Tooltip("Waiting time before reset in case of invalid play")] float invalidPlayTimer = 5f;
    [SerializeField] int numberOfRounds = 11;
    [SerializeField] Dice[] dice; //0 - white, 1 - red
    [SerializeField] Player[] player; //0 - Player, 1 - Bot

    [Header("DEBUG - Manual Roll Settings")]
    [SerializeField] [Tooltip("Set true for next roll manual")] bool nextRollManual;
    [SerializeField] List<int> diceResultManual;

    [Header("DEBUG - Bot's Reroll decision settings")]
    [SerializeField] [Tooltip("Min round score threshold")] [Range(1, 11)] int min = 4;
    [SerializeField] [Tooltip("Max round score threshold")] [Range(1, 11)] int max = 9;

    public bool IsReroll { get; set; }

    int round;
    float invalidTimer = 0;
    bool isTie, isTieEven, isDouble, isDoubleEven;
    bool isGameOver = false;

    public bool IsPlayerTurn { get; set; }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        //Check if the game is over
        if ((round > numberOfRounds || player[0].GameScore > numberOfRounds/2 || 
            player[1].GameScore > numberOfRounds / 2) && !isGameOver)
        {
            GameOver();
            isGameOver = true;
        }

        //prevent invalid play (dice stuck)
        CheckForInvalidPlay();
    }
    
    public void StartGame()
    {
        ResetGame();
        //First Popup
        UIManager.InstantiatePopup(UIManager.firstPopup, this);
        InitializeManualRollList();
    }

    void InitializeManualRollList()
    {
        //set the manual roll list to the dice count
        for (int i=0; i < dice.Length; i++) { diceResultManual.Add(0); }
    }

    public void EnableRollButton(bool isEnable)
    {
        //Enable tap in the screen for the player
        rollButton.SetActive(isEnable);
    }

    void ResetGame()
    {
        isGameOver = false;
        round = 1;
        EnableNextRollManual(false);
        EnableRollButton(false);
        ResetPlayers();
        SetRollTurn(true); //set turn to the player
        IsReroll = false;
        isDouble = false;
        invalidTimer = 0;
        UIManager.ResetUI(player[0].GameScore, player[1].GameScore, round);
    }

    void ResetPlayers()
    {
        foreach (Player p in player)
        {
            p.ResetPlayer();
        }
    }

    void ResetDice()
    {
        foreach (var d in dice)
        {
            d.ResetDice();
        }
    }

    public void RollTheDice()
    {
        foreach (var d in dice)
        {
            d.RollDice();
        }

        EnableRollButton(false);
    }

    void CheckForInvalidPlay()
    {
        int index;
        if (IsPlayerTurn) { index = 0; }
        else { index = 1; }

        //prevents the dice for invalid play 
        if (player[index].DiceResult.Count > 0 && player[index].DiceResult.Count < dice.Length)
        {
            invalidTimer += Time.deltaTime;
            if (invalidTimer > invalidPlayTimer)
            {
                //Invalid play popup
                UIManager.InstantiatePopup(UIManager.invalidPopup, this);
                invalidTimer = 0;
                //Invalid play - reseting the round
                ResetRoll(index);
            }
        }
        else { invalidTimer = 0; }
    }

    void ResetRoll(int index)
    {
        player[index].IsReady = false;
        player[index].RoundScore = 0;
        player[index].DiceResult.Clear();
        ResetDice();

        if (index == 0) { EnableRollButton(true); }
        else
        {
            //Random delay before bot's roll
            var delay = UnityEngine.Random.Range(0, player[1].DelayBeforeNextRoll);
            StartCoroutine(RollTheDiceDelay(delay));
        }
    }

    void SetRollTurn(bool isPlayer)
    {
        IsPlayerTurn = isPlayer;

        //set the dice color
        if (IsPlayerTurn)
        {
            foreach (var d in dice)
            {
                d.GetComponent<MeshRenderer>().material = player[0].DiceMaterial;
            }
        }
        else
        {
            foreach (var d in dice)
            {
                d.GetComponent<MeshRenderer>().material = player[1].DiceMaterial;
            }
        }
    }

    //Called by the Dice (SideNumber)
    public void SetDiceResult(int side)
    {
        if (IsPlayerTurn)
        {
            player[0].DiceResult.Add(side);
            if (player[0].DiceResult.Count == dice.Length) { SetRollResult(0); }
        }
        else
        {
            player[1].DiceResult.Add(side);
            if (player[1].DiceResult.Count == dice.Length) { SetRollResult(1); }
        }
    }

    void SetRollResult(int index)
    {
        if (nextRollManual)
        {
            ApplyManualScore();

            //Manual Score Warning
            UIManager.ShowWarningConfirmation(this);
        }

        //check for doubles 
        if (player[index].DiceResult.Any(x => x != player[index].DiceResult[0]))
        {
            isDouble = false;
            player[index].IsDouble = false;

            //if different add together
            foreach (var result in player[index].DiceResult)
            {
                player[index].RoundScore += result;
            }
        }
        else
        {
            isDouble = true;
            player[index].IsDouble = true;
            //if doubles set to zero
            player[index].RoundScore = 0;
            //Debug.Log("Round score for " + player[index].Name + " " + player[index].RoundScore);
        }

        //check for double-even
        if (isDouble && player[index].DiceResult[0] % 2 == 0) { isDoubleEven = true; }
        else { isDoubleEven = false; }


        CheckIfCanReroll();

        if (!nextRollManual) { NextRoll(); }
        
    }

    //Also called by UI Warning Popup
    public void NextRoll()
    {
        if (IsPlayerTurn && !IsReroll)
        {
            //Delay before next player
            StartCoroutine(NextPlayerDelay(player[0].DelayBeforeNextRoll));
        }
        else
        {
            ProcessRoundResults();
            IsReroll = false;

            //End Round popup
            UIManager.InstantiatePopup(UIManager.endRoundPopup, this);
        }
    }

    void CheckIfCanReroll()
    {
        if (IsPlayerTurn)
        {
            if (player[0].Rerolls > 0) { player[0].CanReroll = true; }
            else { player[0].CanReroll = false; }
            
            //if double-odds, player can't reroll
            if (isDouble && !isDoubleEven) { player[0].CanReroll = false; }
        }
        else
        {
            if (player[1].Rerolls > 0) { player[1].CanReroll = true; }
            else { player[1].CanReroll = false; }
            
            //if it's double-evens, bot can't reroll
            if (isDouble && isDoubleEven) { player[1].CanReroll = false; }
        }
    }

    void ProcessRoundResults()
    {
        if (player[0].RoundScore == player[1].RoundScore)
        {
            //Debug.Log("It's a tie!");
            isTie = true;
            //Even result goes to the player
            if (player[0].RoundScore % 2 == 0)
            {
                //Debug.Log("Its even!");
                isTieEven = true;
            }
            else //Odd result goes to the bot
            {
                //Debug.Log("Its odd!");
                isTieEven = false;
            }
        }
        else { isTie = false; }
    }

    void NextPlayer()
    {
        ResetDice();

        //Set the opponent
        if (!IsReroll) { SetRollTurn(!IsPlayerTurn); }
        else { SetRollTurn(IsPlayerTurn); }

        if (IsPlayerTurn)
        {
            //reset player
            ResetRoll(0);
            EnableRollButton(true);
        }
        else
        {
            //reset bot
            ResetRoll(1);
            EnableRollButton(false);
        }
        //Turn Info Popup
        UIManager.InstantiatePopup(UIManager.infoTurnPopup, this);
    }

    IEnumerator RollTheDiceDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RollTheDice();
    }

    IEnumerator NextPlayerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextPlayer();
    }

    bool IsReadyForNextRound()
    {
        //make sure there's no reroll
        if (player[0].IsReady && player[1].IsReady)
        {
            return true;
        }
        else return false;
    }

    public void NextRound() //called by the UI (RerollPopup)
    {
        if (isGameOver) { return; }

        //Bot do not reroll if it's manual
        if(!nextRollManual) { CheckForBotReroll(); }
        
        if (IsReadyForNextRound())
        {
            round++;
            AddScore();
            ResetRound();
        }
        else
        {
            //Next player
            StartCoroutine(NextPlayerDelay(player[0].DelayBeforeNextRoll));
        }
    }

    void CheckForBotReroll()
    {
        //if it's tie and odd roll, player doesn't need to reroll
        if (isTie && !isTieEven)
        {
            player[1].IsReady = true;
            return;
        }

        //Check if Bot wants to reroll
        if (player[1].CanReroll)
        {
            var isBotRerolled = player[1].RerollRound(min, max, player[0].RoundScore);

            if (isBotRerolled && player[1].Rerolls > 0)
            {
                player[1].Rerolls--;
                IsReroll = true;
                SetPlayersNotReady();

                //Debug.Log("BOT WANTS TO REROLL!");
                IsPlayerTurn = false;
                GetPlayer(false).RoundScore = 0;
            }
            else { player[1].IsReady = true; }
        }
        else { player[1].IsReady = true; }
    }

    void SetPlayersNotReady()
    {
        foreach (Player p in player) { p.IsReady = false; }
    }

    //Add score and trigger the score anim
    void AddScore()
    {
        if (player[0].RoundScore > player[1].RoundScore)
        {
            player[0].GameScore += 1;
            animScore.SetBool("IsPlayer", true);
        }
        else if(isTie)
        {
            if (isTieEven)
            {
                player[0].GameScore += 1;
                animScore.SetBool("IsPlayer", true);
            }
            else
            {
                player[1].GameScore += 1;
                animScore.SetBool("IsPlayer", false);
            }
        }
        else
        {
            player[1].GameScore += 1;
            animScore.SetBool("IsPlayer", false);
        }

        animScore.SetTrigger("NextRound");
    }

    void ResetRound()
    {
        SetRollTurn(true);

        foreach (Player p in player)
        {
            p.DiceResult.Clear();
            p.RoundScore = 0;
            p.IsReady = false;
        }
      
        ResetDice();
        EnableRollButton(true);

        //Update UI
        UIManager.SetUI(player[0].GameScore, player[1].GameScore, round);
    }

    //used for UI
    public Player GetPlayer(bool isPlayer)
    {
        if (isPlayer) { return player[0]; }
        else { return player[1]; }
    }

    void GameOver()
    {
        EnableRollButton(false);
        if (round >= numberOfRounds) { round = numberOfRounds; }
        //update UI
        UIManager.SetUI(player[0].GameScore, player[1].GameScore, round);
        ResetDice();
        UIManager.InstantiatePopup(UIManager.gameOverPopup, this);
    }

    public void ExitGameplay()
    {
        EnableExitButton(false);
        UIManager.ShowExitConfirmation(this);
    }

    public void EnableExitButton(bool isEnabled)
    {
        exitButton.interactable = isEnabled;
    }

    private void OnApplicationQuit()
    {
        ResetGame();
    }

    //Apply score entered in the inspector (for the current player in game)
    void ApplyManualScore()
    {
        if (IsPlayerTurn)
        {
            player[0].DiceResult.Clear();
            for (int i=0; i < diceResultManual.Count; i++)
            {
                player[0].DiceResult.Add(diceResultManual[i]);
            }
        }
        else
        {
            player[1].DiceResult.Clear();
            for (int i = 0; i < diceResultManual.Count; i++)
            {
                player[1].DiceResult.Add(diceResultManual[i]);
            }
        }
    }

    public void EnableNextRollManual(bool isEnabled)
    {
        nextRollManual = isEnabled;
    }

    public List<int> GetManualDice()
    {
        return diceResultManual;
    }

    public bool IsTie()
    {
        return isTie;
    }
}

