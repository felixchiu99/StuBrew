using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioMixerSetting
{
    public ContinuousAudioController controllers;
    public AnimationCurve volumeCurve;
}

public class ContinuousAudioMixer : MonoBehaviour
{
    [SerializeField] AudioMixerSetting[] settings;

    public float volume =1f;
    
    public void Play()
    {
        foreach( AudioMixerSetting setting in settings)
        {
            setting.controllers.Play();
        }
    }
    public void Pause()
    {
        foreach( AudioMixerSetting setting in settings)
        {
            setting.controllers.Pause();
        }
    }

    public void Fade(float fadeDuration)
    {
        foreach (AudioMixerSetting setting in settings)
        {
            setting.controllers.Fade(fadeDuration);
        }
    }

    public void SetVolume(int index, float volume)
    {
        settings[index].controllers.SetVolume(volume);
    }
    public void SetPitch(int index, float pitch)
    {
        settings[index].controllers.SetPitch(pitch);
    }

    public void SetBlend(float blend)
    {
        blend = Mathf.Clamp(blend, 0, 1);
        foreach (AudioMixerSetting setting in settings)
        {
            setting.controllers.SetVolume(EvaluateCurve(setting.volumeCurve, blend) * volume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        this.volume = volume;
    }

    private float EvaluateCurve(AnimationCurve curve, float position)
    {
        return curve.Evaluate(position);
    }


}
