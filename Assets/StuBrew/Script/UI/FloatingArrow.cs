using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FloatingArrow : MonoBehaviour
{
    public Transform target;
    public float yOffset;

    [Button]
    public void SetPosition()
    {
        transform.position = target.position + new Vector3(0, yOffset, 0) ;
    }

    void Update()
    {

    }

    public void ChangeTarget(Transform target) 
    {
        this.target = target;
    }
}
