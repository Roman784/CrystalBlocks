using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : Menu
{
    [SerializeField] private GameObject _panel;

    [Space]

    [SerializeField] private string _mainMenuName;

    public void OpenMenuPanel()
    {
        _panel.SetActive(true);
    }

    public void CloseMenuPanel()
    {
        _panel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        OpenScene(_mainMenuName);
    }

    public void ChangeSound()
    {

    }
}
