using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeatExchangeProcess : StuBrew.BrewingProcess
{
    float liquid = 0f;
    float flowRate = 0.01f;

    [SerializeField]
    LiquidContainerSetting toContainer;

    public UnityEvent<float> fluidOut;

    void Update()
    {
        flowOut();
    }

    public void flowIn(float liquidIn)
    {
        liquid += liquidIn;
    }

    public void flowOut()
    {
        if (liquid > 0)
        {
            if (toContainer)
            {
                float transfered = toContainer.AddLiquid(flowRate * Time.deltaTime);
                fluidOut?.Invoke(transfered);
                liquid = Mathf.Clamp(liquid - flowRate * Time.deltaTime, 0, 100);
                //fluidOut?.Invoke(flowRate);
            }
        }
    }
}
