using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private int _value;
    [SerializeField] private TMP_Text _valueRenderer;

    private int _bestValue;
    [SerializeField] private TMP_Text _bestValueRenderer;

    public static UnityEvent<int> BestValueChanged = new UnityEvent<int>();

    private void Awake()
    {
        UpdateDisplay();

        Repository.DataLoaded.AddListener(LoadData);
        GameLoop.Defeated.AddListener(UpdateBestValue);
    }

    private void LoadData()
    {
        _bestValue = Repository.Instance.GameData.BestScore;
        UpdateDisplay();
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

        BestValueChanged.Invoke(_bestValue);
    }

    private void UpdateDisplay()
    {
        _valueRenderer.text = _value.ToString();
        _bestValueRenderer.text = _bestValue.ToString();
    }
}
