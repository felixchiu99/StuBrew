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
            canNext = false;
            liqProp.canTransfer = true;
            processReset?.Invoke();
        }

        protected void TriggerNextProcess(bool completed = true)
        {
            if (completed)
            {
                hasProcessFinished = true;
                if (nextProcess.IsTransferEnable())
                {
                    processCompleted?.Invoke(completed);
                    //transferLiquid?.Invoke(liqProp);
                    nextProcess.SetLiquidProperties(liqProp);
                }

            }
            else
            {
                processCompleted?.Invoke(completed);
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

        public bool IsLiquidTransferrable(LiquidProperties prevliq)
        {
            return liqProp == prevliq || liqProp.IsDefault();
        }
        /*
        public bool IsTransferEnable()
        {
            return liqProp.canTransfer;
        }
        */
        // check next
        public bool IsTransferEnable()
        {
            return liqProp.canTransfer && nextProcess.IsLiquidTransferrable(liqProp);
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
