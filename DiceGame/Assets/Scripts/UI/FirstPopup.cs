using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPopup : Popup
{
    public void ButtonOK()
    {
        gameManager.EnableRollButton(true);
        anim.SetTrigger("Close");
    }

    public void ButtonRules()
    {
        //todo
    }
}
