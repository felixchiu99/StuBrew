using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class BarrelContainer : FluidContainer
{
    [SerializeField] TextMeshProUGUI displayText;

    void Start()
    {
        UpdateFluidFill += DisplayText;
        InvokeUpdateFluidFill();
    }

    void OnDisable()
    {
        UpdateFluidFill -= DisplayText;
    }

    private void DisplayText()
    {
        if (displayText)
            displayText.SetText(currentStored.ToString("F2"));
    }
}
