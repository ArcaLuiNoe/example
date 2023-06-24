using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed;
        var rb = GetComponent<Rigidbody2D>();
        var animator = GetComponent<Animator>();

        rb.velocity = new Vector2(horizontal, rb.velocity.y);
        bool running = horizontal != 0;
        animator.SetBool("Run", running);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }
    }
}
