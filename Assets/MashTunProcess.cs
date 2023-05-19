using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MashTunProcess : MonoBehaviour
{
    [SerializeField]
    ParticleHopper maltHopper;
    [SerializeField]
    LiquidContainerSetting water;

    float time = 0;
    [SerializeField]
    float timeOvertime = 0.01f;

    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        maltHopper = GetComponent<ParticleHopper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maltHopper.GetFillLevel() == 1 && water.GetFillLevel() == 1)
        {
            time = Mathf.Clamp(time + timeOvertime * Time.deltaTime, 0, 1);
            UpdateText();
        }
    }

    void UpdateText()
    {
        text.SetText((time).ToString("F2"));
    }
}
