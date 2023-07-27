using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(LiquidProperties))]
public class MashTunProcess : StuBrew.BrewingProcess
{
    [SerializeField]
    MaltHopper maltHopper;
    [SerializeField]
    LiquidStage liquidStage;
    [SerializeField]
    LiquidContainerSetting water;

    [SerializeField]
    LiquidStage maltLiquidStage;
    [SerializeField]
    LiquidContainerSetting malt;

    float time = 0;
    [SerializeField]
    float timeOvertime = 0.01f;

    [SerializeField] TextMeshProUGUI waterText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI maltText;

    [SerializeField] private float waterTarget = 0.7f;

    private int flushCounter = 0;
    private float waterFill = 0f;

    private bool canStart = false;
    private bool hasStarted = false;

    [SerializeField]
    UnityEvent processCanStart;
    [SerializeField]
    UnityEvent processResetCanStart;
    [SerializeField]
    UnityEvent<float> PassWaterFillLevel;

    new void Start()
    {
        base.Start();
        if (!maltHopper)
            maltHopper = GetComponent<MaltHopper>();
    }

    public void StartProcess()
    {
        if(!hasStarted && canStart)
        {
            hasStarted = true;
            TriggerOnProcessStarted();
            playSFX?.Invoke(0);
        }
        else
        {
            playSFX?.Invoke(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        canStart = false;

        waterFill = water.GetFillLevel();
        malt.SetVisualFill(maltHopper.GetFillLevel());

        PassWaterFillLevel?.Invoke(waterFill* 0.3f);

        if (maltHopper.GetFillLevel() >= 1 && water.GetFillLevel() >= 0.7 && canStart == false)
        {
            canStart = true;
            processCanStart?.Invoke();
        }

        if(hasStarted && !canNext && canStart)
        {
            time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
        }

        if (time >= 1 && !canNext)
        {
            canNext = true;
            canStart = false;
            processResetCanStart?.Invoke();
            MaltProperties maltProp = maltHopper.GetMaltProperties();

            liquidStage.ChangeLiquidStage(1);
            liqProp.ChangeSweetness(maltProp.GetSweetness() - 0.1f * flushCounter);
            liqProp.ChangeBitterness(maltProp.GetBitterness());
            liqProp.color = maltProp.GetColor();
            TriggerNextProcess();
            flushCounter++;
        }
        if (time < 1 && canNext)
        {
            canNext = false;
        }
        if (time > 0)
        {
            TriggerOnProcessStarted();
        }

        if (time >= 1 && waterFill <= 0)
        {
            time = 0;
            liquidStage.ChangeLiquidStage(0);
            canStart = false;
            processResetCanStart?.Invoke();
        }
        liquidStage.BlendLiquidStage(1, time);
        maltLiquidStage.BlendLiquidStage(1, waterFill / waterTarget);
        UpdateText();
    }

    void UpdateText()
    {
        timeText.SetText((time * 100).ToString("F2"));
        waterText.SetText((waterFill * 100 ).ToString("F2"));
        maltText.SetText(maltHopper.GetFillAmount().ToString("0"));
    }

    public void ResetProcess()
    {
        if (waterFill <= 0 && hasStarted)
        {
            hasStarted = false;
            maltHopper.ClearFill();
            TriggerOnProcessReset();
            processResetCanStart?.Invoke();
            playSFX?.Invoke(0);
        }
        else
        {
            playSFX?.Invoke(1);
        }
    }


}
