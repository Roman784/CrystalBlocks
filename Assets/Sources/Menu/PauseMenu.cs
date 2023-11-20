using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : PanelMenu
{
    [SerializeField] private string _mainMenuName;

    [Space]

    [SerializeField] private Image _soundIcon;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private void Awake()
    {
        SoundPlayer.VolumeChanged.AddListener(UpdateSoundIcon);
    }

    public void OpenMainMenu()
    {
        OpenScene(_mainMenuName);
    }

    public void ChangeSoundVolume()
    {
        SoundPlayer.Instance?.ChangeVolume();
    }

    private void UpdateSoundIcon(float volume)
    {
        _soundIcon.sprite = volume > 0 ? _soundOnSprite : _soundOffSprite;
    }
}
