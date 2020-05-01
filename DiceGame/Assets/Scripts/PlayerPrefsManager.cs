using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string TOTAL_MATCHES_KEY = "total_matches";
    const string TOTAL_PLAYER_WINS_KEY = "total_player_wins";
    const string TOTAL_BOT_WINS_KEY = "total_bot_wins";

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master Volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetPlayerWins(bool isPlayer, int victory)
    {
        if (isPlayer)
        {
            PlayerPrefs.SetInt(TOTAL_PLAYER_WINS_KEY, victory);
        }
        else
        {
            PlayerPrefs.SetInt(TOTAL_BOT_WINS_KEY, victory);
        }
    }

    public static int GetPlayerWins(bool isPlayer)
    {
        if (isPlayer)
        {
            return PlayerPrefs.GetInt(TOTAL_PLAYER_WINS_KEY);
        }
        else
        {
            return PlayerPrefs.GetInt(TOTAL_BOT_WINS_KEY);
        }
    }

    public static void SetTotalMatches(int total)
    {
        PlayerPrefs.SetInt(TOTAL_MATCHES_KEY, total);
    }

    public static int GetTotalMatches()
    {
        return PlayerPrefs.GetInt(TOTAL_MATCHES_KEY);
    }
}
