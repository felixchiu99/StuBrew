using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioProp
{
    public AudioClip audioClip;
    public float volume = 1f;
}

public class MultipleAudioClip : MonoBehaviour
{
    [SerializeField] AudioProp[] audioClipList;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (audioClipList.Length > index)
        {
            audioSource.PlayOneShot(audioClipList[index].audioClip, audioClipList[index].volume);
        }
    }
}
