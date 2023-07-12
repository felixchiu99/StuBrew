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
}

public class ItemDispenser : MonoBehaviour
{
    [SerializeField]
    private List<DispensableObject> objList;

    [SerializeField] private Transform spawnPoint;
    private int selector = 0;
    [SerializeField] TextMeshProUGUI displayText;

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
            }
        }
    }

    private void DisplayText()
    {
        if(displayText)
            displayText.SetText(objList[selector].name);
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
        if (objList[selector].prefab.TryGetComponent(out ItemInfo item))
        {
            CurrencyManager.Instance.Deduct((int)item.GetItem().GetSellingPrice());
        }
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
