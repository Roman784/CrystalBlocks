using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSourcer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init(AudioClip clip, float volume)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);

        Destroy(gameObject, clip.length);
    }
}
