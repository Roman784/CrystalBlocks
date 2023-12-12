using UnityEngine;

public class Language : MonoBehaviour
{
    public static Language Instance;

    public Languages CurrentLanguage { get; private set; }

    private void Awake()
    {
        Instance = Singleton.Get<Language>();
        DontDestroyOnLoad(gameObject);

    }

    public void Init(string language)
    {
        CurrentLanguage = language == "ru" ? Languages.Ru : Languages.En;
    }
}
