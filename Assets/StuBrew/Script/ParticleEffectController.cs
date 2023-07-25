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

    public void ChangeColorOvertime(Color colorStart, Color colorEnd, float alphaStart, float alphaEnd)
    {
        var col = part.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(colorStart, 0.0f), new GradientColorKey(colorEnd, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(alphaStart, 0.0f), new GradientAlphaKey(alphaEnd, 1.0f) });

        col.color = grad;
    }
}
