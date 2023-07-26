using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public Rigidbody2D rb;
    public Transform feet;
    public bool isGrounded;
    public bool isJumping;
    public LayerMask whatIsGround;
    public float checkRadius;
    public float jumpTime;
    public float jumpTimeCounter;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveControls();
        Jump();
        Attacking();
    }

    public void MoveControls()
    {
        var movement = Input.GetAxis("Horizontal");
        if (movement > 0 || movement < 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        transform.position += new Vector3(movement, 0, 0) * Speed * Time.deltaTime;
    }
    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, whatIsGround);

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        if (isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * JumpForce;
        }
        if (isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        isJumping = false;
    }

    public void Attacking()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isAttacking", true);
            Invoke("AttackingStop", 0.5f);
        }
    }

    public void AttackingStop()
    {
        animator.SetBool("isAttacking", false);
    }
       
}
