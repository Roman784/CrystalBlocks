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

        Repository.DataLoaded.AddListener(LoadData);
    }

    public void Increase(int destroyedBlocksCount)
    {
        _value += destroyedBlocksCount * 10;
        UpdateDisplay();
    }

    public void UpdateBestValue()
    {
        if (_value <= _bestValue) return;

        _bestValue = _value;
        UpdateDisplay();

        Repository.Instance.SetBestScore(_bestValue);
    }

    private void UpdateDisplay()
    {
        _valueRenderer.text = _value.ToString();
        _bestValueRenderer.text = _bestValue.ToString();
    }

    private void LoadData()
    {
        _bestValue = Repository.Instance.GameData.BestScore;
        UpdateDisplay();
    }
}
