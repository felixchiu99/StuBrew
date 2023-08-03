using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetTransformOnSceneChange : MonoBehaviour
{
    Vector3 spawnPos = new Vector3(0, 0, 0);
    float randDist = 2f;

    void Start()
    {
        Debug.Log("test");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetSpawnPos(Vector3 pos)
    {
        spawnPos = pos;
    }

    public void SetRandDist(float dist)
    {
        randDist = dist;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject obj = new GameObject();
        transform.position = new Vector3(0, 0, 0);
        SetChildenPosition(obj);
        transform.SetParent(obj.transform);
        //Destroy(gameObject);
    }

    void SetChildenPosition(GameObject obj)
    {
        foreach (Transform child in transform)
        {
            SetChildPosition(child, new Vector3(Random.Range(-randDist, randDist) + spawnPos.x, spawnPos.y, Random.Range(-randDist, randDist) + spawnPos.z));
            SetHighlightable(child.gameObject, false);
        }
        for(int i = transform.childCount-1; i >= 0; i --)
        {
            transform.GetChild(i).transform.SetParent(obj.transform);
        }
    }

    void SetChildPosition(Transform child, Vector3 pos)
    {
        child.position = pos;
    }

    void SetHighlightable(GameObject obj, bool isEnable = false)
    {
        if (obj.TryGetComponent(out Highlightable highlightObj))
        {
            highlightObj.SetOverrideHighlight(isEnable);
            highlightObj.SetHighLight(isEnable);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
