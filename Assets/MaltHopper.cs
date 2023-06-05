using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaltProperties))]
public class MaltHopper : ParticleHopper
{
    MaltProperties malt;

    new void Start()
    {
        base.Start();
        if(!malt)
            malt = GetComponent<MaltProperties>();
    }

    override protected void OnEachParticleCollision(GameObject other)
    {
        CustomTag tag = other.GetComponent<CustomTag>();
        if (!tag)
            return;
        if (!tag.HasTag("Malt"))
            return;
        MaltProperties otherMalt = other.GetComponent<MaltProperties>();
        malt.Blend(otherMalt, (float)1 / particleMax);
    }

    public MaltProperties GetMaltProperties()
    {
        return malt;
    }
}
