using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField] SceneData sceneData;

    [SerializeField]
    List<BarrelData> barrels = new List<BarrelData>();
    List<GameObject> listOfBarrel = new List<GameObject>();

    [SerializeField]
    List<PlayerData> players = new List<PlayerData>();
    List<GameObject> listOfPlayer = new List<GameObject>();

    public void SaveAll()
    {
        SaveScene();
        SavePlayer();
        SaveBarrels();
    }

    public void SaveScene()
    {
        sceneData = new SceneData();
    }

    public void SavePlayer()
    {
        GameObject[] playerObj;
        playerObj = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerObj)
        {
            if (listOfPlayer.Contains(player))
                continue;
            Debug.Log(player.name);
            if(player.name == "[PlayerController]")
            {
                continue;
            }
            players.Add(new PlayerData(player));
            listOfPlayer.Add(player);
        }
    }

    protected void AddBarrel(GameObject obj)
    {
        if (listOfBarrel.Contains(obj))
            return;
        Transform barrelTransform = obj.transform;
        if (obj.TryGetComponent(out LiquidProperties prop))
        {
            if (obj.TryGetComponent(out BarrelContainer container))
            {
                listOfBarrel.Add(obj);
                barrels.Add(new BarrelData(barrelTransform, prop, container));
            }
        }
    }

    public void SaveBarrels()
    {
        var tags = Object.FindObjectsOfType<CustomTag>();
        foreach (CustomTag tag in tags)
        {
            if (tag.HasTag("Barrel"))
            {
                AddBarrel(tag.gameObject);
            }
            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
        }
    }

    public void ClearAll()
    {
        players.Clear();
        listOfPlayer.Clear();

        barrels.Clear();
        foreach (GameObject barrel in listOfBarrel)
        {
            Object.Destroy(barrel);
        }
        listOfBarrel.Clear();
    }

    public void Load()
    {
        sceneData.Load();
        LoadPlayer();
        LoadBarrel();
    }

    protected void LoadPlayer()
    {
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerObj)
        {
            foreach(PlayerData pData in players)
            {
                pData.Load(player);
            }
        }
    }

    protected void LoadBarrel()
    {
        foreach (BarrelData barrel in barrels)
        {
            listOfBarrel.Add(barrel.Load(SaveSystem.prefab._barrelPrefab));
        }
    }
}
