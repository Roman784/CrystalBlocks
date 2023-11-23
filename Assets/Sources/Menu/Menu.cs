using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    protected void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
