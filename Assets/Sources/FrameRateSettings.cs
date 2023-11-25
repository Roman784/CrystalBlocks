using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeInitialized()
    {
        Application.targetFrameRate = 60;
    }
}
