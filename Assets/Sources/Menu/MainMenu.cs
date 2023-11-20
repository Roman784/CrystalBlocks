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

    [Space]

    [SerializeField] private Image _soundIcon;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private void Awake()
    {
        Repository.DataLoaded.AddListener(UpdateBestScoreRenderer);
        SoundPlayer.VolumeChanged.AddListener(UpdateSoundIcon);
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

    private void UpdateSoundIcon(float volume)
    {
        _soundIcon.sprite = volume > 0 ? _soundOnSprite : _soundOffSprite;
    }
}
