using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnorePlayer : ChildIgnoreCollider
{
    public string tag = "Player";
    void Start()
    {
        GetPlayerCol();
        MakeChildrenIgnore();
    }
    void GetPlayerCol()
    {
        GameObject[] foundPlayerObjects = GameObject.FindGameObjectsWithTag("Player");
        List<Collider> cols = new List<Collider>();
        foreach (GameObject obj in foundPlayerObjects)
        {
            foreach (Collider col in obj.GetComponents<Collider>())
            {
                cols.Add(col);
            }
        }
        toBeIgnored = cols.ToArray();
    }
}
