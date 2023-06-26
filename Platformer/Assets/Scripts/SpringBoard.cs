using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10f;
    [SerializeField] Sprite _upSprite;

    SpriteRenderer _spriteRenderer;
    Sprite _downSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _downSprite = _spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, _bounceVelocity);
                _spriteRenderer.sprite = _upSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
            StartCoroutine(SpringBoardReset());

    }

    IEnumerator SpringBoardReset()
    {
        yield return new WaitForSeconds(0.6f);
        _spriteRenderer.sprite = _downSprite;
    }
}
