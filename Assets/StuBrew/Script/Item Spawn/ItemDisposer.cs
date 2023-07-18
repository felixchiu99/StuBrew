using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisposer : MonoBehaviour
{
    [SerializeField] bool deleteOnClear = true;
    [SerializeField] List<GameObject> delList = new List<GameObject>();
    public void ClearObj()
    {
        foreach(GameObject obj in delList)
        {
            if (deleteOnClear)
                Destroy(obj);
            else
                obj.SetActive(false);
        }
        delList.Clear();
    }
    public void AddObj(Collider collider)
    {
        if (delList.Contains(collider.attachedRigidbody.gameObject))
            return;
        SetHighlightable(collider.attachedRigidbody.gameObject, true);
        delList.Add(collider.attachedRigidbody.gameObject);

    }
    public void RemoveObj(Collider collider)
    {
        if (!delList.Contains(collider.attachedRigidbody.gameObject))
            return;
        SetHighlightable(collider.attachedRigidbody.gameObject, false);
        delList.Remove(collider.attachedRigidbody.gameObject);
    }

    void SetHighlightable(GameObject obj, bool isEnable = false)
    {
        if (obj.TryGetComponent(out Highlightable highlightObj))
        {
            highlightObj.SetOverrideHighlight(isEnable);
            highlightObj.SetHighLight(isEnable);
        }
    }
}
