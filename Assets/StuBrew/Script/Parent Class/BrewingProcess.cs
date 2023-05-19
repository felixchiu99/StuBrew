using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StuBrew
{
    public class BrewingProcess : MonoBehaviour
    {
        [SerializeField]
        UnityEvent<bool> processCompleted;
        protected bool canNext = false;
        protected void Start()
        {
            TriggerNextProcess(false);
        }

        protected void TriggerNextProcess(bool completed = true)
        {
            processCompleted?.Invoke(completed);
        }
    }
}
