using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _fallingSpeed = 2f;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _groundCheck;

    Vector2 _startPosition;

    bool isGrounded;
    int _jumpCount;
    float fallTimer = 5;
    float jumpTimer;

    private void Start()
    {
        _startPosition = transform.position;
        _jumpCount = _maxJumps;
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit != null;

        var horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        var rb = GetComponent<Rigidbody2D>();
        var animator = GetComponent<Animator>();

        if (Mathf.Abs(horizontal) >= 1)
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
        else if (Mathf.Abs(horizontal) == 0)
            rb.velocity = new Vector2(0, rb.velocity.y);



        bool running = horizontal != 0;
        animator.SetBool("Run", running);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButtonDown("Jump") && _jumpCount > 0)
         {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, _jumpSpeed );
            _jumpCount--;
            fallTimer = 0;
            jumpTimer = 0;
        }
        else if(Input.GetButton("Jump") && jumpTimer <= _maxJumpDuration && _jumpCount > 0)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, _jumpSpeed);
            fallTimer = 0;
            jumpTimer += Time.deltaTime;
        }
        

        if (isGrounded)
        {
            fallTimer = 0;
            _jumpCount = _maxJumps;
        }
        else
        {
            fallTimer += Time.deltaTime;
            var downForce = _fallingSpeed * fallTimer * fallTimer;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - downForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
