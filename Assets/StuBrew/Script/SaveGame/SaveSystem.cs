using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    static public SaveableObj prefab;

    static SaveData saveData = new SaveData();

    public static void AddPlayer()
    {
        
    } 
    
    public static void Save()
    {
        //retrieve and format data
        saveData.SaveAll();

        //save data
        string data = JsonUtility.ToJson(saveData);
        Debug.Log(data);
        File.WriteAllText(Application.persistentDataPath + "/Save1.json", data);
    }

    public static void Load()
    {
        string saveFile = Application.persistentDataPath + "/Save1.json";
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
