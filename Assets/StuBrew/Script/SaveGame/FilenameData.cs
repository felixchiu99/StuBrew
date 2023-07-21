using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FileData
{
    public FileData()
    {
        Clear();
    }

    public void Clear()
    {
        displayName = "---";
        isSave = false;
        saveDate = "---";
        saveScene = "---";
    }
    public string displayName = "---";
    public bool isSave = false;
    public string saveDate = "---";
    public string saveScene = "---";
}


[System.Serializable]
public class FilenameData
{
    [SerializeField]
    string[] filename = new string[] { "save1", "save2", "save3", "autosave" };
    [SerializeField]
    FileData[] saveData = new FileData[4];

    public FilenameData()
    {
        for(int i= 0; i<4; i++)
        {
            saveData[i] = new FileData();
        }
    }

    public FileData GetFileData(int index)
    {
        if (saveData[index].isSave == true)
        {
            return saveData[index];
        }
        return new FileData();
    }

    public string GetFilename(int index)
    {
        if (saveData[index].isSave == true)
        {
            return saveData[index].displayName;
        }
        return "---";
    }
    public string GetFileScene(int index)
    {
        if (saveData[index].isSave == true)
        {
            return saveData[index].saveScene;
        }
        return "---";
    }
    public string GetFileTime(int index)
    {
        if (saveData[index].isSave == true)
        {
            return saveData[index].saveDate;
        }
        return "---";
    }

    public bool IsSaved(int index)
    {
        return saveData[index].isSave;
    }

    public void SetSave(int index)
    {
        saveData[index].isSave = true;
        System.DateTime dt = System.DateTime.Now;
        saveData[index].saveDate = dt.ToString("yyyy-MM-dd\\THH:mm:ss\\Z");
        Scene scene = SceneManager.GetActiveScene();
        saveData[index].saveScene = scene.name;
        saveData[index].displayName = filename[index];
    }

    public void ClearAll()
    {
        for (int i = 0; i < 4; i++)
        {
            saveData[i].isSave = false;
        }
    }

}
