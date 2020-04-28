using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTurnPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] float timeOnScreen = 1f;
    GameManager gm;
    Animator anim;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = gm.GetPlayer(gm.IsPlayerTurn).Name.ToUpper() + "'S";
      
        Invoke("ClosePopup", timeOnScreen);
    }

    public void DestroyPopup()
    {
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    void ClosePopup()
    {
        anim.SetTrigger("Close");
    }

}
