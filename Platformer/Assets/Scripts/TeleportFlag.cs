using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportFlag : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] Transform flag;
    [SerializeField] UnityEvent _onPress;
    [SerializeField] UnityEvent _onRelease;

    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _onPress?.Invoke();
        
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _spriteRenderer.sprite = _downSprite;
        flag.transform.position = new Vector3(9f, 20.8f, 0);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _onRelease?.Invoke();
    }
}
