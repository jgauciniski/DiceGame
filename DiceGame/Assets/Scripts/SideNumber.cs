using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideNumber : MonoBehaviour
{
    [SerializeField] int side;
    Rigidbody rb;
    Dice dice;
    bool resultSent;

    void Start()
    {
        resultSent = false;
        dice = GetComponentInParent<Dice>();
        rb = dice.GetComponent<Rigidbody>();
    }

    //get the dice side number 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Table" && rb.IsSleeping() && !resultSent)
        {
            dice.gameManager.SetDiceResult(side);
            resultSent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Table")
        {
            resultSent = false;
        }
        
    }
}
