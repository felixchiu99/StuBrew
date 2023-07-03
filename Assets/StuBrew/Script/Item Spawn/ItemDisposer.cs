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
        delList.Add(collider.attachedRigidbody.gameObject);
    }
    public void RemoveObj(Collider collider)
    {
        delList.Remove(collider.attachedRigidbody.gameObject);
    }
}
