using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LiquidProperties))]
public class MashTunProcess : StuBrew.BrewingProcess
{
    [SerializeField]
    ParticleHopper maltHopper;
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

    new void Start()
    {
        base.Start();
        if (!maltHopper)
            maltHopper = GetComponent<ParticleHopper>();
    }

    // Update is called once per frame
    void Update()
    {
        waterFill = water.GetFillLevel();
        malt.SetVisualFill(maltHopper.GetFillLevel());
        
        if (maltHopper.GetFillLevel() >= 1 && water.GetFillLevel() >= 0.7)
        {
            time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
        }

        if (time >= 1 && !canNext)
        {
            canNext = true;
            liquidStage.ChangeLiquidStage(1);
            liqProp.SetSweetness(1 - 0.1f * flushCounter);
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
        }
        liquidStage.BlendLiquidStage(1, time);
        maltLiquidStage.BlendLiquidStage(1, waterFill / waterTarget);
        UpdateText();
    }

    void UpdateText()
    {
        timeText.SetText((time * 100).ToString("F2"));
        waterText.SetText((waterFill * 100 / waterTarget).ToString("F2"));
        maltText.SetText(maltHopper.GetFillAmount().ToString("0"));
    }

    public void ResetProcess()
    {
        if (waterFill <= 0)
        {
            maltHopper.ClearFill();
            TriggerOnProcessReset();
        }
    }
}
