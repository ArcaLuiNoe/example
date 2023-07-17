using System;
using TMPro;
using UnityEngine;

public class UICoinsCollected : MonoBehaviour
{
    TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void UpdateCoinsUI(int _coinsCollected)
    {
        _text.SetText(_coinsCollected.ToString());
    }
}
