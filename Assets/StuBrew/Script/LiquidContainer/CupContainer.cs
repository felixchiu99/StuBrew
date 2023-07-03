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
}
