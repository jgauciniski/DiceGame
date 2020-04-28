using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;
    [SerializeField] Vector3 rotation = new Vector3(100, 100, 100);

    Rigidbody rb;
    Vector3 startRot;
    Vector3 startPos;
    bool isReady = true;
    bool inGame = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            rb.isKinematic = false;
            rb.AddTorque(Random.Range(0, -500), Random.Range(0, -500), Random.Range(0, -500));
        }
    }

    public void ResetDice()
    {
        isReady = true;
        inGame = false;
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = startPos;
        startRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        transform.rotation = Quaternion.Euler(startRot);
    }
}
