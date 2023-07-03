using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] ParticleSystem part;
    void Start()
    {
        var em = part.emission;
        em.enabled = true;
    }
    
    public void Emit(int num = 1)
    {
        var emitParams = new ParticleSystem.EmitParams();
        part.Emit(emitParams, num);
    }

    public void Play()
    {
        part.Play();
        var em = part.emission;
        em.enabled = true;
    }

    public void Stop()
    {
        var em = part.emission;
        em.enabled = false;
    }
}
