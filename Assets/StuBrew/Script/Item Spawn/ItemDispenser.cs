using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DispensableObject
{
    [Header("Object")]
    [NonSerialized]
    public string name;
    public GameObject prefab;

    [NonSerialized]
    public ItemInfo info;
}

public class ItemDispenser : MonoBehaviour
{
    [SerializeField]
    private List<DispensableObject> objList;

    [SerializeField] private Transform spawnPoint;
    private int selector = 0;
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] TextMeshProUGUI costText;

    private bool canSpawn = true;

    void Start()
    {
        GetText();
        DisplayText();
    }

    void GetText()
    {
        foreach (DispensableObject obj in objList)
        {
            if (obj.prefab.TryGetComponent(out ItemInfo item))
            {

                obj.name = item.GetItem().GetName();
                obj.info = item;
            }
        }
    }

    private void DisplayText()
    {
        if(displayText)
            displayText.SetText(objList[selector].name);
        if (costText)
            costText.SetText(objList[selector].info.GetItem().GetSellingPrice().ToString("0"));
    }

    public void NextItem()
    {
        selector++;
        if (selector >= objList.Count)
        {
            selector = 0;
        }
        DisplayText();
    }
    public void PrevItem()
    {
        selector--;
        if (selector < 0)
        {
            selector = objList.Count-1;
        }
        DisplayText();
    }

    public void BuyItem()
    {
        if (!canSpawn)
            return;
        if (objList.Count == 0)
            return;

        CurrencyManager.Instance.Deduct((int)objList[selector].info.GetItem().GetSellingPrice());
        SpawnItem();
    }

    public void SpawnItem()
    {
        GameObject obj = Instantiate(objList[selector].prefab, spawnPoint.position, Quaternion.identity);
    }

    public void OnTriggerStay(Collider other)
    {
        canSpawn = false;
    }

    public void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }
}
