using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjPointer : MonoBehaviour
{
    public Transform target;
    public GameObject ui;

    private bool isLockY = false;

    [SerializeField] TutorialSequencer tutManager;

    void OnEnable()
    {
        if (!tutManager)
            tutManager = (TutorialSequencer)FindObjectOfType(typeof(TutorialSequencer));
        tutManager.OnIndexChange += OnTutChange;
    }

    public void OnTutChange(TutorialObject tutObj)
    {
        target = tutObj.target;
        if (!tutObj.isShow)
            target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (target.position.y < -100)
            {
                ui.SetActive(false);
                return;
            }
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
