using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.Events;

public class CheckVREnable : MonoBehaviour
{
    public bool isVR = false;

    public List<GameObject> XRObject;

    public List<GameObject> PCObject;

    [SerializeField] UnityEvent OnXR;
    [SerializeField] UnityEvent OnPC;

    // Start is called before the first frame update
    void Start()
    {
        // ...
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            isVR = true;
            SetObject(true);
            OnXR?.Invoke();
        }
        else
        {
            isVR = false;
            Debug.Log("Xr device not found");
            SetObject(false);
            OnPC?.Invoke();
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

    public bool CheckIsVR()
    {
        return isVR;
    }
}
