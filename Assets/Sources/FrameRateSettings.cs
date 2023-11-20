using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeInitialized()
    {
        Application.targetFrameRate = 60;
    }
}
