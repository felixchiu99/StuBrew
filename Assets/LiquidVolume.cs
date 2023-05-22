using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidVolume : MonoBehaviour
{
    [SerializeField] float maxVolume = 1000;
    [SerializeField] float currentVolume = 0;
    [SerializeField]
    private AnimationCurve volumeCompensate;

    public float GetFillLevel()
    {
        return currentVolume / maxVolume;
    }
}
