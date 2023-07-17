using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoNotDestroyInArea : ItemInArea
{

    [SerializeField] UnityEvent<GameObject> OnSceneChange;
    GameObject doNotDestroy;
    public void Start()
    {
        doNotDestroy = new GameObject();
        DontDestroyOnLoad(doNotDestroy);
    }

    public void OnSceneChangeEvent()
    {
        foreach (GameObject obj in GetList())
        {
            OnSceneChange?.Invoke(obj);
            obj.transform.SetParent(doNotDestroy.transform);
            //DontDestroyOnLoad(obj);
        }
    }

    public void OnTriggerEnterEvent(Collider col)
    {
        AddObj(col);
        if (col.attachedRigidbody.gameObject.TryGetComponent(out Highlightable obj))
        {
            obj.SetHighLight(true);
            obj.SetOverrideHighlight(true);
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
        
    }
}
