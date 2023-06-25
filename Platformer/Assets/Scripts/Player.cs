using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] float _fallingSpeed = 2f;
    [SerializeField] Transform _groundCheck;

    Vector2 _startPosition;

    int _jumpCount;
    float _fallTimer = 5;

    private void Start()
    {
        _startPosition = transform.position;
        _jumpCount = _maxJumps;
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        bool isGrounded = hit != null;

        var horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        var rb = GetComponent<Rigidbody2D>();
        var animator = GetComponent<Animator>();

        if (Mathf.Abs(horizontal) >= 1)
            rb.velocity = new Vector2(horizontal, rb.velocity.y);


        bool running = horizontal != 0;
        animator.SetBool("Run", running);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButtonDown("Jump") && _jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpSpeed );
            _jumpCount--;
            _fallTimer = 0;
        }

        if (isGrounded)
        {
            _fallTimer = 0;
            _jumpCount = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _fallingSpeed * _fallTimer * _fallTimer;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - downForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
