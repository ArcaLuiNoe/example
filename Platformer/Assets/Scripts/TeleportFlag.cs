using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportFlag : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
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
        if (player == null || player.PlayerNumber != _playerNumber)
            return;

        _spriteRenderer.sprite = _downSprite;
        flag.transform.position = new Vector3(6.25f, 21.8f, 0);
    }
}
