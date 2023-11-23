using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private string _levelName;

    public void OpenLevel()
    {
        OpenScene(_levelName);
    }

    public void ChangeSoundVolume()
    {
        SoundPlayer.Instance?.ChangeVolume();
    }
}
