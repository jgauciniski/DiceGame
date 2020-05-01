using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected new string name;
    [SerializeField] protected int rerolls = 3;
    [SerializeField] protected List<int> diceResult;
    [SerializeField] protected Material diceMaterial;
    [SerializeField] protected float delayBeforeNextRoll = 1f;

    public bool IsReady { get; set; }
    public bool IsDouble { get; set; }
    public bool CanReroll { get; set; }
    public int RoundScore { get; set; }
    public int GameScore { get; set; }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Rerolls
    {
        get { return rerolls; }
        set { rerolls = value; }
    }

    public Material DiceMaterial
    {
        get { return diceMaterial; }
        private set { diceMaterial = value; }
    }

    public List<int> DiceResult
    {
        get { return diceResult; }
        set { diceResult = value; }
    }

    public void ResetPlayer()
    {
        GameScore = 0;
        RoundScore = 0;
        IsReady = false;
        Rerolls = 3;
        CanReroll = true;
        DiceResult.Clear();
    }

    public virtual bool RerollRound(int min, int max, int opponentRoundScore)
    {
        return false;
    }

    public float DelayBeforeNextRoll
    {
        get { return delayBeforeNextRoll; }
        set { delayBeforeNextRoll = value; }
    }
}
