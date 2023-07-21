using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveFileDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI sceneText;

    [SerializeField] int index = 0;

    void Start()
    {
        if (gameObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI display))
        {
            displayText = display;
        }
        DisplayText();
        SaveSystem.OnFilenameChange += DisplayText;
    }

    private void OnDestroy()
    {
        SaveSystem.OnFilenameChange -= DisplayText;
    }

    private void DisplayText()
    {
        FileData file = SaveSystem.GetFileData(index);
        if (displayText)
            displayText.SetText(file.displayName);
        if (dateText)
            dateText.SetText(file.saveDate);
        if (sceneText)
            sceneText.SetText(file.saveScene);
    }
}
