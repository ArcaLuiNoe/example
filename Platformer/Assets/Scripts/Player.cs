using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        var rb = GetComponent<Rigidbody2D>();
        var animator = GetComponent<Animator>();

        if (Mathf.Abs(horizontal) >= 1)
        { 
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
        }

        bool running = horizontal != 0;
        animator.SetBool("Run", running);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButton("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
