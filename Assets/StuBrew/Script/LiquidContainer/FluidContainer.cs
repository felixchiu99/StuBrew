using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidContainer : MonoBehaviour
{
    [Tooltip("Volume")]
    [SerializeField] protected float volume = 1f;
    [Tooltip("Current Stored Volume")]
    [SerializeField] protected float currentStored = 0f;

    public float Add(float toAdd)
    {
        float add = 0f;
        add = currentStored + toAdd < volume ? toAdd : volume - currentStored;
        currentStored = currentStored + add;
        return add;
    }
    public float Remove(float toRemove)
    {
        float removed = 0f;
        removed = currentStored - toRemove > 0 ? toRemove : currentStored;
        currentStored = currentStored - removed;
        return removed;
    }

    protected void SetFill(float fillLevel)
    {
        currentStored = volume * fillLevel;
    }

    public bool isEmpty()
    {
        return currentStored <= 0;
    }
    public bool isFull()
    {
        return currentStored >= volume;
    }

    public float GetFillLevel()
    {
        return currentStored / volume;
    }
}
