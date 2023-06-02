using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeatExchangeProcess : StuBrew.BrewingProcess
{
    float liquid = 0f;
    [SerializeField] float flowRate = 0.04f;

    [SerializeField] float coolTemp = 16f;

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
                if (toContainer.GetFillLevel() < 1)
                {
                    float transfered = toContainer.AddLiquid(flowRate * Time.deltaTime);
                    fluidOut?.Invoke(transfered);
                    liquid = Mathf.Clamp(liquid - transfered, 0, 100);
                    //fluidOut?.Invoke(flowRate);

                    liqProp.SetTemperature(coolTemp * 0.9f + liqProp.GetTemperature() * 0.1f);
                    TriggerNextProcess();
                }
            }
        }
    }
}
