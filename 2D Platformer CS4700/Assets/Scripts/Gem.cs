using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private float animMoveAmount = 1f, animTime = 1f;
    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.position.y + animMoveAmount, animTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.numberOfGems += 1;
            Destroy(gameObject);
        }
    }
}
