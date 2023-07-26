using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void Play()
    {
        if (audioSource)
            audioSource.Play();
    }
    
    public void Pause()
    {
        if (audioSource)
            audioSource.Pause();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
