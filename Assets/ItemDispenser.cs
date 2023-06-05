using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DispensableObject
{
    [Header("Object")]
    public string name;
    public GameObject prefab;

}

public class ItemDispenser : MonoBehaviour
{
    [SerializeField]
    private List<DispensableObject> objList;

    [SerializeField] private Transform spawnPoint;
    private int selector = 0;

    public void NextItem()
    {
        selector++;
        if (selector >= objList.Count)
        {
            selector = 0;
        }
    }
    public void PrevItem()
    {
        selector--;
        if (selector < 0)
        {
            selector = objList.Count;
        }
    }

    public void SpawnItem()
    {
        if (!(objList.Count == 0))
            return;
        GameObject obj = Instantiate(objList[selector].prefab, spawnPoint.position, Quaternion.identity);
    }
}
