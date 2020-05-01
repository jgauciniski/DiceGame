using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseDataPopup : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void ShowPopup()
    {
        anim.SetBool("Active", true);
        anim.SetBool("Confirmation", false);
    }

    public void ClosePopup()
    {
        anim.SetBool("Active", false);
        anim.SetBool("Confirmation", false);
    }

    public void ShowConfirmation()
    {
        anim.SetBool("Active", false);
        anim.SetBool("Confirmation", true);
    }
}
