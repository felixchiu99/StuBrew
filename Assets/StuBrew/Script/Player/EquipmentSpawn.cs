using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSpawn : MonoBehaviour
{
    [SerializeField] Vector3 positionOffset = new Vector3(0.3f, -0.6f, 0.01f);
    [SerializeField] Vector3 rotationOffset = new Vector3(0, 0, 90);

    void Awake()
    {
        UpdateTransform();
    }
    void LateUpdate()
    {
        UpdateTransform();
    }

    void UpdateTransform()
    {
        transform.position = transform.parent.position + new Vector3(0, positionOffset.y, 0);
        transform.localPosition += new Vector3(-positionOffset.x, 0, positionOffset.z);
        //transform.rotation = Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.parent.eulerAngles.y, 0) + rotationOffset);
    }
}
