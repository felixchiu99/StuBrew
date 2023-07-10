using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public CurrencyStatsObject currency;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    public int GetCurrentStored()
    {
        return currency.GetCurrentStored();
    }

    public void Add (int diff)
    {
        currency.Add(diff);
    }

    public void Deduct(int diff)
    {
        currency.Deduct(diff);
    }
}
