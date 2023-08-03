using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public SaveData[] saveData = new SaveData[SceneManager.sceneCountInBuildSettings];

    public int inScene = 0;

    public bool CheckIfExist(int sceneIndex)
    {
        if (saveData[sceneIndex] == null)
            return false;
        return saveData[sceneIndex].isSaved;
    }

    public void SaveAll()
    {
        Scene scene = SceneManager.GetActiveScene();
        inScene = scene.buildIndex;
        if(saveData[inScene] == null)
        {
            saveData[inScene] = new SaveData();
        }
        saveData[inScene].ClearAll();
        saveData[inScene].SaveAll();
    }

    public void Load(bool loadPlayer = true)
    {
        Scene scene = SceneManager.GetActiveScene();
        saveData[scene.buildIndex].Load(loadPlayer);
        //Debug.Log("auto Loading scene" + scene.buildIndex);
        inScene = scene.buildIndex;
    }

    public void Load(int index, bool loadPlayer = true)
    {
        if(index < SceneManager.sceneCountInBuildSettings && index >= 0)
        {
            saveData[index].Load(loadPlayer);
            inScene = index;
        }
            
    }

    public void ClearAll()
    {
        foreach (SaveData save in saveData)
        {
            if(save != null)
                save.ClearAll();
        }
    }
}
