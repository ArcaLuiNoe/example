using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
     Collector _collector;
     static int _countCollected = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        
        gameObject.SetActive(false);
        _collector.ItemWasCollected(_countCollected++);
    }
    internal void SetCollector(Collector collector)
    {
        _collector = collector;
    }
}