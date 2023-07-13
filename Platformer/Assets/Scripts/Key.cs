using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock _keyLock;

    bool _isTaken;
    Fly fly;

    private void Start()
    {
        fly = GetComponent<Fly>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && _isTaken == false)
        {
            fly.enabled = false;
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
            _isTaken = true;
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if (keyLock == null)
            return;
        else if (keyLock == _keyLock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
