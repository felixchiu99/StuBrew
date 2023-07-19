using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CurrencyStatsObject")]
public class CurrencyStatsObject : ScriptableObject
{
    public int InitialValue = 0;

    [NonSerialized]
    public int storedAmount;

    public event UnityAction<int> OnValueChange;

    public void OnEnable()
    {
        storedAmount = InitialValue;
    }

    public void Set(int amount)
    {
        storedAmount = amount;
        OnValueChange?.Invoke(storedAmount);
    }

    public void Add(int diff)
    {
        storedAmount += diff;
        OnValueChange?.Invoke(storedAmount);
    }

    public void Deduct(int diff)
    {
        storedAmount -= diff;
        OnValueChange?.Invoke(storedAmount);
    }

    public int GetCurrentStored()
    {
        return storedAmount;
    }

    
}
