using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

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

    private bool canStart = false;
    private bool hasStarted = false;

    [SerializeField]
    UnityEvent processCanStart;
    [SerializeField]
    UnityEvent processResetCanStart;

    void Update()
    {
        liquidAmount = wort.GetFillLevel();
        if((liquidAmount >= 0.5f && yeastAmount >= 2))
        {
            if(!canStart)
                processCanStart?.Invoke();
            canStart = true;
        }
        ProgressTime();
        BlendLiquidStage();
        UpdateText();
        ProgressNext();
    }

    public void StartProcess()
    {
        if (!hasStarted && canStart)
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

    void AddYeast()
    {
        yeastAmount += 1f;
    }

    void ProgressTime()
    {
        if (!hasStarted)
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
        yeastText.SetText((yeastAmount).ToString("0"));
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

        if (!tag.HasTag("Yeast"))
        {
            UnexpectedObj(other.attachedRigidbody.gameObject);
            return;
        }

        AddYeast();
        Destroy(other.attachedRigidbody.gameObject);
    }

    private void SpawnPrefab(int i)
    {
        Vector3 pos = spawnPoint.position + new Vector3(0, i * 0.7f, 0);
        GameObject b = Instantiate(barrelPrefab, pos, Quaternion.identity);
        LiquidProperties liq = b.GetComponent<LiquidProperties>();
        liq.Copy(liqProp);
    }

    public void ResetProcess()
    {
        if (hasProcessFinished)
        {
            int barrelCount = (int)(wort.GetLiquidStored() / 0.2f);
            for(int i = 0; i < barrelCount; i++)
            {
                SpawnPrefab(i);
            }
            wort.EmptyContent();
            yeastAmount = 0;
            time = 0;
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
