using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class CheckVREnable : MonoBehaviour
{
    public GameObject XRPlayer;
    public GameObject PCPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // ...
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            // XR device detected/loaded
            Debug.Log("Xr device found");
            XRPlayer.SetActive(true);
            PCPlayer.SetActive(false);
        }
        else
        {
            Debug.Log("Xr device not found");
            XRPlayer.SetActive(false);
            PCPlayer.SetActive(true);
        }
    }
}
