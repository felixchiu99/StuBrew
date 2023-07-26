using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ParticleContainer : MonoBehaviour
{
    [Tooltip("Maximum Particle storage in container")]
    [SerializeField] int maxParticle = 10000;
    [Tooltip("Current Number of Particle stored in container")]
    [SerializeField] int storedParticle = 10000;
    [Tooltip("Particle Emited per second")]
    [SerializeField] int flowRate = 10;
    [Tooltip("Particle Emited per emit")]
    [SerializeField] int emitNum = 1;

    [SerializeField] ParticleSystem part;
    private bool isEmit = false;

    [SerializeField] TextMeshProUGUI display;

    public event UnityAction<float> OnPour;

    private bool hasEmit = false;

    [SerializeField]
    UnityEvent OnPourStart;
    [SerializeField]
    UnityEvent OnPourEnd;
    [SerializeField]
    UnityEvent<float> OnPourStay;

    void Start()
    {
        StartCoroutine(EmitParticle(1 / ((float)flowRate / emitNum)));
    }

    protected int RemoveParticle(int num = 1)
    {
        storedParticle -= num;
        return num;
    }
    IEnumerator EmitParticle(float waitTime)
    {
        if (display)
        {
            display.SetText(storedParticle.ToString());
        }
        if (!isEmit)
        {
            if (hasEmit)
            {
                hasEmit = false;
                OnPourEnd?.Invoke();
            }
        }
        if (storedParticle > 0 && isEmit)
        {
            int emit = 0;
            if(storedParticle > emitNum)
            {
                emit = emitNum;
            }
            else
            {
                emit = storedParticle;
            }
            float volume = 1 - Mathf.InverseLerp(0, 0.09f, waitTime);
            if (emit > 0)
            {
                if (!hasEmit) {
                    hasEmit = true;
                    OnPourStart?.Invoke();
                }
                OnPourStay?.Invoke(volume*0.9f);
            }
            Emit(emit);
            RemoveParticle(emit);
            OnPour?.Invoke(GetFillLevel());
        }
        yield return new WaitForSeconds(waitTime);
        if( flowRate == 0)
        {
            flowRate = 10;
        }
        StartCoroutine(EmitParticle(1 / ((float)flowRate / emitNum)));
    }

    void Emit(int num = 1)
    {
        var emitParams = new ParticleSystem.EmitParams();
        part.Emit(emitParams, num);
    }

    public float GetFillLevel()
    {
        return (float)storedParticle / maxParticle;
    }
    public void SetFillLevel(float fillLevel)
    {
        SetFill((int)(maxParticle * fillLevel));
    }

    public void SetFlowRate(int particlePerSecond)
    {
        flowRate = particlePerSecond;
    }

    public void Play()
    {
        if (storedParticle > 0)
        {
            isEmit = true;
        }
        else
        {
            isEmit = false;
        }
    }
    
    public void Stop()
    {
        isEmit = false;
    }

    public void ClearFill()
    {
        storedParticle = 0;
    }

    public void SetFill(int fill)
    {
        storedParticle = fill > maxParticle ? maxParticle: fill;
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
