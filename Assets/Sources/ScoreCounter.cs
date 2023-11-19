using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _value;
    [SerializeField] private TMP_Text _valueRenderer;

    private int _bestValue;
    [SerializeField] private TMP_Text _bestValueRenderer;

    private void Awake()
    {
        UpdateDisplay();
    }

    public void Increase(int destroyedBlocksCount)
    {
        _value += destroyedBlocksCount * 10;

        UpdateDisplay();
    }

    private void UpdateBestValue()
    {
        if (_value <= _bestValue) return;

        _bestValue = _value;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _valueRenderer.text = _value.ToString();
        _bestValueRenderer.text = _bestValue.ToString();
    }
}
