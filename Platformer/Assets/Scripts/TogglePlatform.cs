using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TogglePlatform : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] UnityEvent _onEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _downSprite;

        _onEnter?.Invoke();
    }
}
