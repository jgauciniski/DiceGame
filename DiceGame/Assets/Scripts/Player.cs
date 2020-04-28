using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] bool isBot;
    [SerializeField] int rerolls = 3;
    [SerializeField] List<int> diceResult;
    [SerializeField] Material diceMaterial;

    public bool IsReady { get; set; }
    public bool CanReroll { get; set; }
    public int RoundScore { get; set; }
    public int GameScore { get; set; }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public bool IsBot
    {
        get { return isBot; }
        private set { isBot = value; }
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

    public bool RerollRoundBot(int min, int max, int opponentRoundScore)
    {
        bool reroll = false;

        if (min >= max)
        {
            Debug.LogError("Min value can not be greater or equal than max value! ");
            return false;
        }
        //Never reroll if the round score is greater than the player
        if (RoundScore > opponentRoundScore) { reroll = false; }

        //Reroll if the player has a score less than the min (debug set in the game manager - inspector)
        else if (opponentRoundScore <= min) { reroll = true; }
        //Random 50% chance of reroll if the player has a score in between min and max (debug set in the game manager - inspector)
        else if (opponentRoundScore > min && opponentRoundScore <= max)
        {
            int decision = Random.Range(0, 2);
            
            if (decision == 0) { reroll = true; }
            else { reroll = false; }
        }
        //clear result
        if (reroll) { diceResult.Clear(); }

        return reroll;
    }
}
