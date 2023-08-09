using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumAni : MonoBehaviour
{
    [SerializeField]
    GameObject floatingTextPrefab;
    [SerializeField]
    Transform floatingTextSpawn;

    public void SpawnFloatingText(string text)
    {
        GameObject floatingNum = Instantiate(floatingTextPrefab, floatingTextSpawn.position, floatingTextSpawn.rotation);
        floatingNum.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
    }
}
