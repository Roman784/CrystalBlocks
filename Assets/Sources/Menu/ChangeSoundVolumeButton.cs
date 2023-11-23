using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundVolumeButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private void Awake()
    {
        SoundPlayer.VolumeChanged.AddListener(UpdateIcon);
    }

    private void UpdateIcon(float volume)
    {
        _icon.sprite = volume > 0 ? _soundOnSprite : _soundOffSprite;
    }
}
