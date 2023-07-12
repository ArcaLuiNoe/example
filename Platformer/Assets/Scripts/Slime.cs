using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] Sprite _deadSprite;

    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    float _direction = -1;
    bool _isAlive;

    void Start()
    {
        _isAlive = true;
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_isAlive)
            _rb.velocity = new Vector2(_direction, _rb.velocity.y);

        if (_direction < 0)
            ScanSensor(_leftSensor);
        else
            ScanSensor(_rightSensor);

    }

    private void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    void TurnAround()
    {
        _direction *= -1;
        _sprite.flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y <= -0.5)
        {
            StartCoroutine(Die());
        }
        else
            player.ResetToStart();
    }

    IEnumerator Die()
    {
        _isAlive = false;
        _sprite.sprite = _deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        float alpha = 1;

        while (alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime / 2;
            _sprite.color = new Color(1, 1, 1, alpha);
            Destroy(gameObject, 2f);
        }
    }
}
