public class DefeatMenu : PanelMenu
{
    private void Awake()
    {
        DefeatChecker.Defeated.AddListener(OpenPanel);
    }

    public void OpenMainMenu()
    {
        OpenScene(SceneNames.MainMenu);
    }

    public void RestartLevel()
    {
        OpenScene(SceneNames.Level);
    }
}
