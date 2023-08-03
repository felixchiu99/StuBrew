using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

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
        if(fileNum != 3)
        {
            SaveSystem.Save(3);
        }
        SaveSystem.Save(fileNum);
    }
    [Button]
    public void Load(int fileNum = 3)
    {
        SaveSystem.Load(fileNum);

    }

    [Button]
    public void LoadBrew()
    {
        LoadScene(1);
    }

    [Button]
    public void LoadStall()
    {
        LoadScene(2);

    }

    public void LoadScene(int sceneIndex)
    {
        if (SaveSystem.CheckIfExist(sceneIndex))
            SaveSystem.Load(3, sceneIndex, false);
        else
        {
            if (sceneIndex < SceneManager.sceneCountInBuildSettings && sceneIndex >= 0) 
            {
                string path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
                int slash = path.LastIndexOf('/');
                string name = path.Substring(slash + 1);
                int dot = name.LastIndexOf('.');
                sceneChange.sceneName = name.Substring(0, dot);
                sceneChange.DoFade();
            }
        }
    }

    [Button]
    public void Clear()
    {

        SaveSystem.ClearFileName();

    }
}
