using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private bool isFading = false;
    public void Play()
    {
        if (audioSource)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            isFading = false;
        }
            
    }
    
    public void Pause()
    {
        if (audioSource)
            if (audioSource.isPlaying)
                audioSource.Pause();
    }

    public void Fade(float fadeDuration)
    {
        if (audioSource)
            if (audioSource.isPlaying)
            {
                isFading = true;
                StartCoroutine(FadeOut(audioSource, fadeDuration));
            }
    }

    public void SetVolume(float volume)
    {
        if(!isFading)
            audioSource.volume = volume;
        //Debug.Log("fade" + isFading);
    }
    
    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            if (isFading == false)
                break;
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        isFading = false;
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
