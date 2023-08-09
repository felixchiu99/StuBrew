using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] TextMeshProUGUI displayText2;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI costText2;

    [SerializeField] FloatingNumAni floatingNumAni;

    private bool canSpawn = true;

    [Tooltip("SFX index : 0 - buy, 1 - prev, 2 - next, 3 - buyFail, 4 - spawn(no cost)")]
    [SerializeField]
    protected UnityEvent<int> playSFX;

    [SerializeField]
    GameObject prevBtn;
    [SerializeField]
    GameObject nextBtn;

    void Start()
    {
        GetText();
        DisplayText();
        if(objList.Count <= 1)
        {
            if (prevBtn)
                prevBtn.SetActive(false);
            if (nextBtn)
                nextBtn.SetActive(false);
        }

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
        if(displayText2)
            displayText2.SetText(objList[selector].name);
        if (costText2)
            costText2.SetText(objList[selector].info.GetItem().GetSellingPrice().ToString("0"));
    }

    public void NextItem()
    {
        selector++;
        if (selector >= objList.Count)
        {
            selector = 0;
        }
        DisplayText();
        playSFX?.Invoke(2);
    }
    public void PrevItem()
    {
        selector--;
        if (selector < 0)
        {
            selector = objList.Count-1;
        }
        DisplayText();
        playSFX?.Invoke(1);
    }

    public void BuyItem()
    {
        if (!canSpawn)
            return;
        if (objList.Count == 0)
            return;

        bool transectionSuccess = CurrencyManager.Instance.Deduct((int)objList[selector].info.GetItem().GetSellingPrice());
        if (transectionSuccess)
        {
            if ((int)objList[selector].info.GetItem().GetSellingPrice() > 0)
            {
                floatingNumAni.SpawnFloatingText("-$" + ((int)objList[selector].info.GetItem().GetSellingPrice()).ToString());
                playSFX?.Invoke(0);
            }
            else
                playSFX?.Invoke(4);
            SpawnItem();
        } 
        else
            playSFX?.Invoke(3);
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
