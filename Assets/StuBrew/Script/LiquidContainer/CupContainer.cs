using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CupContainer : FluidContainer
{
    [Required]
    [SerializeField] ParticleContainer particleContainer;
    [MinValue(0), MaxValue(10)]
    [SerializeField] float SellMinFill = 0.9f;
    //[Button("TestContainer")]
    public void UpdateContainerFill()
    {
        particleContainer.SetFillLevel(currentStored / volume);
    }

    public bool isSellable()
    {
        return GetFillLevel() >= SellMinFill;
    }

    private void SyncFillLevel(float fillLevel)
    {
        SetFill(fillLevel);
        Debug.Log("fillLevel "+ fillLevel);
    }
    private void Start()
    {
        particleContainer.OnPour += SyncFillLevel;
    }
    private void OnDestroy()
    {
        particleContainer.OnPour -= SyncFillLevel;
    }
}
