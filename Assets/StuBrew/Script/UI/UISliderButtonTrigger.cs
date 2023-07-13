using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderButtonTrigger : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float increment = 0.05f;

    void Awake()
    {
        if(slider == null)
            if (gameObject.TryGetComponent<Slider>(out Slider temp))
            {
                slider = temp;
            }
    }

    public void Increase()
    {
        slider.value += increment;
    }

    public void Decrease()
    {
        slider.value -= increment;
    }
}
