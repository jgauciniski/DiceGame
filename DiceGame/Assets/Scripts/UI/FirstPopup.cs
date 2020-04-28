using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPopup : MonoBehaviour
{
    GameManager gm;
    Animator anim;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }
    
    public void ButtonOK()
    {
        if (gm && anim)
        {
            gm.EnableRollButton(true);
            anim.SetTrigger("Close");
        }
        else
        {
            Debug.LogError("Game Manager or Animator not found!" + gameObject.name);
        }
        
    }

    public void ButtonRules()
    {
        //todo
    }

    public void DestroyPopup()
    {
        Destroy(transform.parent.gameObject);
    }
}
