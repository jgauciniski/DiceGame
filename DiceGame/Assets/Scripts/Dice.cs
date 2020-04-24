using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rotation = new Vector3(-90, -90, -90);
    Vector3 startPos;
    bool isReady = true;
    bool inGame = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        ResetDice();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady && !inGame)
        {
            transform.Rotate(rotation * Time.deltaTime);
        }
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetDice();
        }

        if (rb.IsSleeping() && !isReady)
        {
            isReady = true;
        }
    }

    public void RollDice()
    {
        if (isReady && !inGame)
        {
            isReady = false;
            inGame = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
    }

    public void ResetDice()
    {
        isReady = true;
        inGame = false;
        rb.useGravity = false;
        transform.position = startPos;
    }
}
