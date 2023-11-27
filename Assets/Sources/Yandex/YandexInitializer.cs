using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexInitializer : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void InitExtern();

    private void Awake()
    {
        try
        {
            InitExtern();
        } 
        catch
        {
            Debug.Log("YSDK init error");
            OpenMainMenu();
        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
