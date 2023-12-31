using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeSoundVolumeButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private void Awake()
    {
        EventBus.Instance.VolumeChanged.AddListener(UpdateIcon);

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        float volume = Repository.Instance?.GameData?.SoundVolume ?? 1;
        UpdateIcon(volume);
    }

    private void UpdateIcon(float volume)
    {
        _icon.sprite = volume > 0 ? _soundOnSprite : _soundOffSprite;
    }
}
