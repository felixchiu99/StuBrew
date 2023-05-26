using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidProperties : MonoBehaviour
{
    //
    [SerializeField] float temperature = 0;

    // liquid properties
    [SerializeField] float flavourBalance = 0;   //0-1
    [SerializeField] float bitterness = 0;       //0-1
    [SerializeField] float sweetness = 0;        //0-1

    [SerializeField] float aroma = 0;         //0-1

    // Color, clarity and foam
    public Color color;
    float transparency = 0;     //0-1
    public Color foamColor;
    //

    void Update()
    {
        flavourBalance = bitterness / sweetness;
    }

    public void Copy(LiquidProperties prop)
    {
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

}
