public class PauseMenu : PanelMenu
{
    public void OpenMainMenu()
    {
        OpenScene(SceneNames.MainMenu);
    }

    public void ChangeSoundVolume()
    {
        SoundPlayer.Instance?.ChangeVolume();
    }
}
