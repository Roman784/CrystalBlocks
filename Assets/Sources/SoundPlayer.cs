using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    public static UnityEvent<float> VolumeChanged = new UnityEvent<float>();

    public float Volume { get; private set; }

    [SerializeField] private Sound _soundPrefab;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _blocksDestructionSound;
    [SerializeField] private AudioClip _figurePlacementSound;
    [SerializeField] private AudioClip _defeatSound;

    private void Awake()
    {
        Instance = Singleton.Get<SoundPlayer>();

        Repository.DataLoaded.AddListener(LoadData);
    }

    private void LoadData()
    {
        Volume = Repository.Instance?.GameData.SoundVolume ?? 1f;
        VolumeChanged.Invoke(Volume);
    }

    public void ChangeVolume()
    {
        Volume = Volume > 0 ? 0f : 1f;
        VolumeChanged.Invoke(Volume);
    }

    public void PlayButtonClickSound()
    {
        PlaySound(_buttonClickSound);
    }

    public void PlayBlocksDestructionSound()
    {
        PlaySound(_blocksDestructionSound);
    }

    public void PlayFigurePlacementSound()
    {
        PlaySound(_figurePlacementSound);
    }

    public void PlayDefeatSound()
    {
        PlaySound(_defeatSound);
    }

    private void PlaySound(AudioClip clip)
    {
        Sound sound = Instantiate(_soundPrefab);
        sound.Init(clip, Volume);
    }
}
