public class MainMenu : Menu
{
    public void OpenLevel()
    {
        OpenScene(SceneNames.Level);
    }

    public void ChangeSoundVolume()
    {
        SoundPlayer.Instance?.ChangeVolume();
    }
}
