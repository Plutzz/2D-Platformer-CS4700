using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float force = 10f;
    [SerializeField] Animator animator;

    public void PlayAnimation()
    {
        animator.Play("Spring");
        AudioManager.Instance.spring.Play();
    }
}
