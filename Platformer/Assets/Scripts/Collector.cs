using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;

    int _countRemaining;

    TMP_Text _remainingText;

    void Start()
    {
        _countRemaining = _collectibles.Count;
        _remainingText = GetComponentInChildren<TMP_Text>();
        _remainingText.SetText(_collectibles.Count.ToString());
        foreach (var collectible in _collectibles)
            collectible.OnPickedUp += ItemWasCollected;
    }

    public void ItemWasCollected()
    {
        _countRemaining--;

        _remainingText?.SetText(_countRemaining.ToString());

        if (_countRemaining <= 0)
            _onCollectionComplete.Invoke();
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }
}