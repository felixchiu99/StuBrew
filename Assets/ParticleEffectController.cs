using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] ParticleSystem part;
    public void Emit(int num = 1)
    {
        var emitParams = new ParticleSystem.EmitParams();
        part.Emit(emitParams, num);
    }

    public void Play()
    {
        part.Play();
        part.enableEmission = true;
    }

    public void Stop()
    {
        part.enableEmission = false;
    }
}
