using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SellingArea : ItemInArea
{
    [SerializeField] Material[] materialList;
    [SerializeField] Renderer objRenderer;

    [SerializeField] QueueManager queueManager;

    [SerializeField] UnityEvent<GameObject> OnSell;

    [Tooltip("SFX index : 0 - sell, 1 - buyFail")]
    [SerializeField]
    protected UnityEvent<int> playSFX;

    [SerializeField]
    private int sellingPrice = 2;

    [SerializeField]
    GameObject floatingNumPrefab;
    [SerializeField]
    Transform floatingNumSpawn;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (!queueManager)
            queueManager = (QueueManager)FindObjectOfType(typeof(QueueManager));
    }

    public void SellItem()
    {
        if (!CheckIfEmpty() && queueManager.HasQueue())
        {
            playSFX?.Invoke(0);
            GameObject delObj = RemoveFirst();
            int price = (int)(sellingPrice * queueManager.GetBonus(delObj));
            CurrencyManager.Instance.Add(price);
            SpawnFloatingNum(price);
            OnSell?.Invoke(delObj);
            queueManager.SellObj(delObj);
            //Destroy(delObj);
            ChangeMaterialIfEmpty();
            queueManager.RemoveFromQueue();
        }
        else
        {
            playSFX?.Invoke(1);
        }
    }

    private void SpawnFloatingNum(int num)
    {
        GameObject floatingNum = Instantiate(floatingNumPrefab, floatingNumSpawn.position, Quaternion.Euler(0, 180, 0));
        floatingNum.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("$ " + num);
    }

    public void OnTriggerEnterEvent(Collider col)
    {
        if(col.attachedRigidbody.gameObject.TryGetComponent<CupContainer>(out CupContainer cup))
        {
            if (cup.isSellable())
            {
                ChangeMaterial(0);
                AddObj(col);
                if (col.attachedRigidbody.gameObject.TryGetComponent(out Highlightable obj))
                {
                    obj.SetHighLight(true);
                    obj.SetOverrideHighlight(true);
                }
            }
        }
    }

    public void OnTriggerExitEvent(Collider col)
    {
        RemoveObj(col);
        if (col.attachedRigidbody.gameObject.TryGetComponent(out Highlightable obj))
        {
            obj.SetOverrideHighlight(false);
            obj.SetHighLight(false);
        }
        ChangeMaterialIfEmpty();
    }

    public void ChangeMaterialIfEmpty()
    {
        if (CheckIfEmpty())
        {
            ChangeMaterial(2);
        }
    }

    public void ChangeMaterial(int index)
    {
        if(index < materialList.Length && index >= 0)
            objRenderer.material = materialList[index];
    }
}
