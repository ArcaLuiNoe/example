using Unity.VisualScripting;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2.5f;
    [SerializeField] float _speed = 2.5f;


    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        var distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= _maxDistance)
        {
            transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }

}
