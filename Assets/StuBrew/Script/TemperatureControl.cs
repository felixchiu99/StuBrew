using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TemperatureControl : MonoBehaviour
{
    [SerializeField]
    float temperature = 18f;
    [SerializeField]
    float heatOverTime = 0.5f;
    [SerializeField]
    float heatLossOverTime = 0.1f;
    [SerializeField]
    float roomTemperature = 18f;
    [SerializeField]
    float maxTemperature = 200f;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField]
    float temperatureThreshold = 77f;
    [SerializeField]
    UnityEvent<bool> aboveTemperatureThreshold;

    void Update()
    {
        Cool();

        if(temperature > temperatureThreshold)
        {
            aboveTemperatureThreshold?.Invoke(true);
        }
        else
        {
            aboveTemperatureThreshold?.Invoke(false);
        }

        if(text)
            text.SetText(temperature.ToString("0"));
    }

    public void Heat()
    {
        temperature = Mathf.Clamp(temperature + heatOverTime * Time.deltaTime, roomTemperature, maxTemperature);
    }

    public void Cool()
    {
        Cool(heatLossOverTime);
    }

    public void Cool(float heatLoss)
    {
        temperature = Mathf.Clamp(temperature - heatLoss * Time.deltaTime, roomTemperature, maxTemperature);
    }
}
