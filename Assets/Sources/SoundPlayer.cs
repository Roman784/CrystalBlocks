using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    public static UnityEvent<float> VolumeChanged = new UnityEvent<float>();

    public float Volume { get; private set; }

    [SerializeField] private SoundSourcer _soundSourcerPrefab;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _blocksDestructionSound;
    [SerializeField] private AudioClip _figurePlacementSound;
    [SerializeField] private AudioClip _defeatSound;

    private void Awake()
    {
        Instance = Singleton.Get<SoundPlayer>();

        Repository.DataLoaded.AddListener(LoadData);
    }

    private void Start()
    {
        Figure.Placed.AddListener(PlayFigurePlacementSound);
        LineCleaner.BlocksDestroyed.AddListener(PlayBlocksDestructionSound);
        DefeatChecker.Defeated.AddListener(PlayDefeatSound);
        Button.Clicked.AddListener(PlayButtonClickSound);
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

    private void PlaySound(AudioClip clip)
    {
        SoundSourcer sound = Instantiate(_soundSourcerPrefab);
        sound.Init(clip, Volume);
    }

    private void PlayButtonClickSound()
    {
        PlaySound(_buttonClickSound);
    }

    private void PlayBlocksDestructionSound(int blockCount)
    {
        if (blockCount > 0)
            PlaySound(_blocksDestructionSound);
    }

    private void PlayFigurePlacementSound(Figure _)
    {
        PlaySound(_figurePlacementSound);
    }

    private void PlayDefeatSound()
    {
        PlaySound(_defeatSound);
    }
}
