using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIWorldSpaceToOverlay : MonoBehaviour
{
    [SerializeField] Canvas UI;
    [SerializeField] CanvasScaler UIScaler;
    [SerializeField] Transform parentUnder;
    [SerializeField] UnityEvent OnOverlay;

    public void SetUIToOverLay()
    {
        UI.transform.SetParent(parentUnder);
        UI.renderMode = RenderMode.ScreenSpaceOverlay;
        UIScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        UIScaler.referenceResolution = new Vector3(600, 450);
        UI.gameObject.SetActive(true);
        gameObject.SetActive(true);
        OnOverlay?.Invoke();
    }


}
