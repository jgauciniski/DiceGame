using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideNumber : MonoBehaviour
{
    public int side;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Table")
        {
            Debug.Log("SIDE " + side);
        }
    }
}
