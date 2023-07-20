using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class SaveSystem
{
    static public SaveableObj prefab;

    static SaveData saveData = new SaveData();

    static public event UnityAction OnFilenameChange;

    [SerializeField]
    static FilenameData filename = new FilenameData();

    static SaveSystem()
    {
        LoadFileName();
    }

    public static void ClearFileName()
    {
        filename.ClearAll();
        SaveFileName();
    }
    public static string GetFileName(int index)
    {
        return filename.GetFilename(index);
    }
    public static void SaveFileName()
    {
        string data = JsonUtility.ToJson(filename);
        File.WriteAllText(Application.persistentDataPath + "/saveFilename.json", data);
        OnFilenameChange?.Invoke();
    }
    public static void LoadFileName()
    {
        Debug.Log("loadname");
        string saveFile = Application.persistentDataPath + "/saveFilename.json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            Debug.Log(fileContents);
            // Work with JSON
            filename = JsonUtility.FromJson<FilenameData>(fileContents);

        }
    }

    public static void Save(int saveNum = 3)
    {
        //retrieve and format data
        saveData.SaveAll();

        //save data
        string data = JsonUtility.ToJson(saveData);
        Debug.Log(data);
        if (!filename.IsSaved(saveNum))
        {
            filename.SetSave(saveNum);
        }
        Debug.Log(filename.IsSaved(saveNum));
        File.WriteAllText(Application.persistentDataPath + "/" + filename.GetFilename(saveNum) + ".json", data);

        SaveFileName();
    }

    public static void Load(int saveNum = 3)
    {
        string saveFile = Application.persistentDataPath + "/" + filename.GetFilename(saveNum) + ".json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            Debug.Log(fileContents);
            // Work with JSON
            SaveData newSave = CreateFromJson(fileContents);
            newSave.Load();
            saveData.ClearAll();
            saveData = newSave;
        }
    }

    public static SaveData CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<SaveData>(jsonString);
    }


}
