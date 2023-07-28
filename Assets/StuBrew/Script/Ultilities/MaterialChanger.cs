using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] Material[] materials;

    [SerializeField] MeshRenderer meshRenderer;

    void Start()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial(int index)
    {
        if(materials.Length > index)
        {
            meshRenderer.material = materials[index];
        }
    }
}
