using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalMatches;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI botScore;

    // Start is called before the first frame update
    void Start()
    {
        totalMatches.text = PlayerPrefsManager.GetTotalMatches().ToString();
        playerScore.text = PlayerPrefsManager.GetPlayerWins(true).ToString();
        botScore.text = PlayerPrefsManager.GetPlayerWins(false).ToString();
    }
}
