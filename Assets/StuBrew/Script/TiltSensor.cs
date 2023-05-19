using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltSensor : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] bool isTilted = false;
    [SerializeField] Transform pourTip;

    [SerializeField] ParticleContainer container;
    [SerializeField] float pourAngleStart = 70;

    [SerializeField] private AnimationCurve flowRateGraph;
    [SerializeField] private AnimationCurve tiltRateGraph;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTilted)
        {
            container.Stop();
        }
        else
        {
            container.Play();
        }

        Vector3 tip = pourTip.position - transform.position;

        float fillLevel = container.GetFillLevel();
        pourAngleStart = 90 * EvaluateCurve(tiltRateGraph, fillLevel);

        float tiltAngle = Vector3.SignedAngle(tip, Vector3.up, transform.right);

        //Debug.Log(tiltAngle + "  " + (Mathf.Abs(tiltAngle) - pourAngleStart) / Mathf.Abs(tiltAngle));

        float flowRate = (Mathf.Abs(tiltAngle) - pourAngleStart) / 90;
        if (tiltAngle + Vector3.Angle(tip, transform.up) < -90)
        {
            flowRate = 1;
        }
        container.SetFlowRate( (int) (100 * EvaluateCurve(flowRateGraph, flowRate)));

        if (tiltAngle > pourAngleStart || tiltAngle < -pourAngleStart)
        {
            isTilted = true;
        }
        else
        {
            isTilted = false;
        }
    }

    private float EvaluateCurve(AnimationCurve curve, float position)
    {
        return curve.Evaluate(position);
    }

}
