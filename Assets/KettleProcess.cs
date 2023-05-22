using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KettleProcess : StuBrew.BrewingProcess
{
    //temp
    float temperature = 0;
    float minTemp = 77;
    float maxTemp = 102;
    [SerializeField]
    TemperatureControl tempControl;

    //water
    private float waterFill = 0f;
    [SerializeField]
    LiquidStage liquidStage;
    [SerializeField]
    LiquidContainerSetting wort;

    //hops
    public int hopStage = 0;
    int[] hopAmount = new int[3];

    //time/progress
    bool isOn = false;
    float time = 0;
    [SerializeField]
    float timeOvertime = 0.01f;

    //Text
    [SerializeField] TextMeshProUGUI tempText;
    [SerializeField] TextMeshProUGUI waterText;
    [SerializeField] TextMeshProUGUI hopText;
    [SerializeField] TextMeshProUGUI timeText;

    public void SwitchedOn()
    {
        isOn = true;
    }
    public void SwitchedOff()
    {
        isOn = false;
    }


    void Update()
    {
        waterFill = wort.GetFillLevel();
        ProgressTime();
        BlendLiquidStage();
        UpdateText();
    }

    void ProgressTime()
    {
        if (!isOn)
            return;
        if (waterFill == 0)
            return;
        time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
        if(time < 0.5f)
        {
            hopStage = 0;
        }
        if (time >= 0.5f && time < 0.9f)
        {
            hopStage = 1;
        }
        if(time >= 0.9f)
        {
            hopStage = 2;
        }
    }

    void BlendLiquidStage()
    {
        liquidStage.BlendLiquidStage(1, time);
    }

    void ResetKettle()
    {
        temperature = 18;

        hopStage = 0;
        for (int i = 0; i < 3; i++)
        {
            hopAmount[i] = 0;
        }
    }

    void UpdateText()
    {
        tempText.SetText((tempControl.GetTemperature()).ToString("F2"));
        waterText.SetText((waterFill* 100).ToString("F2"));
        timeText.SetText((time * 100).ToString("F2"));
    }
}
