using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Rigidbody2D _slimeRb;

    void Start()
    {
        _slimeRb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        _slimeRb.velocity = Vector2.left;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        player.ResetToStart();
    }
}
