using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameplayPopup : Popup
{
   
    public override void ClosePopup()
    {
        base.ClosePopup();

        gameManager.EnableExitButton(true);

    }
}
