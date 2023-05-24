using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Events;

public class LiquidValve : Autohand.PhysicsGadgetHingeAngleReader
{
    [SerializeField]
    float valveMinValue = 0f;
    [SerializeField]
    float valveMaxValue = 0.1f;

    [SerializeField]
    LiquidContainerSetting fromContainer;
    [SerializeField]
    LiquidContainerSetting toContainer;

    [SerializeField]
    Vector3 initRotation = new Vector3(0, 0, 0);

    public UnityEvent<float> valveOn;
    public UnityEvent<float> fluidOut;

    private bool allowFluidTransfer = true;

    protected override void Start()
    {
        base.Start();
        transform.localRotation = Quaternion.Euler(initRotation);
    }

    void Update()
    {
        var value = GetValue();
        float newValue = math.remap(-1, 1, valveMinValue, valveMaxValue, Mathf.Round(value));
        bool canTransfer = allowFluidTransfer;
        if (fromContainer && canTransfer)
        {
            float transfered = fromContainer.SubstractLiquid(newValue * Time.deltaTime);
            canTransfer = transfered == 0 ? false : canTransfer;
        }
        
        if (toContainer && canTransfer)
        {
            float transfered = toContainer.AddLiquid(newValue * Time.deltaTime);
            fluidOut?.Invoke(transfered * 10000);
        }
        else if(canTransfer)
        {
            Debug.Log("fluidOut");
            fluidOut?.Invoke(newValue * Time.deltaTime);
        }
    }

    public void EnableFluidTransfer(bool isEnable)
    {
        allowFluidTransfer = isEnable;
    }
}
