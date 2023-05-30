using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringProcess : StuBrew.BrewingProcess
{
    //Cleanliness
    float cleanliness = 1f;
    
    //Text
    [SerializeField] TextMeshProUGUI bitternessText;
    [SerializeField] TextMeshProUGUI sweetnessText;
    [SerializeField] TextMeshProUGUI aromaText;
    [SerializeField] TextMeshProUGUI flavourText;
    [SerializeField] TextMeshProUGUI cleanlinessText;

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        bitternessText.SetText(liqProp.GetBitterness().ToString("F2"));
        sweetnessText.SetText(liqProp.GetSweetness().ToString("F2"));
        aromaText.SetText(liqProp.GetAroma().ToString("F2"));
        cleanlinessText.SetText((cleanliness * 100).ToString("F2") );
    }
}
