using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField] private string _levelName;

    [Space]

    [SerializeField] private TMP_Text _bestScoreRenderer;

    private void Awake()
    {
        Repository.DataLoaded.AddListener(LoadData);
    }

    public void OpenLevel()
    {
        OpenScene(_levelName);
    }

    public void ChangeSound()
    {

    }

    private void LoadData()
    {
        int bestScore = Repository.Instance.GameData.BestScore;
        _bestScoreRenderer.text = bestScore.ToString();
    }
}
