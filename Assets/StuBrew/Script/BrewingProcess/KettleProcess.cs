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
        liqProp.SetTemperature(tempControl.GetTemperature());
        if (hasProcessFinished && waterFill <= 0)
        {
            ResetKettle();
        }
        ProgressTime();
        BlendLiquidStage();
        UpdateText();
    }

    void ProgressTime()
    {
        if (!isOn || waterFill <= 0.7f)
            return;
        if (waterFill == 0)
            return;
        time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
        if (time < 0.5f)
        {
            hopStage = 0;
        }
        if (time >= 0.5f && time < 0.9f)
        {
            hopStage = 1;
        }
        if (time >= 0.9f && time < 1f)
        {
            hopStage = 2;
        }
        if (time >= 0 && !hasProcessStarted)
        {
            hasProcessStarted = true;
            liqProp.canTransfer = false;
        }
        else if(time <=0 && hasProcessStarted)
        {
            liqProp.canTransfer = true;
            hasProcessStarted = false;
        }
        if (time >= 1 && !CanFlow())
        {
            canNext = true;
            liquidStage.ChangeLiquidStage(1);
            liqProp.ChangeBitterness(0.2f);
            TriggerNextProcess();
        }
        if (time < 1 && canNext)
        {
            canNext = false;
        }
    }

    void BlendLiquidStage()
    {
        liquidStage.BlendLiquidStage(1, time);
        if(time >= 1)
        {
            liquidStage.ChangeLiquidStage(1);
        }
    }

    void UpdateText()
    {
        tempText.SetText((tempControl.GetTemperature()).ToString("F2"));
        waterText.SetText((waterFill* 100).ToString("F2"));
        timeText.SetText((time * 100).ToString("F2"));
        hopText.SetText(hopAmount[0].ToString("0") + " : " + hopAmount[1].ToString("0") + " : " + hopAmount[2].ToString("0"));
    }

    void ResetKettle()
    {
        temperature = 18;
        time = 0;
        hopStage = 0;
        for (int i = 0; i < 3; i++)
        {
            hopAmount[i] = 0;
        }
        liquidStage.ChangeLiquidStage(0);
        TriggerOnProcessReset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        CustomTag tag = other.attachedRigidbody.GetComponent<CustomTag>();
        if (!tag)
            return;
        if (!tag.HasTag("Hops"))
            return;
        
        hopAmount[hopStage]++;

        switch(hopStage){
            case 0:
                liqProp.ChangeBitterness(2f);
                return;
            case 1:
                liqProp.ChangeBitterness(1f);
                liqProp.ChangeAroma(0.5f) ;
                return;
            case 2:
                liqProp.ChangeBitterness(0.5f);
                liqProp.ChangeAroma(1f);
                return;
        }

        Destroy(other.attachedRigidbody.gameObject);
    }

    override public bool IsTransferEnable()
    {
        return true;
    }
}
