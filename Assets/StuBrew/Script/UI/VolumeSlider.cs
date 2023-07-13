using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;
    void Awake()
    {
        if (slider == null)
        {
            Debug.Log(slider);
            if (gameObject.TryGetComponent<Slider>(out Slider temp))
            {
                Debug.Log(slider);
                slider = temp;
                OnEnable();
            }
        }
            
    }

    void OnEnable()
    {
        slider.value = audioSource.volume;
    }
}
