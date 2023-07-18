using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoNotDestroyInArea : ItemInArea
{

    [SerializeField] UnityEvent<GameObject> OnSceneChange;
    GameObject doNotDestroy;

    [SerializeField] Vector3 spawnPos = new Vector3(0, 0, 0);
    [SerializeField] float randDist = 2f;

    public void Start()
    {
        doNotDestroy = new GameObject();
        SetTransformOnSceneChange temp = doNotDestroy.AddComponent(typeof(SetTransformOnSceneChange)) as SetTransformOnSceneChange;
        temp.SetSpawnPos(spawnPos);
        temp.SetRandDist(randDist);


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
