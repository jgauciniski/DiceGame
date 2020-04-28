using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    [SerializeField] float timerToClose = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Close", timerToClose);
    }

    void Close()
    {
        Destroy(gameObject);
    }
}
