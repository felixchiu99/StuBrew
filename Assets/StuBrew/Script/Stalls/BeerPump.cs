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

    [SerializeField] LiquidProperties liqProp;

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
        if (obj.gameObject.TryGetComponent(out LiquidProperties liqProp))
        {
            this.liqProp = liqProp;
            if(cup != null)
            {
                cup.SetColor(liqProp);
            }
        }
    }

    public void RemoveBarrel()
    {
        barrel = null;
        liqProp = null;
    }

    public void AddCup(Autohand.PlacePoint placePoint, Autohand.Grabbable obj)
    {
        if (obj.gameObject.TryGetComponent(out CupContainer container))
        {
            cup = container;
            if(liqProp != null)
            {
                cup.SetColor(liqProp);
            }
                
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
            //liq.UpdateColor();
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
