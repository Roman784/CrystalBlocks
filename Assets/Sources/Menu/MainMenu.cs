using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private string _levelName;

    [Space]

    [SerializeField] private TMP_Text _bestScoreRenderer;

    private void Awake()
    {
        Repository.DataLoaded.AddListener(UpdateBestScoreRenderer);
    }

    public void OpenLevel()
    {
        OpenScene(_levelName);
    }

    public void ChangeSoundVolume()
    {
        SoundPlayer.Instance?.ChangeVolume();
    }

    private void UpdateBestScoreRenderer()
    {
        int bestScore = Repository.Instance.GameData.BestScore;
        _bestScoreRenderer.text = bestScore.ToString();
    }
}
