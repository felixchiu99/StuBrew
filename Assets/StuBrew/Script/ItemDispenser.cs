using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] TextMeshProUGUI displayText;

    private bool canSpawn = true;

    void Start()
    {
        DisplayText();
    }

    private void DisplayText()
    {
        if(displayText)
            displayText.SetText(objList[selector].name);
    }

    public void NextItem()
    {
        selector++;
        if (selector >= objList.Count)
        {
            selector = 0;
        }
        Debug.Log(objList[selector].name);
        DisplayText();
    }
    public void PrevItem()
    {
        selector--;
        if (selector < 0)
        {
            selector = objList.Count;
        }
        DisplayText();
    }

    public void SpawnItem()
    {
        if (!canSpawn)
            return;
            
        if (objList.Count == 0)
            return;
        Debug.Log(objList[selector].name);
        GameObject obj = Instantiate(objList[selector].prefab, spawnPoint.position, Quaternion.identity);
    }

    public void OnTriggerStay(Collider other)
    {
        canSpawn = false;
    }

    public void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }
}
