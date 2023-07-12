using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinsColected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null )
            return;

        gameObject.SetActive(false);
        coinsColected++;
    }
}
