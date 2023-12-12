using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenAdvView : MonoBehaviour
{
    private void Start()
    {
        YandexSender.Instance.ShowFullscreenAdv();
    }
}
