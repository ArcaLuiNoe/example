using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float _fallSpeed = 12f;
    [Range(0.1f, 5f)][SerializeField] float fallAfterSeconds = 3f;
    [Range(0.005f, 0.1f)][SerializeField] float shakeX = 0.05f;
    [Range(0.005f, 0.1f)][SerializeField] float shakeY = 0.05f;
    [Tooltip("Reset the wiggle timer when no player is standing on the platform")]
    [SerializeField] bool _resetOnEmpty;

    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Coroutine _coroutine;
    Vector3 _initialPosition;
    public bool PlayerInside;


    bool _isFalling;
    float wiggleTimer = 0f;
    float fallTimer = 0f;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Add(player); 
        PlayerInside = true;

        if (_playersInTrigger.Count == 1)
            _coroutine = StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        yield return new WaitForSeconds(0.25f);

        while (wiggleTimer < fallAfterSeconds)
        {
            float randomX = UnityEngine.Random.Range(-shakeX, shakeX);
            float randomY = UnityEngine.Random.Range(-shakeY, shakeY);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }

        _isFalling = true;
        foreach (var collider in GetComponents<Collider2D>())
            collider.enabled = false;

        while (fallTimer < 0.25f)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_isFalling)
            return;

        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(_coroutine);

            if (_resetOnEmpty)
                wiggleTimer = 0;
        }
    }
}
