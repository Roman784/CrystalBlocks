using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private YandexSender _yandexSender;

    private void Start()
    {
        _yandexSender = YandexSender.Instance;

        YandexReceiver.SDKInited.AddListener(_yandexSender.LoadData);
        YandexReceiver.SDKInited.AddListener(InitLanguage);
        YandexReceiver.DataLoaded.AddListener(InitRepository);
        EventBus.Instance.DataLoaded.AddListener(OpenMainMenu);

        _yandexSender.InitYSDK();
    }

    private void InitRepository(string data)
    {
        Repository.Instance.Load(data);
    }

    private void InitLanguage()
    {
        string language = _yandexSender.GetLanguage();
        Language.Instance.Init(language);
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
