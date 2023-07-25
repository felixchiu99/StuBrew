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
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }
    

    public int GetCurrentStored()
    {
        return currency.GetCurrentStored();
    }

    public void SetCurrentStored(int num)
    {
        currency.Set(num);
    }

    public void Add(int diff)
    {
        currency.Add(diff);
    }

    public bool Deduct(int diff)
    {
        if (currency.GetCurrentStored() - diff < 0)
            return false;
        currency.Deduct(diff);
        return true;
    }
}
