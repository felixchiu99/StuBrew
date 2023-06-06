using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCall : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider> OnTriggerEnterCall;
    [SerializeField] UnityEvent<Collider> OnTriggerStayCall;
    [SerializeField] UnityEvent<Collider> OnTriggerExitCall;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterCall?.Invoke(other);
    }
    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayCall?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitCall?.Invoke(other);
    }
}
