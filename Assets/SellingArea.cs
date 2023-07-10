using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SellingArea : MonoBehaviour
{
    [SerializeField] Material[] materialList;
    [SerializeField] Renderer objRenderer;

    [SerializeField] List<GameObject> delList = new List<GameObject>();

    [SerializeField] UnityEvent<GameObject> OnSell;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    public void SellItem()
    {
        if (delList.Count > 0)
        {
            CurrencyManager.Instance.Add(10);
            OnSell?.Invoke(delList[0]);
            GameObject delObj = delList[0];
            delList.RemoveAt(0);
            Destroy(delObj);
            CheckIfEmpty();
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
        CheckIfEmpty();
    }

    private void CheckIfEmpty()
    {
        if (delList.Count <= 0)
        {
            ChangeMaterial(2);
        }
    }

    public void ChangeMaterial(int index)
    {
        if(index < materialList.Length && index >= 0)
            objRenderer.material = materialList[index];
    }

    public void ClearObj()
    {
        foreach (GameObject obj in delList)
        {
            Destroy(obj);
        }
        delList.Clear();
    }
    public void AddObj(Collider collider)
    {
        if (delList.Contains(collider.attachedRigidbody.gameObject))
            return;
        delList.Add(collider.attachedRigidbody.gameObject);
    }
    public void RemoveObj(Collider collider)
    {
        delList.Remove(collider.attachedRigidbody.gameObject);
    }
}
