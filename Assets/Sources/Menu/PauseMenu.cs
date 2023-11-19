using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : PanelMenu
{
    [SerializeField] private string _mainMenuName;

    public void OpenMainMenu()
    {
        OpenScene(_mainMenuName);
    }

    public void ChangeSound()
    {

    }
}
