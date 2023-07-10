using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;
    [SerializeField] int _requiredCoins = 3;
    [SerializeField] Door _exit;

    bool _open;

    [ContextMenu("Open Door")]
    void Open()
    {
        _open = true;
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
    }

    void Update()
    {
        if (_open == false && Coin.CoinsColected >= _requiredCoins)
            Open();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_open == false) 
            return;
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }
    }
}
