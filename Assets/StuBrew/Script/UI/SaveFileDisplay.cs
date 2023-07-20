using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveFileDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText;

    [SerializeField] int index = 0;

    void Start()
    {
        if (gameObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI display))
        {
            displayText = display;
            DisplayText();
        }
        SaveSystem.OnFilenameChange += DisplayText;
    }

    private void OnDestroy()
    {
        SaveSystem.OnFilenameChange -= DisplayText;
    }

    private void DisplayText()
    {
        if (displayText)
            displayText.SetText(SaveSystem.GetFileName(index));
    }
}
