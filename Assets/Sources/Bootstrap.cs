using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private YandexSender _yandexSender;

    private void Start()
    {
        YandexReceiver.SDKInited.AddListener(LoadData);
        Repository.DataLoaded.AddListener(OpenMainMenu);

        _yandexSender.InitYSDK();
    }

    private void LoadData() => _yandexSender.LoadData();

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
