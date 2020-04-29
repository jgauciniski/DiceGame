using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] protected float timeOnScreen = 1f;

    protected GameManager gameManager;
    protected Animator anim;

    public virtual void Initialize(GameManager gm)
    {
        gameManager = gm;
        anim = GetComponent<Animator>();

        if (!gameManager) { Debug.LogError("Game Manager not found!" + gameObject.name); }
        if (!anim) { Debug.LogError("Animator not found!" + gameObject.name); }
    }

    public virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        if (!anim) { Debug.LogError("Animator not found!" + gameObject.name); 
    }
}

    public void DestroyPopup()
    {
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    public virtual void ClosePopup()
    {
        anim.SetTrigger("Close");
    }
}
