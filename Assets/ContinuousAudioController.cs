using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void Play()
    {
        if (audioSource)
            if(!audioSource.isPlaying)
                audioSource.Play();
    }
    
    public void Pause()
    {
        if (audioSource)
            if (audioSource.isPlaying)
                audioSource.Pause();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
