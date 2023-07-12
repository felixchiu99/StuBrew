using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : BuyableItem
{
    [SerializeField] string name;

    public string GetName()
    {
        return name;
    }
}
