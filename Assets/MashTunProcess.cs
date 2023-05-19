using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MashTunProcess : StuBrew.BrewingProcess
{
    [SerializeField]
    ParticleHopper maltHopper;
    [SerializeField]
    LiquidContainerSetting water;

    float time = 0;
    [SerializeField]
    float timeOvertime = 0.01f;

    [SerializeField] TextMeshProUGUI waterText;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] private float waterTarget = 0.7f;
    private float waterFill = 0f;

    private short processStage = 0;

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
        if (maltHopper.GetFillLevel() >= 1 && water.GetFillLevel() >= 0.7)
        {
            time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
        }

        if(time >= 1 && !canNext)
        {
            TriggerNextProcess();
            canNext = true;
            maltHopper.ClearFill();
        }
        if (time < 1 && canNext)
        {
            TriggerNextProcess(false);
            canNext = false;
        }
        if(time > 0)
        {
            TriggerOnProcessStarted();
        }

        UpdateText();
    }

    void UpdateText()
    {
        timeText.SetText((time * 100).ToString("F2"));
        waterText.SetText((waterFill * 100 / waterTarget).ToString("F2"));
    }
}
