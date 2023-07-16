using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
}

