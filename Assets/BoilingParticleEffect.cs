using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleEffectController controller;


    public void ChangeShapeLength(float length)
    {
        var newLength = Mathf.Lerp(0, 5.2f, length);
        controller.ChangeShapeLength(newLength);
        if(newLength < 1f)
            controller.ChangeRate(0);
    }
    public void ChangeRate(float rate)
    {
        controller.ChangeRate(rate/3);
    }
}
