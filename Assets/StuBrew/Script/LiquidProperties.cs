using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidProperties : MonoBehaviour
{
    //
    float temperature = 0;

    // liquid properties
    float flavourBalance = 0;   //0-1
    float bitterness = 0;       //0-1
    float sweetness = 0;        //0-1

    float aroma = 0;         //0-1

    // Color, clarity and foam
    public Color color;
    float transparency = 0;     //0-1
    public Color foamColor;
    //

    void Update()
    {
        flavourBalance = bitterness / sweetness;
    }

    public void ChangeBitterness(float change)
    {
        bitterness += change;
    }
    
    public void ChangeSweetness(float change)
    {
        sweetness += change;
    }

    public void ChangeAroma(float change)
    {
        aroma += change;
    }

    public void ChangeTransparency(float change)
    {
        transparency += change;
    }
}
