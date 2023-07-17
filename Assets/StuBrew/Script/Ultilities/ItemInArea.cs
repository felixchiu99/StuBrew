using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInArea: MonoBehaviour
{
    [SerializeField] protected List<GameObject> inAreaList = new List<GameObject>();

    public List<GameObject> GetList()
    {
        return inAreaList;
    }

    public void ClearObj()
    {
        foreach (GameObject obj in inAreaList)
        {
            Destroy(obj);
        }
        inAreaList.Clear();
    }
    public void AddObj(Collider collider)
    {
        if (inAreaList.Contains(collider.attachedRigidbody.gameObject))
            return;
        inAreaList.Add(collider.attachedRigidbody.gameObject);
    }
    public void RemoveObj(Collider collider)
    {
        inAreaList.Remove(collider.attachedRigidbody.gameObject);
    }

    protected bool CheckIfEmpty()
    {
        if (inAreaList.Count <= 0)
        {
            return true;
        }
        return false;
    }

    protected GameObject RemoveFirst()
    {
        GameObject removedObj = null;
        if (inAreaList.Count > 0)
        {
            removedObj = inAreaList[0];
            inAreaList.RemoveAt(0);
        }
        return removedObj;
    }
}
