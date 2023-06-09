using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnEntry : MonoBehaviour
{
    [SerializeField] Transform tpDestination;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("hi");
            other.attachedRigidbody.transform.position = tpDestination.position;
        }
    }
}
