using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    private List<Canvas> uiList = new List<Canvas>();

    public void Add(Canvas ui)
    {
        uiList.Add(ui);
    }

    public bool IsAnyEnabled()
    {
        foreach(Canvas ui in uiList)
        {
            if (ui.enabled)
            {
                return true;
            }
        }
        return false;
    }


}
