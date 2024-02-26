using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        float animationLength = _animator.GetCurrentAnimatorClipInfo(0).Length - 0.5f;

        Destroy(gameObject, animationLength);
    }
}