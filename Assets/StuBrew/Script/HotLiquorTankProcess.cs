using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotLiquorTankProcess : StuBrew.BrewingProcess
{
    [SerializeField]
    TemperatureControl tempControl;

    [SerializeField]
    float temperatureThreshold = 77f;

    new void Start()
    {
        base.Start();
        tempControl = GetComponent<TemperatureControl>();
    }

    void Update()
    {
        var temperature = tempControl.GetTemperature();
        //toggle next process
        if (temperature > temperatureThreshold && !canNext)
        {
            TriggerNextProcess();
            canNext = true;
        }
        //reset process avalibility
        if (temperature <= temperatureThreshold && canNext)
        {
            TriggerNextProcess(false);
            canNext = false;
        }
    }
}
