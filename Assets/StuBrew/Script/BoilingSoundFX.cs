using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingSoundFX : MonoBehaviour
{
    [SerializeField]
    ContinuousAudioMixer audioMixer;

    public void SetTemp(float temp)
    {
        audioMixer.SetBlend(temp / 77);
    }
    public void SetFill(float fill)
    {
        float volume = Mathf.Clamp(fill / 0.2f, 0, 1);
        audioMixer.SetMasterVolume(volume);
    }
}
