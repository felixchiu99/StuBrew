using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StuBrew
{
    [RequireComponent(typeof(LiquidProperties))]
    public class BrewingProcess : MonoBehaviour
    {
        [SerializeField]
        UnityEvent processStarted;
        [SerializeField]
        UnityEvent processReset;
        [SerializeField]
        UnityEvent<bool> processCompleted;

        [SerializeField]
        UnityEvent<LiquidProperties> transferLiquid;

        [SerializeField] BrewingProcess nextProcess;

        protected bool canNext = false;

        protected LiquidProperties liqProp;

        protected void Start()
        {
            TriggerNextProcess(false);
            if (!liqProp)
                liqProp = GetComponent<LiquidProperties>();
        }

        protected void TriggerOnProcessStarted()
        {
            liqProp.canTransfer = false;
            processStarted?.Invoke();
        }
        protected void TriggerOnProcessReset()
        {
            liqProp.canTransfer = true;
            processReset?.Invoke();
        }

        protected void TriggerNextProcess(bool completed = true)
        {
            processCompleted?.Invoke(completed);
            if (completed)
            {
                //transferLiquid?.Invoke(liqProp);
                nextProcess.SetLiquidProperties(liqProp);
            }
        }

        public void ToggleCanNext()
        {
            canNext = !canNext;
        }

        protected bool CanNext()
        {
            return (canNext && nextProcess.IsTransferEnable());
        }

        public void BlendLiquidProperties(LiquidProperties prop , float blend)
        {
            liqProp.Copy(prop);
        }

        public void SetLiquidProperties(LiquidProperties prop)
        {
            liqProp.Copy(prop);
        }

        public bool IsTransferEnable()
        {
            return liqProp.canTransfer;
        }
    }
}
