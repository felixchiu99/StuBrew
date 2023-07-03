using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldSpaceToOverlay : MonoBehaviour
{
    [SerializeField] Canvas UI;
    [SerializeField] Transform parentUnder;

    public void SetUIToOverLay()
    {
        UI.transform.SetParent(parentUnder);
        UI.renderMode = RenderMode.ScreenSpaceOverlay;
        UI.gameObject.SetActive(true);
    }


}
