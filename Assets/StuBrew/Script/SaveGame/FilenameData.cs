using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FilenameData
{
    [SerializeField]
    string[] filename = new string[] { "save1", "save2", "save3", "autosave" };
    [SerializeField]
    bool[] isSave = new bool[] {false , false, false, false};

    public string GetFilename(int index)
    {
        if (isSave[index] == true)
        {
            return filename[index];
        }
        return "---";
    }
    public bool IsSaved(int index)
    {
        return isSave[index];
    }

    public void SetSave(int index)
    {
        isSave[index] = true;
    }

    public void ClearAll()
    {
        isSave = new bool[] { false , false, false, false};
    }

}
