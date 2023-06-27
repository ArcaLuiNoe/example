using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _groundCheckLeft;
    [SerializeField] Transform _groundCheckRight;
    [Header("Velocity < 0")]
    [SerializeField] float _velocity = -1f;

    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    Vector2 desiredVelocity;
    float _direction = -1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        desiredVelocity = new Vector2(_velocity, _rb.velocity.y);
    }

    void Update()
    {
        _rb.velocity = new Vector2(_direction, _rb.velocity.y);

        if (_direction < 0)
        {
            Debug.DrawRay(_groundCheckLeft.position, Vector2.down * 0.1f, Color.red);
            var result = Physics2D.Raycast(_groundCheckLeft.position, Vector2.down, 0.1f);
            if (result.collider == null)
                TurnAround();
        }
        else
        {
            Debug.DrawRay(_groundCheckRight.position, Vector2.down * 0.1f, Color.red);
            var result = Physics2D.Raycast(_groundCheckRight.position, Vector2.down, 0.1f);
            if (result.collider == null)
                TurnAround();
        }
    }

    private void TurnAround()
    {
        _direction *= -1;
        _sprite.flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        player.ResetToStart();
    }
}
