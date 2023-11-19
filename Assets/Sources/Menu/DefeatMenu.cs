using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMenu : PanelMenu
{
    [SerializeField] private string _mainMenuName;
    [SerializeField] private string _levelName;

    private void Awake()
    {
        GameLoop.Defeated.AddListener(OpenPanel);
    }

    public void OpenMainMenu()
    {
        OpenScene(_mainMenuName);
    }

    public void RestartLevel()
    {
        OpenScene(_levelName);
    }
}
