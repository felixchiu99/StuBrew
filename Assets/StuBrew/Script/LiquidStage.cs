using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class LiquidSettings
{
    [Header("Liquids")]
    public Color surfaceColour = Color.white;
    public Color liquidColour = Color.white;
    public Color foamColour = Color.white;
}

public class LiquidStage : MonoBehaviour
{
    [Header("Liquid Fill Min Max")]
    [SerializeField]
    float fillMin = -0.5f;
    [SerializeField]
    float fillMax = 0.5f;

    [Space(10)]
    [Header("Operation")]

    [SerializeField]
    Renderer rend;

    [SerializeField]
    int currentLiquid = 0;

    [SerializeField]
    List<LiquidSettings> liqColours;

    void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        rend.material.SetVector("_Fill_Min_Max", new Vector4(fillMin, fillMax, 0, 0) );

        UpdateLiquidStage();
    }

    void UpdateLiquidStage()
    {
        rend.material.SetVector("_LiquidSurface", liqColours[currentLiquid].surfaceColour);
        rend.material.SetVector("_LiquidColour", liqColours[currentLiquid].liquidColour);
        rend.material.SetVector("_Foam_EdgeColour", liqColours[currentLiquid].foamColour);
    }
}
