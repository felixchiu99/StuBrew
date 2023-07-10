using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText;

    [SerializeField] CurrencyStatsObject manager;

    private void Start()
    {
        manager.OnValueChange += DisplayText;
        displayText.SetText(CurrencyManager.Instance.GetCurrentStored().ToString());
    }

    private void OnDestroy()
    {
        manager.OnValueChange -= DisplayText;
    }

    private void DisplayText(int display)
    {
        if (displayText)
            displayText.SetText(display.ToString());
            //displayText.SetText(CurrencyManager.Instance.GetCurrentStored().ToString());
    }
}
