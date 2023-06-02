using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FermentationProcess : StuBrew.BrewingProcess
{
    //Wort
    float liquidAmount = 0;
    [SerializeField]
    LiquidStage liquidStage;
    [SerializeField]
    LiquidContainerSetting wort;

    //Yeast
    float yeastAmount = 0;
    [SerializeField]
    private AnimationCurve yeastTimeGraph;

    //Time
    float time = 0;
    float timeOvertime = 0.01f;

    //Text
    [SerializeField] TextMeshProUGUI tempText;
    [SerializeField] TextMeshProUGUI waterText;
    [SerializeField] TextMeshProUGUI yeastText;
    [SerializeField] TextMeshProUGUI timeText;

    //barrel
    public GameObject barrelPrefab;
    public Transform spawnPoint;

    void Update()
    {
        liquidAmount = wort.GetFillLevel();
        ProgressTime();
        BlendLiquidStage();
        UpdateText();
        ProgressNext();
    }

    void AddYeast()
    {
        yeastAmount += 0.1f;
    }

    void ProgressTime()
    {
        if ((liquidAmount <= 0 || yeastAmount <= 0))
            return;
        timeOvertime = EvaluateCurve(yeastTimeGraph, yeastAmount);

        time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
    }

    void BlendLiquidStage()
    {
        liquidStage.BlendLiquidStage(1, time);
        if (time >= 1)
        {
            liquidStage.ChangeLiquidStage(1);
        }
    }

    void UpdateText()
    {
        yeastText.SetText((yeastAmount).ToString("F2"));
        waterText.SetText((liquidAmount * 100).ToString("F2"));
        timeText.SetText((time * 100).ToString("F2"));
        tempText.SetText(liqProp.GetTemperature().ToString("0"));
    }

    void ProgressNext()
    {
        if (time >= 1 && !canNext)
        {
            canNext = true;
            TriggerNextProcess();
        }
        if (time < 1 && canNext)
        {
            canNext = false;
        }
    }

    private float EvaluateCurve(AnimationCurve curve, float position)
    {
        return curve.Evaluate(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        CustomTag tag = other.attachedRigidbody.GetComponent<CustomTag>();
        if (!tag)
            return;
        if (!tag.HasTag("Yeast"))
            return;

        AddYeast();
        Destroy(other.attachedRigidbody.gameObject);
    }

    private void SpawnPrefab()
    {
        GameObject b = Instantiate(barrelPrefab, spawnPoint.position, Quaternion.identity);
        LiquidProperties liq = b.GetComponent<LiquidProperties>();
        liq.Copy(liqProp);
    }

    public void ResetProcess()
    {
        if (hasProcessFinished)
        {
            SpawnPrefab();
            wort.EmptyContent();
            yeastAmount = 0;
            time = 0;
            TriggerOnProcessReset();
        }
    }
}
