using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    Outline outline;

    bool overrideHighLight = false;

    protected void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }
    public void ToggleHighLight()
    {
        if (!overrideHighLight)
            outline.enabled = !outline.enabled;
    }
    public void SetHighLight(bool enabled)
    {
        if(!overrideHighLight)
            outline.enabled = enabled;
    }
    public void SetOverrideHighlight(bool isOverride)
    {
        overrideHighLight = isOverride;
    }
}
