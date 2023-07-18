using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinBox : MonoBehaviour
{
    [SerializeField] Sprite _boxEmptySprite;
    [SerializeField] int _totalCoins = 3;
    [SerializeField] GameObject _coinPrefab;

    UICoinsCollected _uiCoinsCollected;
    Rigidbody2D _rbPlayer;
    Vector2 _startPosition;

    Vector2 _direction = Vector2.up;
    float _maxDistance = 2.5f;
    float _speed = 2.5f;
    int _remainingCoins;
    int _playerRemainingJumps;

    void Start()
    {
        _startPosition = transform.position;
        _remainingCoins = _totalCoins;
        _uiCoinsCollected = GameObject.Find("Coins Collected Text").GetComponent<UICoinsCollected>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        _playerRemainingJumps = player._jumpCount;
        player._jumpCount--;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
            player._jumpCount = _playerRemainingJumps;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;
        _rbPlayer = player.GetComponent<Rigidbody2D>();

        if (collision.contacts[0].normal.y > 0 && _remainingCoins > 0)
            StartCoinRoutine();
        else if (collision.contacts[0].normal.y > 0 && _remainingCoins <= 0)
            Break();
    }

    void Animate()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        var distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= _maxDistance)
        {
            transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }

        StartCoroutine(WaitForReset());
    }

    void InstantiateCoin()
    {
        Vector2 _coinPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);

        GameObject _coin = Instantiate(_coinPrefab, _coinPosition, transform.rotation);
        Rigidbody2D _rbCoin = _coin.GetComponent<Rigidbody2D>();
        SpriteRenderer _srCoin = _coin.GetComponent<SpriteRenderer>();

        _rbCoin.velocity = new Vector2(_rbCoin.velocity.x, 3.5f);
        StartCoroutine(ReduceCoinAlpha(_srCoin, _coin));
    }

    IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(0.15f);
        transform.position = _startPosition;
    }

    IEnumerator ReduceCoinAlpha(SpriteRenderer _srCoin, GameObject _coin)
    {
        float _alpha = 1f;
        while (_alpha > 0)
        {
            yield return null;
            if (_srCoin != null && _coin != null)
            {
                _alpha -= Time.deltaTime * 2;
                _srCoin.color = new Color(1, 1, 1, _alpha);
            }
            Destroy(_coin, 1f);
        }
    }

    void StartCoinRoutine()
    {
        _rbPlayer.velocity = new Vector2(_rbPlayer.velocity.x, -8f);
        InstantiateCoin();
        Animate();
        _remainingCoins--;
        _uiCoinsCollected.UpdateCoinsUI(Coin.coinsCollected++);

        if (_remainingCoins <= 0)
            GetComponent<SpriteRenderer>().sprite = _boxEmptySprite;
    }

    void Break()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 1.5f);
    }
}
