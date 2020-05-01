using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPopup : Popup
{
    [SerializeField] GameObject rules;

    private void OnEnable()
    {
        rules.SetActive(false);
    }

    public void ButtonOK()
    {
        gameManager.EnableRollButton(true);
        anim.SetTrigger("Close");
    }
}
