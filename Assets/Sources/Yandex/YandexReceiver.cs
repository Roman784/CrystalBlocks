using UnityEngine;
using UnityEngine.Events;

public class YandexReceiver : MonoBehaviour
{
    public static UnityEvent SDKInited = new UnityEvent();
    public static UnityEvent<string> DataLoaded = new UnityEvent<string>();

    public void InvokeYSDKInitEvent() => SDKInited.Invoke();
    public void InvokeDataLoadEvent(string data) => DataLoaded.Invoke(data);
    public void OnRewarded() => LineCleaner.Instance?.DestroyMiddleBlocks();

    public void StopGame()
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
