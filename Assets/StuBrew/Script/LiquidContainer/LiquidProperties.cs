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

    private bool isDefault = true;

    //transfer
    public bool canTransfer = true;

    void Awake()
    {
        canTransfer = true;
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
        isDefault = false;
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
        isDefault = false;
        bitterness += change;
    }
    public float GetBitterness()
    {
        return bitterness;
    }
    public void SetBitterness(float change)
    {
        isDefault = false;
        bitterness = change;
    }

    public void ChangeSweetness(float change)
    {
        isDefault = false;
        sweetness += change;
    }
    public float GetSweetness()
    {
        return sweetness;
    }
    public void SetSweetness(float change)
    {
        isDefault = false;
        sweetness = change;
    }

    public void ChangeAroma(float change)
    {
        isDefault = false;
        aroma += change;
    }
    public float GetAroma()
    {
        return aroma;
    }

    public void SetAroma(float aroma)
    {
        this.aroma = aroma;
    }

    public Color GetColor()
    {
        return color;
    }
    public void SetColor(Color color)
    {
        this.color = color;
    }

    public Color GetFoamColor()
    {
        return foamColor;
    }
    public void SetFoamColor(Color color)
    {
        this.foamColor = color;
    }

    public float GetTransparency()
    {
        return transparency;
    }
    public void SetTransparency(float transparency)
    {
        this.transparency = transparency;
    }

    public void ChangeTransparency(float change)
    {
        isDefault = false;
        transparency += change;
    }

    public void ChangeTemperature(float change)
    {
        isDefault = false;
        temperature += change;
    }
    public float GetTemperature()
    {
        return temperature;
    }
    public void SetTemperature(float change)
    {
        isDefault = false;
        temperature = change;
    }

    public void ClearLiq()
    {
        isDefault = true;
        temperature = 18f;
        flavourBalance = 0;

        bitterness = 0;
        sweetness = 0;
        aroma = 0;
        color = Color.white;
        foamColor = Color.white;
        transparency = 0;
    }

    public bool IsDefault()
    {
        return isDefault;
    }

    public static bool operator == (LiquidProperties a, LiquidProperties b)
    {
        if (b is null && a is null)
        {
            return true;
        }
        else if (b is null)
        {
            return false;
        }
        bool similar = true;
        similar = a.GetTemperature() != b.GetTemperature()? false : similar;
        similar = a.GetBitterness() != b.GetBitterness()? false : similar;
        similar = a.GetSweetness() != b.GetSweetness()? false : similar;
        similar = a.GetAroma() != b.GetAroma()? false : similar;
        
        return similar;
    }
    public static bool operator !=(LiquidProperties a, LiquidProperties b)
    {
        bool similar = a == b;
        return !similar;
    }
}
