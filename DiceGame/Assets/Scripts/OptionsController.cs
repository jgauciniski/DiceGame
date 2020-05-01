using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [SerializeField] Slider volumeSlider;
    private MusicManager musicManager;

    // Use this for initialization
    void Start ()
    {
        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (musicManager) { musicManager.ChangeVolume(volumeSlider.value); }
        else { Debug.LogError("Music Manager missing!"); }
	}

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
    }

    public void SetDefaults()
    {
        volumeSlider.value = 0.5f;
    }

    public void EraseStats()
    {
        PlayerPrefsManager.SetTotalMatches(0);
        PlayerPrefsManager.SetPlayerWins(true, 0);
        PlayerPrefsManager.SetPlayerWins(false, 0);
    }
}
