﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    private static MusicManager _instance;

    [SerializeField] AudioClip[] levelMusicChangeArray;
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (_instance != null)
        {
            Destroy(gameObject);
            //Debug.Log("Duplicate music player self-destructiong!");
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
        //print("MUSIC " + scene.buildIndex);

        if (thisLevelMusic) //If theres's some music attached
        {
            audio.clip = thisLevelMusic;

            if (audio.clip.name == thisLevelMusic.name && !audio.isPlaying)
            {
                audio.clip = thisLevelMusic;
                audio.loop = true;
                audio.Play();
            }
        }
    }

    public void ChangeVolume(float volume)
    {
        audio.volume = volume;
       // print("VOL " + audio.volume);
    }
}
