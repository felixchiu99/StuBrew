using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUICollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " ui collision");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " ui trigger");
    }
}
