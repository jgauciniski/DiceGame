using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : Player
{
    public override bool RerollRound(int min, int max, int opponentRoundScore)
    {
        bool reroll = false;

        if (min >= max)
        {
            Debug.LogError("Bot's reroll decision Min value can not be greater or equal than max value!");
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
