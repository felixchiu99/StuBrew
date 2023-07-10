using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BeerPump : MonoBehaviour
{
    [Required]
    [SerializeField] ParticleEffectController particles;
    [SerializeField] float flowRate = 0.0005f;

    [SerializeField] BarrelContainer barrel;
    [SerializeField] CupContainer cup;

    public bool isPulled = false;

    void Start()
    {
        OnReleased();
    }

    void Update()
    {
        if (!isPulled)
        {
            return;
        }  
        Pulled();
    }

    public void AddBarrel(Autohand.PlacePoint placePoint, Autohand.Grabbable obj)
    {
        if (obj.gameObject.TryGetComponent(out BarrelContainer container))
        {
            barrel = container;
        }
    }

    public void RemoveBarrel()
    {
        barrel = null;
    }

    public void AddCup(Autohand.PlacePoint placePoint, Autohand.Grabbable obj)
    {
        if (obj.gameObject.TryGetComponent(out CupContainer container))
        {
            cup = container;
        }
    }

    public void RemoveCup()
    {
        cup = null;
    }

    [Button("TestTransfer")]
    public void Pulled()
    {
        if (Transfer())
        {
            particles.Play();
        }
        else {
            OnReleased();
        }
    }

    public void OnPulled()
    {
        isPulled = true;
    }
    public void OnReleased()
    {
        isPulled = false;
        particles.Stop();
    }

    bool Transfer()
    {
        if(cup != null && barrel != null)
        {
            float transfered = 0;
            if (!cup.isFull())
                transfered = barrel.Remove(flowRate);
            if (transfered <= 0)
                return false;
            cup.Add(transfered);
            cup.UpdateContainerFill();
            return true;
        }
        return false;
    }
}
