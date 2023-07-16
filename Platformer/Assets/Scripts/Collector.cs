using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
	[SerializeField] List<Collectible> _collectibles;

    [SerializeField] UnityEvent _onCollectionComplete;
    TMP_Text _remainingText;

    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        int _countRemaining = 0;
        foreach (var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
                _countRemaining++;
        }

        _remainingText?.SetText(_countRemaining.ToString());

        if (_countRemaining > 0)
            return;

        _onCollectionComplete.Invoke();
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }
}
