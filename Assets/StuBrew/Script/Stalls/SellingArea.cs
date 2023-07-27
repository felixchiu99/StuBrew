using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SellingArea : ItemInArea
{
    [SerializeField] Material[] materialList;
    [SerializeField] Renderer objRenderer;

    [SerializeField] UnityEvent<GameObject> OnSell;

    [Tooltip("SFX index : 0 - sell, 1 - buyFail")]
    [SerializeField]
    protected UnityEvent<int> playSFX;

    [SerializeField]
    private int sellingPrice = 2;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    public void SellItem()
    {
        if (!CheckIfEmpty())
        {
            playSFX?.Invoke(0);
            CurrencyManager.Instance.Add(sellingPrice);
            GameObject delObj = RemoveFirst();
            OnSell?.Invoke(delObj);
            Destroy(delObj);
            ChangeMaterialIfEmpty();
        }
        else
        {
            playSFX?.Invoke(1);
        }
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
