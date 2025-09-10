using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Blade : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float moveTime = 1;
    [SerializeField] private Ease ease;

    public void Start()
    {
        transform.position = startPoint.position;
        transform.DOMoveX(endPoint.position.x, moveTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
    
    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, 0, spinSpeed * Time.deltaTime * 100);
    }
}
