using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float speed;
    public Transform target;
    public float maximumDistance;
    public float minimumDistance;

    public Animator animator;

    public void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, minSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        if (Vector2.Distance(transform.position, target.position) > maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, maxSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
