using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUICollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ui collision");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ui trigger");
    }
}
