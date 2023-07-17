using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    [SerializeField] public Material[] materialList;
    [SerializeField] Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    public void ChangeMaterial(int index)
    {
        if (index < materialList.Length && index >= 0)
            objRenderer.material = materialList[index];
    }
}
