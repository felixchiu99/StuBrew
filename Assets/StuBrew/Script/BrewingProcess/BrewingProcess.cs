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

        [SerializeField] protected BrewingProcess nextProcess;

        protected bool canNext = false;
        protected bool hasProcessStarted = false;
        protected bool hasProcessFinished = false;

        protected LiquidProperties liqProp;

        protected void Start()
        {
            canNext = false;
            TriggerNextProcess(false);
            if (!liqProp)
                liqProp = GetComponent<LiquidProperties>();
            liqProp.canTransfer = true;
        }

        protected void TriggerOnProcessStarted()
        {
            hasProcessStarted = true;
            liqProp.canTransfer = false;
            processStarted?.Invoke();
        }
        protected void TriggerOnProcessReset()
        {
            hasProcessStarted = false;
            hasProcessFinished = false;
            canNext = false;
            liqProp.canTransfer = true;
            processReset?.Invoke();
        }

        protected void TriggerNextProcess(bool completed = true)
        {
            if (completed)
            {
                hasProcessFinished = true;

                if (IsTransferEnable())
                {
                    processCompleted?.Invoke(completed);
                    if(nextProcess)
                        nextProcess.SetLiquidProperties(liqProp);
                }

            }
        }

        public void ToggleCanNext()
        {
            canNext = !canNext;
        }

        protected bool CanFlow()
        {
            return canNext || (hasProcessFinished && nextProcess.IsTransferEnable());
        }

        public void BlendLiquidProperties(LiquidProperties prop , float blend)
        {
            liqProp.Copy(prop);
        }

        public void SetLiquidProperties(LiquidProperties prop)
        {
            liqProp.Copy(prop);
        }

        virtual public bool IsLiquidTransferrable(LiquidProperties prevliq)
        {
            return liqProp == prevliq || liqProp.IsDefault() || !hasProcessStarted;
        }
        // check next
        virtual public bool IsTransferEnable()
        {
            if(nextProcess)
                return nextProcess.IsLiquidTransferrable(liqProp);
            return true;
        }

        virtual public bool IsExportingFluid()
        {
            return hasProcessFinished && IsTransferEnable();
        }

        virtual public bool IsAcceptingFluid()
        {
            return !hasProcessStarted;
        }
    }
}
