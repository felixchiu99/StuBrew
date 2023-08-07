using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    bool loadPlayer = true;

    [SerializeField]
    public bool isSaved = false;

    public void SaveAll()
    {
        isSaved = true;
        SaveScene();
        SavePlayer();
        SaveBarrels();
    }

    public void SaveScene()
    {
        if(sceneData == null)
            sceneData = new SceneData();
        sceneData.Save();
    }

    public void SavePlayer()
    {
        GameObject[] playerObj;
        playerObj = GameObject.FindGameObjectsWithTag("PlayerController");
        foreach (GameObject player in playerObj)
        {
            if (listOfPlayer.Contains(player))
                continue;
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
                Debug.Log("hi");
                if(tag.gameObject.scene.name != "DontDestroyOnLoad")
                    AddBarrel(tag.gameObject);
            }
            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
        }
        Debug.Log("barrel saved " + barrels.Count);
    }

    public void ClearAll(bool isDestroy = true)
    {
        players.Clear();
        listOfPlayer.Clear();

        barrels.Clear();
        if(isDestroy)
            foreach (GameObject barrel in listOfBarrel)
            {
                Object.Destroy(barrel);
            }
        listOfBarrel.Clear();
    }

    public void Load(bool loadPlayer = true)
    {
        this.loadPlayer = loadPlayer;
        SaveSystem.hasLoaded = false;
        sceneData.Load();
        SceneManager.sceneLoaded += LoadSceneLoaded;
    }

    private void LoadSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadSceneLoaded;
        Debug.Log("Loading On Sceneload");
        if (loadPlayer)
        {
            LoadPlayer();
            Debug.Log("Loading Player");
        }

        LoadBarrel();
        SaveSystem.hasLoaded = true;
        loadPlayer = true;
        SaveSystem.CheckFinishLoading();
    }

    protected void LoadPlayer()
    {
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("PlayerController");

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
        Debug.Log("barrel loaded " + barrels.Count);
    }
}
