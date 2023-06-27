using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;

    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    float _direction = -1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(_direction, _rb.velocity.y);

        if (_direction < 0)
            ScanSensor(_leftSensor);
        else
            ScanSensor(_rightSensor);

    }

    private void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    void TurnAround()
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
