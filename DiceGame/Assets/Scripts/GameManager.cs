﻿using System.Collections;
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
    bool isTieEven;
    bool isGameOver = false;

    public bool IsPlayerTurn { get; set; }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (round > numberOfRounds && !isGameOver)
        {
            GameOver();
            isGameOver = true;
        }

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
        nextRollManual = false;
        EnableRollButton(false);
        ResetPlayers();
        SetRollTurn(true); //set turn to the player
        IsReroll = false;
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

    public void RollTheDices()
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
        else { RollTheDices(); }
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
            //todo instantiate confirmation
        }

        //check for doubles 
        if (player[index].DiceResult.Any(x => x != player[index].DiceResult[0]))
        {
            //if different add together
            foreach (var result in player[index].DiceResult)
            {
                player[index].RoundScore += result;
            }
        }
        else
        {
            //if player rolled double-odds, cant reroll
            if (player[0].DiceResult[0] % 2 != 0) { player[0].CanReroll = false; }
            //if bot rolled double-evens, cant reroll
            else { player[1].CanReroll = false; }

            //if doubles set to zero
            player[index].RoundScore = 0;
        }

        if (IsPlayerTurn && !IsReroll) { NextPlayer(); }
        else
        {
            ProcessRoundResults();
            IsReroll = false;
           
            //Reroll popup
            UIManager.InstantiatePopup(UIManager.endRoundPopup, this);
        }

        Debug.Log("Round score for " + player[index].Name + " " + player[index].RoundScore);
    }

    void CheckIfCanReroll()
    {
        foreach (Player p in player)
        {
            if (p.Rerolls > 0) { p.CanReroll = true; }
            else { p.CanReroll = false; }
        }
    }

    void ProcessRoundResults()
    {
        if (player[0].RoundScore == player[1].RoundScore)
        {
            Debug.Log("It's a tie!");

            //Even result goes to the player
            if (player[0].RoundScore % 2 == 0)
            {
                Debug.Log("Its even!");
                //player[1].RoundScore = 0;
                isTieEven = true;
            }
            else //Odd result goes to the bot
            {
                Debug.Log("Its odd!");
                //player[0].RoundScore = 0;
                isTieEven = false;
            }
        }
    }

    void NextPlayer()
    {
        ResetDice();

        //Set the opponent
        if (!IsReroll) { SetRollTurn(!IsPlayerTurn); }
        else { SetRollTurn(IsPlayerTurn); }

        if (IsPlayerTurn) { EnableRollButton(true); }
        else
        {
            EnableRollButton(false);
            //todo: add delay;
            RollTheDices();
        }

        //Turn Info Popup
        UIManager.InstantiatePopup(UIManager.infoTurnPopup, this);
    }

    bool IsReadyForNextRound()
    {
        if (player[0].IsReady && player[1].IsReady)
        {
            return true;
        }
        else return false;
    }

    public void NextRound() //called by the UI (RerollPopup)
    {
        if (isGameOver) { return; }

        CheckIfCanReroll();
        CheckForBotReroll();

        if (IsReadyForNextRound())
        {
            round++;
            AddScore();
            ResetRound();
        }
        else
        {
            NextPlayer();
        }
    }

    void CheckForBotReroll()
    {
        //Check if Bot wants to reroll
        if (player[1].CanReroll)
        {
            var isBotRerolled = player[1].RerollRound(min, max, player[0].RoundScore);

            if (isBotRerolled && player[1].Rerolls > 0)
            {
                player[1].Rerolls--;
                IsReroll = true;
                SetPlayersNotReady();
                //todo show ui

                Debug.Log("BOT WANTS TO REROLL!");
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

    void AddScore()
    {
        if (player[0].RoundScore > player[1].RoundScore)
        {
            player[0].GameScore += 1;
        }
        else if (isTieEven) { player[0].GameScore += 1; }
        else if (!isTieEven) { player[1].GameScore += 1; }
        else { player[1].GameScore += 1; }
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

        CheckIfCanReroll();
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

        round = numberOfRounds;
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
            for (int i=0; i < player[0].DiceResult.Count; i++)
            {
                player[0].DiceResult[i] = diceResultManual[i];
            }
        }
        else
        {
            for (int i = 0; i < player[1].DiceResult.Count; i++)
            {
                player[1].DiceResult[i] = diceResultManual[i];
            }
        }
    }
}

