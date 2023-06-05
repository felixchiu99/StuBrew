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

    private GameObject triggeredObj;

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

    public void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        CustomTag tag = other.attachedRigidbody.GetComponent<CustomTag>();
        if (!tag)
            return;
        if (!tag.HasTag("Barrel"))
            return;
        SetLiquidProperties(other.attachedRigidbody.GetComponent<LiquidProperties>());
        triggeredObj = other.attachedRigidbody.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        CustomTag tag = other.attachedRigidbody.GetComponent<CustomTag>();
        if (!tag)
            return;
        if (!tag.HasTag("Barrel"))
            return;
        if (triggeredObj == other.attachedRigidbody.gameObject)
        {
            triggeredObj = null;
            liqProp.ClearLiq();
        }
    }
}
