using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] float autoLoadNextLevelAfter;
  
    void Start()
    {
        if (autoLoadNextLevelAfter == 0)
        {
            //Debug.Log("Level auto load disabled");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
            
    }

    public void LoadLevel(string name) 
    {
        //Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        //Debug.Log("QUIT!!");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene+1);
    }
}
