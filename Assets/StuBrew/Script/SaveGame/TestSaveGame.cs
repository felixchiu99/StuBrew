using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class TestSaveGame : MonoBehaviour
{
    public SaveableObj prefab;

    void Start()
    {
        SaveSystem.prefab = prefab;
    }

    [Button]
    void Save()
    {
        SaveSystem.Save();
    }
    [Button]
    void Load()
    {
        SaveSystem.Load();

    }
}
