using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HotLiquorTankProcess : StuBrew.BrewingProcess
{
    [SerializeField]
    TemperatureControl tempControl;

    [SerializeField]
    float temperatureThreshold = 77f;

    [SerializeField] PhysicsGadgetSwitch physicsSwitch;

    [SerializeField]
    LiquidContainerSetting water;

    [SerializeField]
    UnityEvent<float> PassTemperatureLevel;
    [SerializeField]
    UnityEvent<float> PassWaterLevel;

    new void Start()
    {
        base.Start();
        if(!tempControl)
            tempControl = GetComponent<TemperatureControl>();
    }

    void Update()
    {
        var temperature = tempControl.GetTemperature();
        //toggle next process
        if (temperature > temperatureThreshold)
        {
            if (!canNext)
            {
                TriggerNextProcess();
                canNext = true;
                liqProp.SetTemperature(temperature);
            }
        }
        //reset process avalibility
        if (temperature <= temperatureThreshold && canNext)
        {
            TriggerNextProcess(false);
            canNext = false;
        }

        PassTemperatureLevel?.Invoke(temperature);
        PassWaterLevel?.Invoke(water.GetFillLevel());
    }

    override public bool IsExportingFluid()
    {
        bool exporting = base.IsExportingFluid();

        return exporting && tempControl.GetTemperature() > temperatureThreshold;
    }
}
