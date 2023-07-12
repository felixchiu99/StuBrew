using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonColliderTrigger : MonoBehaviour
{
    [Tooltip("The layers that cause the Button to be activated")]
    public LayerMask collisionTriggers = ~0;

    [Tooltip("Reference to Button")]
    [SerializeField] Button btn;

    [Tooltip("Reference to Base of all related UI")]
    [SerializeField] UITouchable UIController;

    void Start()
    {
        if(btn == null)
        {
            if (TryGetComponent<Button>(out Button temp))
            {
                btn = temp;
            }
            if (TryGetComponent<Collider>(out Collider col))
            {
                col.isTrigger = true;
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                if (TryGetComponent<BoxCollider>(out BoxCollider boxCol))
                    boxCol.size = new Vector3(Mathf.Abs(rectTransform.sizeDelta.x), Mathf.Abs(rectTransform.sizeDelta.y), 1);
            }
        }
        if (UIController == null)
        {
            Rigidbody rb = GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                if (rb.gameObject.TryGetComponent<UITouchable>(out UITouchable controller))
                {
                    UIController = controller;
                }
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        OnTrigger(other);
    }

    void OnTrigger(Collider other)
    {
        if (!btn.interactable)
            return;
        if (collisionTriggers == (collisionTriggers | (1 << other.gameObject.layer)) && UIController.IsActive())
            if (UIController.IsFingerTip(other))
            {
                if (IsFront(other))
                    if (UIController.CheckHand(FindHand(other)))
                    {
                        UIController.SetInactive();
                        btn.onClick.Invoke();
                    }


            }
    }

    Autohand.Hand FindHand(Collider other)
    {
        Autohand.Hand hand = other.gameObject.GetComponentInParent<Autohand.Hand>();
        return hand;
    }

    bool IsFront(Collider other)
    {
        Vector3 finger = gameObject.transform.InverseTransformPoint(other.gameObject.transform.position);
        return finger.z < 0;
    }
}
