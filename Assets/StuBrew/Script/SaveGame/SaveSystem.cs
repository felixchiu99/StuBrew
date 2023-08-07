using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class SaveSystem
{
    static public SaveableObj prefab;

    static public FadeOnSceneChange sceneChange;

    //static SaveData saveData = new SaveData();
    static GameData saveData = new GameData();

    static public event UnityAction OnFilenameChange;
    
    static public event UnityAction OnFinishLoadedScene;

    [SerializeField]
    static FilenameData filename = new FilenameData();

    public static bool hasLoaded = true;
    public static bool isLoading = false;

    static SaveSystem()
    {
        LoadFileName();
        OnFinishLoadedScene += OnLoaded;
    }

    public static void ClearFileName()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
            File.Delete(filePath);

        filename.ClearAll();
        SaveFileName();
    }
    public static FileData GetFileData(int index)
    {
        return filename.GetFileData(index);
    }
    public static string GetFileName(int index)
    {
        return filename.GetFilename(index);
    }
    public static void SaveFileName()
    {
        string data = JsonUtility.ToJson(filename);
        File.WriteAllText(Application.persistentDataPath + "/saveFilename.json", data);
        //Debug.Log("saving" + data);
        OnFilenameChange?.Invoke();
    }

    public static void LoadFileName()
    {
        string saveFile = Application.persistentDataPath + "/saveFilename.json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            // Work with JSON
            filename = JsonUtility.FromJson<FilenameData>(fileContents);
            OnFilenameChange?.Invoke();
        }
    }

    public static void Save(int saveNum = 3)
    {
        Debug.Log("Saving " + filename.GetFilename(saveNum));

        //retrieve and format data
        saveData.SaveAll();

        //save data
        string data = JsonUtility.ToJson(saveData);
        if (!filename.IsSaved(saveNum))
        {
            filename.SetSave(saveNum);
        }
        File.WriteAllText(Application.persistentDataPath + "/" + filename.GetFilename(saveNum) + ".json", data);

        SaveFileName();
    }

    public static bool CheckIfExist(int sceneIndex)
    {

        return saveData.CheckIfExist(sceneIndex);
    }

    public static void Load(int saveNum = 3, int index = -1, bool loadPlayer = true)
    {
        if (isLoading)
            return;
        isLoading = true;
        string saveFile = Application.persistentDataPath + "/" + filename.GetFilename(saveNum) + ".json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            Debug.Log("Loading " + filename.GetFilename(saveNum));
            string fileContents = File.ReadAllText(saveFile);
            // Work with JSON
            GameData newSave = CreateFromJson(fileContents);
            if (index == -1)
            {
                Debug.Log("index not found");
                newSave.Load();
            }
            else
            {
                Debug.Log("index"+ index);
                newSave.Load(index, loadPlayer);
            }
            saveData.ClearAll();
            saveData = newSave;
        }
    }

    public static GameData CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<GameData>(jsonString);
    }

    public static void CheckFinishLoading()
    {
        if (true)
        {
            Debug.Log("on finished");
            OnFinishLoadedScene?.Invoke();
        }
    }

    public static void OnLoaded()
    {
        Save(3);
        isLoading = false;
    }
}
