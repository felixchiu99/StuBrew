using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class CheckVREnable : MonoBehaviour
{
    public List<GameObject> XRObject;
    public List<GameObject> PCObject;

    // Start is called before the first frame update
    void Start()
    {
        // ...
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            SetObject(true);
        }
        else
        {
            Debug.Log("Xr device not found");
            SetObject(false);
        }
    }

    void SetObject(bool isXR)
    {
        foreach(GameObject item in XRObject)
        {
            item.SetActive(isXR);
        }
        foreach (GameObject item in PCObject)
        {
            item.SetActive(!isXR);
        }
    }
}
