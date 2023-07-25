using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidChangeColor : MonoBehaviour
{
    [SerializeField]
    Renderer rend;
    [SerializeField]
    LiquidContainerSetting container;

    [SerializeField]
    LiquidProperties liqProp;

    public bool isDisplayOnly = false;

    void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        if (liqProp == null)
        {
            liqProp = GetComponent<LiquidProperties>();
            ChangeColor(liqProp.GetColor());
            ChangeFoamColor(liqProp.GetFoamColor());
            ChangeTransparency(liqProp.GetTransparency());
        }
        rend.material.SetFloat("_Fill", 0f);
        if(container && isDisplayOnly)
            container.SetVisualFill(0);
    }

    public void ChangeColor(Color color)
    {
        rend.material.SetColor("_LiquidColour", color);
    }

    public void ChangeFoamColor(Color color)
    {
        rend.material.SetColor("_Foam_EdgeColour", Color.Lerp(color, Color.black, 0.1f));
        rend.material.SetColor("_LiquidSurface", color);

    }

    public void ChangeTransparency(float transparent)
    {
        rend.material.SetFloat("_Transparency", transparent);
    }

    public void ChangeLiquidProp(LiquidProperties liquidProperties)
    {
        liqProp.Copy(liquidProperties);
        UpdateColor();
        if (isDisplayOnly)
            container.SetVisualFill(0.8f);
    }

    public void UpdateColor()
    {
        ChangeColor(liqProp.GetColor());
        ChangeFoamColor(liqProp.GetFoamColor());
        ChangeTransparency(liqProp.GetTransparency());
    }

    public void HideColor()
    {
        container.SetVisualFill(0);
    }
}
