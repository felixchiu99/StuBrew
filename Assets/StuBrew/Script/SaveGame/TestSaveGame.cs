using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class TestSaveGame : MonoBehaviour
{
    public bool autoSave = false;
    public SaveableObj prefab;
    public FadeOnSceneChange sceneChange;

    void Start()
    {
        SaveSystem.prefab = prefab;
        SaveSystem.sceneChange = sceneChange;
    }

    [Button]
    public void Save(int fileNum = 3)
    {
        SaveSystem.Save(fileNum);
    }
    [Button]
    public void Load(int fileNum = 3)
    {
        SaveSystem.Load(fileNum);

    }

    [Button]
    public void Clear()
    {
        SaveSystem.ClearFileName();

    }
}
