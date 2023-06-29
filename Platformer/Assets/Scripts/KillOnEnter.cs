using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player._fallTimer = 0;
            player.transform.position = new Vector2(-11, -0.06f);
        }
    }
}
