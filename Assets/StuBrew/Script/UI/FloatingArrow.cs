using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FloatingArrow : MonoBehaviour
{
    public Transform target;
    public float yOffset;
    public bool isBobbing = true;
    float bobbingSpeed = 0.0001f;
    public float yBobbingOffset = 0.01f;

    [SerializeField] TutorialSequencer tutManager;

    bool hasTeleported = false;
    bool isUp = true;

    void OnEnable()
    {
        if(!tutManager)
            tutManager = (TutorialSequencer)FindObjectOfType(typeof(TutorialSequencer));
        tutManager.OnIndexChange += OnTutChange;
        bobbingSpeed = yBobbingOffset * 0.0045f;
        if (target)
            SetPosition();
    }

    public void OnTutChange(TutorialObject tutObj)
    {
        target = tutObj.target;
        yOffset = tutObj.yOffset;
        SetPosition();
    }

    [Button]
    public void SetPosition()
    {
        transform.position = target.position + new Vector3(0, yOffset, 0) ;
    }

    void Update()
    {
        if (isBobbing)
        {
            if (target)
            {
                if (transform.position.y <= (target.position + new Vector3(0, yOffset, 0) - new Vector3(0, yBobbingOffset, 0)).y)
                    isUp = true;
                if (transform.position.y >= (target.position + new Vector3(0, yOffset, 0) + new Vector3(0, yBobbingOffset, 0)).y)
                    isUp = false;
                if (isUp)
                    transform.position = transform.position + new Vector3(0, bobbingSpeed, 0);
                else
                    transform.position = transform.position - new Vector3(0, bobbingSpeed, 0);
            }

        }
    }

    public void ChangeTarget(Transform target) 
    {
        this.target = target;
    }
}
