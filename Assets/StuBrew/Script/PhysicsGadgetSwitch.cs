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

    HingeJoint hinge;

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

    void CheckLeverPos()
    {
        var spring = hinge.spring;
        spring.spring = 1f;
        spring.damper = 1f;

        var value = GetValue();

        if(value > cutOffValue)
        {
            spring.targetPosition = 100;
            isOn = true;
        }
        else
        {
            spring.targetPosition = 0;
            isOn = false;
        }

        hinge.spring = spring;
        hinge.useSpring = true;
    }
}
