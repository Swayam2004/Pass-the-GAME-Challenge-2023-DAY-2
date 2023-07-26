using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maximumDistance;
    [SerializeField] private float _minimumDistance;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < _maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _minSpeed * Time.deltaTime);
            _animator.SetBool("isMoving", true);
        }

        if (Vector2.Distance(transform.position, target.position) > _maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _maxSpeed * Time.deltaTime);
            _animator.SetBool("isMoving", true);
        }

        if (Vector2.Distance(transform.position, target.position) < _minimumDistance)
        {
            _animator.SetBool("isMoving", false);
        }
    }
}
