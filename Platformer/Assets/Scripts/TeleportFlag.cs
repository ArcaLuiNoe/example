using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportFlag : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] Transform flag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        else Debug.Log("Colided");

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _downSprite;

        flag.transform.position = new Vector3(9f, 20.8f, 0);
    }
}
