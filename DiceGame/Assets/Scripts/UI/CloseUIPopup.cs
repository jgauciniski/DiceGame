using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIPopup : Popup
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ClosePopup", timeOnScreen);
    }
}
