using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinsCollected = 1;
    UICoinsCollected _uiCoinsCollected;

    void Start()
    {
        _uiCoinsCollected = GameObject.Find("Coins Collected Text").GetComponent<UICoinsCollected>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null )
            return;

        
        gameObject.SetActive(false);
        _uiCoinsCollected.UpdateCoinsUI(coinsCollected++);
    }
}
