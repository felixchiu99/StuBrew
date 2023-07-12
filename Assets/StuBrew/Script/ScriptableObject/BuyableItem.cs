using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyableItem")]
public class BuyableItem : ScriptableObject
{
    [SerializeField] float sellingPrice;

    public float GetSellingPrice()
    {
        return sellingPrice;
    }

}

