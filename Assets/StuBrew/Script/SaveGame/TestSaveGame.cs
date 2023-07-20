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
    public void Save(int fileNum)
    {
        SaveSystem.Save(fileNum);
    }
    [Button]
    public void Load(int fileNum)
    {
        SaveSystem.Load(fileNum);

    }

    [Button]
    public void Clear()
    {
        SaveSystem.ClearFileName();

    }
}
