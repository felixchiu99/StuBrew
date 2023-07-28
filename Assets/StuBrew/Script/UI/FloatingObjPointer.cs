using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjPointer : MonoBehaviour
{
    public Transform target;
    public GameObject ui;

    private bool isLockY = false;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            ui.SetActive(true);

            Vector3 pos = target.position;
            if(isLockY)
                pos.y = transform.position.y;

            transform.LookAt(pos);
        }
        else
        {
            ui.SetActive(false);
        }
    }

    public void ChangeTarget(Transform target = null)
    {
        this.target = target;
    }
}
