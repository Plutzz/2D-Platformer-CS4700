using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float desiredX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredX = Input.GetAxisRaw("Horizontal") * 3f;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
        }
    }
}
