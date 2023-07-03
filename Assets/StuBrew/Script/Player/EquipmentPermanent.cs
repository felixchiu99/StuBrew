using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPermanent : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    void Start()
    {
        ReturnToSpawn();
    }
    public void ReturnToSpawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        transform.parent = spawnPoint;
    }
}
