using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsColected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null )
            return;

        gameObject.SetActive(false);
        CoinsColected++;
        Debug.Log(CoinsColected);
    }
}
