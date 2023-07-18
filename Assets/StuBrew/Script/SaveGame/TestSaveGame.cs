using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class TestSaveGame : MonoBehaviour
{

    [Button]
    void SaveBarrel()
    {
        var tags = FindObjectsOfType<CustomTag>();


        foreach (CustomTag tag in tags)
        {
            if (tag.HasTag("Barrel"))
            {
                SaveSystem.AddBarrel(tag.gameObject);
            }
            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
        }
        SaveSystem.SaveBarrel();
    }
    [Button]
    void LoadBarrel()
    {
        SaveSystem.LoadBarrel();

    }
}
