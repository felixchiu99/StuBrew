using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    Outline outline;

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
        outline.enabled = !outline.enabled;
    }
    public void SetHighLight(bool enabled)
    {
        outline.enabled = enabled;
    }
}
