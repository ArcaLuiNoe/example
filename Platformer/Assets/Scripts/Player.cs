using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _groundCheck;

    int _jumpCount;

    private void Start()
    {
        _startPosition = transform.position;
        _jumpCount = _maxJumps;
    }

    void Update()
    {
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
            rb.AddForce(Vector2.up * _jumpForce);
            _jumpCount--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        if (isGrounded != null)
            _jumpCount = _maxJumps;

    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
