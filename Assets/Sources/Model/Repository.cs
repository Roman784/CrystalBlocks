using UnityEngine;

public class Repository : MonoBehaviour
{
    public static Repository Instance;

    public GameData GameData { get; private set; }
    [SerializeField] private GameData _defaultGameData;

    private void Awake()
    {
        Instance = Singleton.Get<Repository>();
        DontDestroyOnLoad(gameObject);
    }

    private void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(GameData, true);
            Debug.Log("Save json:\n" + json);
            YandexSender.Instance.SaveData(json);

            Debug.Log("Save data complete");
        }
        catch { Debug.Log("Save data error"); }
    }

    public void Load(string data)
    {
        try
        {
            GameData = JsonUtility.FromJson<GameData>(data);

            if (GameData == null || data == null || data == "{}" || data == "")
            {
                DefaultData();
            }

            Debug.Log("Load data complete");
        }
        catch 
        { 
            Debug.Log("Load data error");
            DefaultData();
        }

        EventBus.Instance.DataLoaded.Invoke();
    }

    private void DefaultData()
    {
        GameData = new GameData()
        {
            BestScore = _defaultGameData.BestScore,
            SoundVolume = _defaultGameData.SoundVolume
        };
    }

    public void SetBestScore(int value)
    {
        if (value == GameData.BestScore) return;

        GameData.BestScore = value;
        Save();
    }

    public void SetSoundVolume(float value)
    {
        if (value == GameData.SoundVolume) return;

        GameData.SoundVolume = value;
        Save();
    }
}
