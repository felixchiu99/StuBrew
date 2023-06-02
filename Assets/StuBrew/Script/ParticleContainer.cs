using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        
        if (storedParticle > 0 && isEmit)
        {
            if(storedParticle > emitNum)
            {
                Emit(emitNum);
                RemoveParticle(emitNum);
            }
            else
            {
                Emit(storedParticle);
                RemoveParticle(storedParticle);
            }
            
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
}
