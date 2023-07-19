using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidChangeColor : MonoBehaviour
{
    [SerializeField]
    Renderer rend;
    [SerializeField]
    LiquidContainerSetting container;

    void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        rend.material.SetFloat("_Fill", 0f);
    }

    public void ChangeColor(Color color)
    {
        rend.material.SetColor("_LiquidColour", color);
        container.SetVisualFill(0.8f);
    }

    public void HideColor()
    {
        container.SetVisualFill(0);
    }
}
