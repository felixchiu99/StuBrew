using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    static List<GameObject> listOfBarrel = new List<GameObject>();

    public static void AddBarrel(GameObject obj)
    {
        if (listOfBarrel.Contains(obj))
            return;
        listOfBarrel.Add(obj);
    }

    static string SaveBarrelData(string str, GameObject obj)
    {
        Transform barrelTransform = obj.transform;
        if (obj.TryGetComponent(out LiquidProperties prop))
        {
            if (obj.TryGetComponent(out BarrelContainer container))
            {
                BarrelData data = new BarrelData(barrelTransform, prop, container);
                str += JsonUtility.ToJson(data);
                return str;
            }
        }
        return "";
    }

    public static void SaveBarrel()
    {
        string barrel = "";

        foreach (GameObject obj in listOfBarrel)
        {
            barrel = SaveBarrelData(barrel, obj);
        }

        File.WriteAllText(Application.persistentDataPath + "/Barrel.json", barrel);
    }

    public static void LoadBarrel()
    {
        string saveFile = Application.persistentDataPath + "/Barrel.json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            Debug.Log(fileContents);
            // Work with JSON
        }
    }
}
