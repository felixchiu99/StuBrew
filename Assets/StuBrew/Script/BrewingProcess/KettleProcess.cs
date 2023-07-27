using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


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

    private bool canStart = false;

    [SerializeField]
    UnityEvent processCanStart;
    [SerializeField]
    UnityEvent processResetCanStart;

    [SerializeField]
    UnityEvent<float> PassWaterLevel;

    public void SwitchedOn()
    {
        if (canStart)
        {
            isOn = true;
            playSFX?.Invoke(0);
        }
        else
        {
            playSFX?.Invoke(1);
        }

    }
    public void SwitchedOff()
    {
        isOn = false;
    }


    void Update()
    {
        waterFill = wort.GetFillLevel();
        liqProp.SetTemperature(tempControl.GetTemperature());
        if (waterFill > 0.7 && hopAmount[0] > 0)
        {
            if(!canStart)
                processCanStart?.Invoke();
            canStart = true;
        }
        else
        {
            if (canStart)
                processResetCanStart?.Invoke();
            canStart = false;
        }
        if (hasProcessFinished && waterFill <= 0)
        {
            ResetKettle();
        }
        if (canStart && isOn)
            ProgressTime();
        BlendLiquidStage();
        UpdateText();
    }

    void ProgressTime()
    {
        if (waterFill == 0)
            return;
        tempControl.Heat();
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
            SwitchedOff();
        }
        if (time < 1 && canNext)
        {
            canNext = false;
        }
        PassWaterLevel?.Invoke(waterFill);
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
        processResetCanStart?.Invoke();
    }

    private void UnexpectedObj(GameObject obj)
    {
        if (obj.TryGetComponent<Autohand.Grabbable>(out Autohand.Grabbable grab))
        {
            Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        CustomTag tag = other.attachedRigidbody.GetComponent<CustomTag>();
        if (!tag)
        {
            UnexpectedObj(other.attachedRigidbody.gameObject);
            return;
        }
            
        if (!tag.HasTag("Hops"))
        {
            UnexpectedObj(other.attachedRigidbody.gameObject);
            return;
        }

        hopAmount[hopStage]++;

        switch(hopStage){
            case 0:
                liqProp.ChangeBitterness(2f);
                break;
            case 1:
                liqProp.ChangeBitterness(1f);
                liqProp.ChangeAroma(0.5f) ;
                break;
            case 2:
                liqProp.ChangeBitterness(0.5f);
                liqProp.ChangeAroma(1f);
                break;
        }

        Destroy(other.attachedRigidbody.gameObject);
    }

    override public bool IsTransferEnable()
    {
        return true;
    }
}
