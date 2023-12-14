public class DefeatMenu : PanelMenu
{
    public void OpenMainMenu()
    {
        OpenScene(SceneNames.MainMenu);
    }

    public void RestartLevel()
    {
        OpenScene(SceneNames.Level);
    }
}
