using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidProperties : MonoBehaviour
{
    //
    [SerializeField] float temperature = 18f;

    // liquid properties
    [SerializeField] float flavourBalance = 0;   //0-1
    [SerializeField] float bitterness = 0;       //0-1
    [SerializeField] float sweetness = 0;        //0-1

    [SerializeField] float aroma = 0;         //0-1

    // Color, clarity and foam
    public Color color = Color.white;
    float transparency = 0;     //0-1
    public Color foamColor = Color.white;
    //

    //transfer
    public bool canTransfer = false;

    void Start()
    {
        canTransfer = false;
        ClearLiq();
    }

    void Update()
    {
        flavourBalance = (sweetness + bitterness) == 0 ?0 :bitterness / (sweetness + bitterness);
    }

    public void Copy(LiquidProperties prop)
    {
        //if (!canTransfer)
        //    return;
        temperature = prop.temperature;
        flavourBalance = prop.flavourBalance;
        
        bitterness = prop.bitterness;
        sweetness = prop.sweetness;
        aroma = prop.aroma;
        color = prop.color;
        foamColor = prop.foamColor;
        transparency = prop.transparency;

    }

    public void ChangeBitterness(float change)
    {
        bitterness += change;
    }
    public float GetBitterness()
    {
        return bitterness;
    }

    public void ChangeSweetness(float change)
    {
        sweetness += change;
    }
    public float GetSweetness()
    {
        return sweetness;
    }
    public void SetSweetness(float change)
    {
        sweetness = change;
    }

    public void ChangeAroma(float change)
    {
        aroma += change;
    }
    public float GetAroma()
    {
        return aroma;
    }

    public void ChangeTransparency(float change)
    {
        transparency += change;
    }

    public void ChangeTemperature(float change)
    {
        temperature += change;
    }
    public float GetTemperature()
    {
        return temperature;
    }
    public void SetTemperature(float change)
    {
        temperature = change;
    }

    public void ClearLiq()
    {
        temperature = 18f;
        flavourBalance = 0;

        bitterness = 0;
        sweetness = 0;
        aroma = 0;
        color = Color.white;
        foamColor = Color.white;
        transparency = 0;
    }
}
