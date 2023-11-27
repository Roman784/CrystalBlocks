using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance;

    [DllImport("__Internal")] private static extern void ShowRewardedVideoExtern();

    private void Awake()
    {
        Instance = Singleton.Get<Yandex>();

        DontDestroyOnLoad(gameObject);
    }

    public void ShowRewardedVideo()
    {
        try
        {
            ShowRewardedVideoExtern();
        } 
        catch 
        { 
            Debug.Log("Rewarded video error");
            OnRewarded();
        }
    }
    public void OnRewarded()
    {
        LineCleaner.Instance.DestroyMiddleBlocks();
    }

    public void PauseGame()
    {
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }
    public void ContinueGame()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
    }
}
