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
    public Color fenselColour = new Color(0.1f, 1f, 0f, 1f);
    [Range(0, 1)] public float transparency = 0.9f;
    [Range(0, 1)] public float foamAmount = 0f;
    [Range(0, 1)] public float foamSmoothness = 0f;
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

    public void UpdateLiquidStage()
    {
        rend.material.SetVector("_LiquidSurface", liqColours[currentLiquid].surfaceColour);
        rend.material.SetVector("_LiquidColour", liqColours[currentLiquid].liquidColour);
        rend.material.SetVector("_Foam_EdgeColour", liqColours[currentLiquid].foamColour);
        rend.material.SetVector("_Fensel", liqColours[currentLiquid].fenselColour);
        rend.material.SetFloat("_Transparency", liqColours[currentLiquid].transparency);
        rend.material.SetFloat("_FoamPercentage", liqColours[currentLiquid].foamAmount);
        rend.material.SetFloat("_FoamSmoothness", liqColours[currentLiquid].foamSmoothness);
    }
    public void BlendLiquidStage(int liquidStage, float blend = 0)
    {
        Color surfaceColour = liqColours[currentLiquid].surfaceColour;
        Color liquidColour = liqColours[currentLiquid].liquidColour;
        Color foamColour = liqColours[currentLiquid].foamColour;
        Color fenselColour = liqColours[currentLiquid].fenselColour;
        float transparency = liqColours[currentLiquid].transparency;
        float foamAmount = liqColours[currentLiquid].foamAmount;
        float foamSmoothness = liqColours[currentLiquid].foamSmoothness;
        if (liquidStage < liqColours.Count)
        {
            surfaceColour = Color.Lerp(surfaceColour, liqColours[liquidStage].surfaceColour, blend);
            liquidColour = Color.Lerp(liquidColour, liqColours[liquidStage].liquidColour, blend);
            foamColour = Color.Lerp(foamColour, liqColours[liquidStage].foamColour, blend);
            fenselColour = Color.Lerp(fenselColour, liqColours[liquidStage].fenselColour, blend);
            transparency = Mathf.Lerp(transparency, liqColours[liquidStage].transparency, blend);
            foamAmount = Mathf.Lerp(foamAmount, liqColours[liquidStage].foamAmount, blend);
            foamSmoothness = Mathf.Lerp(foamSmoothness, liqColours[liquidStage].foamSmoothness, blend);
        }
        rend.material.SetVector("_LiquidSurface", surfaceColour);
        rend.material.SetVector("_LiquidColour", liquidColour);
        rend.material.SetVector("_Foam_EdgeColour", foamColour);
        rend.material.SetVector("_Fensel", fenselColour);
        rend.material.SetFloat("_Transparency", transparency);
        rend.material.SetFloat("_FoamPercentage", foamAmount);
        rend.material.SetFloat("_FoamSmoothness", foamSmoothness);
    }

    public void ChangeLiquidStage(int stage)
    {
        if (stage < liqColours.Count)
            currentLiquid = stage;  
    }
}
