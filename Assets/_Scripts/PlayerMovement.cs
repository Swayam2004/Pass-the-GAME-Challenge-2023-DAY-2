using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _feetTransform;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _noOfJumps;

    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _isGrounded;
    private bool _isJumping;
    private float _jumpTimeCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();    
    }

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
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
        transform.position += new Vector3(movement, 0, 0) * _moveSpeed * Time.deltaTime;
    }
    public void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_feetTransform.position, _checkRadius, _whatIsGround);

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (_isGrounded)
        {
            _isJumping = true;
            _jumpTimeCounter = _noOfJumps;
            _rb.velocity = Vector2.up * _jumpForce;
        }

        if (_isJumping)
        {
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * _jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        _isJumping = false;
    }

    public void Attacking()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool("isAttacking", true);
            Invoke("AttackingStop", 0.5f);
        }
    }

    public void AttackingStop()
    {
        _animator.SetBool("isAttacking", false);
    }
       
}
