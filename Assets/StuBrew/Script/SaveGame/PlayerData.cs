using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public float currency;

    public string name;

    public PlayerData(GameObject obj)
    {
        if (!obj.activeSelf)
            return;
        position = new float[3];
        position[0] = obj.transform.position.x;
        position[1] = obj.transform.position.y;
        position[2] = obj.transform.position.z;

        name = obj.name;

        currency = CurrencyManager.Instance.GetCurrentStored();
    }

    public GameObject Load(GameObject playerObj)
    {
        if (playerObj.name != name)
            return null;

        if (playerObj.TryGetComponent<PcPlayerController>(out PcPlayerController pcPlayer))
        {
            pcPlayer.Teleported();
        }

        playerObj.transform.position = new Vector3(position[0],position[1],position[2]);
        CurrencyManager.Instance.SetCurrentStored((int)currency);

        return playerObj;
    }
}
