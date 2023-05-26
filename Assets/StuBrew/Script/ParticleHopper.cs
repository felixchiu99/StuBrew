using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleHopper : MonoBehaviour
{
    [SerializeField] int particleCount = 0;
    [Tooltip("Maximum Particle storage")]
    [SerializeField] int particleMax = 500;
    [Tooltip("Maximum Particle Overflow storage")]
    [SerializeField] int particleOverflowMax = 500;
    [Tooltip("Particle Loss per second")]
    [SerializeField] int particleLoss = 10;

    [SerializeField] SphereCollider overflowCollider;

    ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        InvokeRepeating("RemoveFromHopper", 1.0f, 1.0f);
    }

    void RemoveFromHopper()
    {
        if (particleLoss == 0)
            return;

        particleCount = Mathf.Clamp(particleCount - particleLoss, 0, particleMax);

        if (text)
            text.SetText(particleCount.ToString());
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);
        part = other.GetComponent<ParticleSystem>();
        var collision = part.collision;
        var trigger = part.trigger;
        int i = 0;
        while (i < numCollisionEvents)
        {
            if (particleCount > particleMax + particleOverflowMax)
            {
                trigger.enabled = false;
                if(overflowCollider)
                    overflowCollider.radius = 0.5f;
            }  
            else if (particleCount >= particleMax && particleCount < particleMax + particleOverflowMax)
            {
                trigger.enabled = true;
                if (overflowCollider)
                    overflowCollider.radius = (float)(particleCount - particleMax) / particleOverflowMax * 0.5f;
                particleCount++;
            }
            else
            {
                trigger.enabled = true;
                if(overflowCollider)
                    overflowCollider.radius = 0f;
                particleCount++;
            }
            i++;
        }

        if (text)
            text.SetText(particleCount.ToString());
    }

    public float GetFillLevel()
    {
        return particleCount == 0 ? 0 : (float)particleCount / particleMax;
    }

    public float GetFillAmount()
    {
        return particleCount;
    }

    public void ClearFill()
    {
        particleCount = 0;
        if (text)
            text.SetText(particleCount.ToString());
    }
}
