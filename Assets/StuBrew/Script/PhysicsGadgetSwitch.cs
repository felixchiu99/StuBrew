using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGadgetSwitch : Autohand.PhysicsGadgetHingeAngleReader
{
    [SerializeField]
    bool isOn = false;

    [SerializeField]
    float cutOffValue = 0.5f;

    public UnityEvent switchOn;
    public UnityEvent switchOff;

    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;

    HingeJoint hinge;

    short forcedDir = 0;

    protected override void Start()
    {
        base.Start();
        hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLeverPos();
        if(isOn)
        {
            switchOn?.Invoke();
        }
        else
        {
            switchOff?.Invoke();
        }
    }
    
    float ForceLeverOn(bool dir)
    {
        bool actualDir = dir;
        if(forcedDir != 0)
        {
            actualDir = forcedDir > 0;
        }
        float targetPosition;
        if (actualDir)
        {
            targetPosition = 100;
        }
        else
        {
            targetPosition = 0;
        }
        return targetPosition;
    }

    void CheckLeverPos()
    {
        var spring = hinge.spring;
        spring.spring = 1f;
        if (forcedDir != 0)
            spring.spring = 5f;
        spring.damper = 1f;

        var value = GetValue();

        if(value > cutOffValue)
        {
            spring.targetPosition = ForceLeverOn(true);
            if (!isOn)
            {
                onSwitchOn?.Invoke();
            }
            isOn = true;
            if (forcedDir > 0)
                forcedDir = 0;
        }
        else
        {
            spring.targetPosition = ForceLeverOn(false);
            if (isOn)
            {
                onSwitchOff?.Invoke();
            }
            isOn = false;
            if (forcedDir < 0)
                forcedDir = 0;
        }

        hinge.spring = spring;
        hinge.useSpring = true;
    }

    public void ForceState(bool dir = false)
    {
        forcedDir = dir ? (short)1 : (short)-1;
    }
    public bool GetState()
    {
        return isOn;
    }
}
