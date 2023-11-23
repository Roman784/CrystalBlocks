using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public class GameData
{
    public int BestScore;
    public float SoundVolume;
}

public class Repository : MonoBehaviour
{
    public static Repository Instance;

    public static UnityEvent DataLoaded = new UnityEvent();

    public GameData GameData { get; private set; }
    [SerializeField] private GameData _defaultGameData;

    [SerializeField] private string _saveFileName;
    private string _savePath;

    private void Awake()
    {
        Instance = Singleton.Get<Repository>();

        SetSavePath();

        ScoreCounter.BestScoreChanged.AddListener(SetBestScore);
        SoundPlayer.VolumeChanged.AddListener(SetSoundVolume);
    }

    private void Start()
    {
        Load(); // В Awake остальные классы подписываются на загрузку, поэтому вызов в Start.
    }

    private void Save()
    {
        try
        {
            string json = JsonUtility.ToJson (GameData, true);
            File.WriteAllText (_savePath, json);

            Debug.Log ("Save data");
        }
        catch { Debug.Log ("Save data error"); }
    }

    private void Load()
    {
        if (!File.Exists(_savePath))
        {
            Debug.Log("File not exist");

            ClearAll();
        }

        try
        {
            string json = File.ReadAllText(_savePath);
            GameData = JsonUtility.FromJson<GameData>(json);

            DataLoaded.Invoke();

            Debug.Log("Load data");
        }
        catch { Debug.Log("Load data error"); }
    }

    [ContextMenu("ClearAll")]
    private void ClearAll()
    {
        GameData = _defaultGameData;
        Save();
    }

    private void SetBestScore(int value)
    {
        if (value == GameData.BestScore) return;

        GameData.BestScore = value;
        Save();
    }

    private void SetSoundVolume(float value)
    {
        if (value == GameData.SoundVolume) return;

        GameData.SoundVolume = value;
        Save();
    }

    private void SetSavePath()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            _savePath = Path.Combine (Application.persistentDataPath, _saveFileName);
        #else
            _savePath = Path.Combine(Application.dataPath, _saveFileName);
        #endif
    }
}
