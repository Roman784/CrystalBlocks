using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    public static UnityEvent<float> VolumeChanged = new UnityEvent<float>();

    public float _volume;

    [SerializeField] private SoundSourcer _soundSourcerPrefab;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _blocksDestructionSound;
    [SerializeField] private AudioClip _figurePlacementSound;
    [SerializeField] private AudioClip _defeatSound;

    private void Start()
    {
        Instance = Singleton.Get<SoundPlayer>();

        Figure.Placed.AddListener(PlayFigurePlacementSound);
        LineCleaner.BlocksDestroyed.AddListener(PlayBlocksDestructionSound);
        DefeatChecker.Defeated.AddListener(PlayDefeatSound);
        Button.Clicked.AddListener(PlayButtonClickSound);

        UpdateVolume();
    }

    private void UpdateVolume()
    {
        _volume = Repository.Instance.GameData.SoundVolume;
        VolumeChanged.Invoke(_volume);
    }

    public void ChangeVolume()
    {
        _volume = _volume > 0 ? 0f : 1f;
        VolumeChanged.Invoke(_volume);

        Repository.Instance?.SetSoundVolume(_volume);
    }

    private void PlaySound(AudioClip clip)
    {
        SoundSourcer sound = Instantiate(_soundSourcerPrefab);
        sound.Init(clip, _volume);
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
