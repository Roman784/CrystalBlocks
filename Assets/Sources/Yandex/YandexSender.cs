using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSender : MonoBehaviour
{
    public static YandexSender Instance { get; private set; }

    [DllImport("__Internal")] private static extern void InitYSDKExtern();
    [DllImport("__Internal")] private static extern void SaveDataExtern(string date);
    [DllImport("__Internal")] private static extern void LoadDataExtern();
    [DllImport("__Internal")] private static extern string GetLanguageExtern();
    [DllImport("__Internal")] private static extern void ShowFullscreenAdvExtern();
    [DllImport("__Internal")] private static extern void ShowRewardedVideoExtern();

    private void Awake()
    {
        Instance = Singleton.Get<YandexSender>();
    }

    public void InitYSDK()
    {
        try { InitYSDKExtern(); }
        catch { Debug.Log("Init SDK extern error"); }
    }

    public void SaveData(string data)
    {
        try { SaveDataExtern(data); }
        catch { Debug.Log("Save extern error"); }
    }

    public void LoadData()
    {
        try { LoadDataExtern(); }
        catch { Debug.Log("Load extern error"); }
    }

    public string GetLanguage()
    {
        try { return GetLanguageExtern(); }
        catch { return "en"; }
    }

    public void ShowFullscreenAdv()
    {
        try { ShowFullscreenAdvExtern(); }
        catch { Debug.Log("Full screen adv error"); }
    }

    public void ShowRewardedVideo()
    {
        try { ShowRewardedVideoExtern(); }
        catch { Debug.Log("Rewarded video error"); }
    }
}
