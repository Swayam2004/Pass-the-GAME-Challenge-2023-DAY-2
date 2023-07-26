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

    private void Start()
    {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        Jump();
    }

    void Update()
    {
        MoveControls();
        Attacking();
    }

    public void MoveControls()
    {
        Vector2 movement = GameInput.Instance.GetMovementNormalized();
        
        transform.position +=  (Vector3)movement * _moveSpeed * Time.deltaTime;

        bool isMoving = !(movement.magnitude == 0);
        _animator.SetBool("isMoving", isMoving);
    }
    public void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_feetTransform.position, _checkRadius, _whatIsGround);

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
