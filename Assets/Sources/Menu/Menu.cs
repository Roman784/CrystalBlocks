using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    protected void OpenScene(string name)
    {
        float delay = 0; // Для будущей анимации перехода.
        StartCoroutine(OpenSceneWithDelay(name, delay));
    }

    private IEnumerator OpenSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}
