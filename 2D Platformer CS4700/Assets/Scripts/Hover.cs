using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private float hoverAmount = 0.1f, hoverTime = 1;
    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y + hoverAmount, hoverTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
