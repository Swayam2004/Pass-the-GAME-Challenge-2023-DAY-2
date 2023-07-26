using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool("isAttacking", true);
            Invoke("StopAttacking", 1f);
        }
    }

    public void StopAttacking()
    {
        _animator.SetBool("isAttacking", false);
    }
}
