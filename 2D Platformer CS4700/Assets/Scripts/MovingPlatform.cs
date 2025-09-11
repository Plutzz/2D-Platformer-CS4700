using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    private bool movingForward = true;

    // Update is called once per frame
    void Update()
    {
        if (!movingForward)
        {
            rb.velocity = (startPoint.position - transform.position).normalized * moveSpeed;
            if ((transform.position - startPoint.position).magnitude < 0.25f)
            {
                rb.velocity = Vector2.zero;
                movingForward = true;
            }
        }
        else
        {
            rb.velocity = (endPoint.position - transform.position).normalized * moveSpeed;
            if ((transform.position - endPoint.position).magnitude < 0.25f)
            {
                rb.velocity = Vector2.zero;
                movingForward = false;
            }
        }
    }
}
